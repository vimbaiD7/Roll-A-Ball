using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0f, 3f, -5f);
    
    void LateUpdate()
    {
        if (player != null)
        {
            transform.position = player.position + offset;
            transform.LookAt(player);
        }
        else
        {
            Debug.LogError("CAMERA: Player is not assigned! Drag Player to CameraFollow script.");
        }
    }
}
