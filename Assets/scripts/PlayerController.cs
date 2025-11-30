
using UnityEngine;

public class PlayerController : MonoBehaviour
{ 
    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float laneDistance = 3f;

    [Header("Lane System")] 
    private int currentLane = 1;
    private float targetX;
    
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        targetX = 0;
    }
    void Update()
    {
        HandleInput();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, moveSpeed);
        float newX = Mathf.Lerp(transform.position.x, targetX, Time.fixedDeltaTime * moveSpeed);
        transform.position = new Vector3(newX, transform.position.y, transform.position.z);
    }

    void HandleInput()
    {
        if (Input.GetKey(KeyCode.A) ||  Input.GetKeyDown(KeyCode.LeftArrow) )
        {
            if (currentLane > 0)
            {
                currentLane--;
                UpdateTargetX();
            }
        }
        if (Input.GetKey(KeyCode.D) ||  Input.GetKeyDown(KeyCode.RightArrow) )
            {
            if (currentLane < 2)
                {
                currentLane++;
                UpdateTargetX();
                }
            }
    }

    void UpdateTargetX()
    {
        targetX = (currentLane - 1) * laneDistance;
    }
}
