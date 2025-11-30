
using UnityEngine;

public class WallManager : MonoBehaviour
{
    [Header("Wall Settings")]
    public GameObject leftWallPrefab;
    public GameObject rightWallPrefab;
    public Transform player;
    public float wallLength = 100f;
    
    private GameObject currentLeftWall;
    private GameObject currentRightWall;
  
    void Start()
    {
        CreateWalls();
    }
    
    void Update()
    {
        if (player != null)
        {
            float wallZ = player.position.z;
            
            if (currentLeftWall != null)
                currentLeftWall.transform.position = new Vector3(-5,1,wallZ);
            if (currentRightWall != null)
                currentRightWall.transform.position = new Vector3(5,1,wallZ);
        }
    }

    void CreateWalls()
    {
        currentLeftWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        currentLeftWall.name = "LeftWall";
        currentLeftWall.transform.position = new Vector3(-5,1,0);
        currentLeftWall.transform.localScale = new Vector3(0.5f, 2f, wallLength);
        
        currentRightWall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        currentRightWall.name = "RightWall";
        currentRightWall.transform.position = new Vector3(5,1,0);
        currentRightWall.transform.localScale = new Vector3(0.5f, 2f, wallLength);
    }
}
