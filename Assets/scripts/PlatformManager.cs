
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [Header("Platform Settings")] 
    public GameObject platformSegment;
    public int numberOfSegments = 5;
    public float segmentLength = 20f;
    
    [Header("References")]
    public Transform player;
    
    private List<GameObject> activeSegments = new List<GameObject>();
    private float spawnZ = 0f;
    private float safeZone = 40f;
 
    void Start()
    {
        for (int i = 0; i < numberOfSegments; i++)
        {
            SpawnSegment();
        }
    }
    
    void Update()
    {
        if (player.position.z > (spawnZ - safeZone))
            {
            SpawnSegment();
            DeleteOldSegment();
            }
    }

    void SpawnSegment()
    {
        GameObject segment = Instantiate(platformSegment,
            new Vector3(0, -0.5f, spawnZ),
            Quaternion.identity);
        activeSegments.Add(segment);
        spawnZ += segmentLength;
    }

    void DeleteOldSegment()
    {
        if (activeSegments.Count > numberOfSegments)
            {
                GameObject oldSegment = activeSegments[0];
                activeSegments.RemoveAt(0);
                Destroy(oldSegment);
            }
    }
    
}
