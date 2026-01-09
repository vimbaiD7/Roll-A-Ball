using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    
    private Vector3 originalPosition;
    private float shakeTimeRemaining = 0f;
    private float shakePower = 0f;
    
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    void Start()
    {
        originalPosition = transform.localPosition;
    }
    
    void Update()
    {
        if (shakeTimeRemaining > 0)
        {
            shakeTimeRemaining -= Time.deltaTime;
            
            float xAmount = Random.Range(-1f, 1f) * shakePower;
            float yAmount = Random.Range(-1f, 1f) * shakePower;
            
            transform.localPosition = originalPosition + new Vector3(xAmount, yAmount, 0);
        }
        else
        {
            transform.localPosition = originalPosition;
        }
    }
    
    public void TriggerShake(float power, float duration)
    {
        shakePower = power;
        shakeTimeRemaining = duration;
    }
}