using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject[] collectiblePrefabs;
    public GameObject[] hazardPrefabs;
    public float spawnDistance = 20f;
    public float spawnInterval = 2f;
    
    [Header("Difficulty")]
    [Range(0f, 1f)]
    public float hazardSpawnChance = 0.2f;
    
    [Header("Trap Patterns")]
    public bool enableTraps = true;
    [Range(0f, 1f)]
    public float trapChance = 0.3f; // 30% chance for trap pattern
    public float trapDistance = 3f; // Distance after collectible
    
    [Header("Lane Positions")]
    public float laneDistance = 3f;
    
    private float nextSpawnTime = 0f;
    private Transform player;
    private int lastSpawnLane = 1; // Remember last lane
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    
    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnItem();
            nextSpawnTime = Time.time + spawnInterval;
        }
        
        // Gradually increase hazard chance over time
        hazardSpawnChance = Mathf.Min(0.5f, hazardSpawnChance + Time.deltaTime * 0.001f);
    }
    
    void SpawnItem()
    {
        if (player == null) return;
        
        // Decide: collectible or hazard?
        bool spawnHazard = Random.value < hazardSpawnChance;
        GameObject prefab = null;
        
        if (spawnHazard && hazardPrefabs.Length > 0)
        {
            prefab = hazardPrefabs[Random.Range(0, hazardPrefabs.Length)];
            SpawnSingleItem(prefab);
        }
        else if (collectiblePrefabs.Length > 0)
        {
            prefab = collectiblePrefabs[Random.Range(0, collectiblePrefabs.Length)];
            SpawnSingleItem(prefab);
            
            // TRAP PATTERN: Sometimes spawn hazard right after collectible!
            if (enableTraps && Random.value < trapChance && hazardPrefabs.Length > 0)
            {
                SpawnTrap();
            }
        }
    }
    
    void SpawnSingleItem(GameObject prefab)
    {
        if (prefab == null) return;
        
        // Random lane
        int lane = Random.Range(0, 3);
        lastSpawnLane = lane; // Remember for trap
        
        float xPos = (lane - 1) * laneDistance;
        float zPos = player.position.z + spawnDistance;
        
        Vector3 spawnPosition = new Vector3(xPos, 0.5f, zPos);
        
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
    
    void SpawnTrap()
    {
        // Spawn hazard in SAME lane as last collectible
        GameObject hazardPrefab = hazardPrefabs[Random.Range(0, hazardPrefabs.Length)];
        
        float xPos = (lastSpawnLane - 1) * laneDistance;
        float zPos = player.position.z + spawnDistance + trapDistance;
        
        Vector3 spawnPosition = new Vector3(xPos, 0.5f, zPos);
        
        Instantiate(hazardPrefab, spawnPosition, Quaternion.identity);
        
        Debug.Log("TRAP! Hazard spawned after collectible in lane " + lastSpawnLane);
    }
    
    public void IncreaseDifficulty()
    {
        hazardSpawnChance = Mathf.Min(0.6f, hazardSpawnChance + 0.1f);
        spawnInterval = Mathf.Max(0.5f, spawnInterval - 0.1f);
        trapChance = Mathf.Min(0.5f, trapChance + 0.05f); // More traps!
        
        Debug.Log("Difficulty increased! Hazards: " + (hazardSpawnChance * 100) + "%");
    }
}