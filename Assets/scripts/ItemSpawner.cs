using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] collectiblePrefabs;

    public float spawnDistance = 20f;
    public float spawnInterval = 2f;

    [Header("Lane Positions")] public float laneDistance = 3f;

    private float nextSpawnTime = 0f;
    private Transform player;
    void Start()
    {
        player  = GameObject.FindGameObjectWithTag("Player").transform;
    }
    void Update()
    {
        if  (Time.time > nextSpawnTime)
            {
                SpawnItem();    
                nextSpawnTime = Time.time + spawnInterval;
            }
    }

    void SpawnItem()
    {
        if (collectiblePrefabs.Length == 0 || player == null)
            return;
        
        GameObject prefab = collectiblePrefabs[Random.Range(0, collectiblePrefabs.Length)];
        
        int lane = Random.Range(0, 3);
        float xPos = (lane - 1) * laneDistance;
        
        float zPos = player.position.z + spawnDistance;
        Vector3 spawnPosition = new Vector3(xPos, 0.5f, zPos);
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
