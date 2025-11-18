using UnityEngine;

public class ConditionalItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform[] spawnPoints;
    public PlayerHealth player; // Referencia al script del jugador

    private bool itemSpawned = false;

    void Update()
    {
        if (!itemSpawned && player != null && player.currentHealth < 50)
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
