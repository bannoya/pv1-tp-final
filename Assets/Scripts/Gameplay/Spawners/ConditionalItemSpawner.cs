using UnityEngine;

public class ConditionalItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform[] spawnPoints;
    

    private bool itemSpawned = false;
    public PlayerStats playerStats;
    void Update()
    {
        if (!itemSpawned && playerStats.zombieKills >= 10)
        {
            SpawnItem();
        }
    }

    void SpawnItem()
    {
        if (itemPrefab == null || spawnPoints.Length == 0) return;

        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        Instantiate(itemPrefab, spawnPoint.position, Quaternion.identity);

        itemSpawned = true;
    }
}
