
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Target")] 
    public Transform player;
    
    [Header("Camera Settings")]
    public Vector3 offset = new Vector3(0f, 3f, -5f);
    public float smoothSpeed = 5f;
    void LateUpdate()
    {
        if (player == null) 
            return;

        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = smoothedPosition;
        
        transform.LookAt(player.position + Vector3.up);
    }
}
