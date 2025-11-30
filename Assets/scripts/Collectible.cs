
using UnityEngine;

public class Collectible : MonoBehaviour
{
    [Header("Collectible Settings")] 
    public int pointValue = 10;
    public CollectibleType type = CollectibleType.Trophy;

    [Header("Visual Effects")] 
    public float rotateSpeed = 100f;
    public float bobSpeed = 2f;
    public float bobHeight = 0.3f;
    
    private Vector3 startPosition;  
    void Start()
    {
        startPosition = transform.position;
    }
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        
        float newY = startPosition.y + Mathf.Sin(Time.time * bobSpeed) * bobHeight;
        transform.position = new Vector3(startPosition.x, newY, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.AddScore(pointValue);
            Destroy(gameObject);
        }
    }

    public enum CollectibleType
    {
        Trophy,
        Money,
        Battery,
        SpeedBoost,
        Magnet,
        Shield,
        HealthKit
    }
}
