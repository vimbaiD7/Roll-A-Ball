using UnityEngine;


public class Hazard : MonoBehaviour
{
    [Header("Hazard Settings")]
    public HazardType hazardType = HazardType.Skull;
    public int damage = 1;
    
    [Header("Visual Effects")]
    public float rotateSpeed = 100f;
    public float bobSpeed = 3f;
    public float bobHeight = 0.2f;
    public Color warningColor = Color.red;
    
    [Header("Effects")]
    public GameObject hitEffectPrefab;
    
    private Vector3 startPosition;
    private float pulseTime = 0f;
    private Renderer meshRenderer;
    private Material material;
    
    void Start()
    {
        startPosition = transform.position;
        meshRenderer = GetComponent<Renderer>();
        
        // Create unique material instance
        if (meshRenderer != null)
        {
            material = meshRenderer.material;
        }
    }
    
    void Update()
    {
        // Rotate menacingly
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        
        // Bob up and down
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        
        // Pulse effect (warning!)
        pulseTime += Time.deltaTime * 3f;
        float pulse = (Mathf.Sin(pulseTime) + 1f) / 2f; // 0 to 1
        
        if (material != null)
        {
            // Pulsing emission
            material.SetColor("_EmissionColor", warningColor * pulse * 2f);
        }
        
        // Destroy if far behind player
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && transform.position.z < player.transform.position.z - 20f)
        {
            Destroy(gameObject);
        }
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (CameraShake.Instance != null)
            {
                CameraShake.Instance.TriggerShake(0.3f, 0.3f);
            }
            switch (hazardType)
            {
                case HazardType.Skull:
                    GameManager.Instance.LoseLife();
                    break;
                    
                case HazardType.SkullAndBones:
                    GameManager.Instance.LoseLife();
                    // Maybe lose 2 lives or instant death?
                    break;
                    
                case HazardType.Lock:
                    FreezePlayer();
                    break;
            }
            
            // Spawn hit effect
            if (hitEffectPrefab != null)
            {
                Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            }
            
            // Destroy hazard
            Destroy(gameObject);
        }
    }
    
    void FreezePlayer()
    {
        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        if (player != null)
        {
            player.FreezeControls(2f);
        }
    }
}

public enum HazardType
{
    Skull,
    SkullAndBones,
    Lock
}
