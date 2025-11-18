using UnityEngine;
using System.Collections.Generic;

public class ItemSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform[] spawnPoints;
    public float interval = 3f;
    public int maxAlive = 5;

    private List<GameObject> pool = new List<GameObject>();
    private int aliveCount = 0;

    void Start()
    {
        int poolSize = maxAlive > 0 ? maxAlive : 5;
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            obj.SetActive(false);
            pool.Add(obj);

            ItemDespawnHook hook = obj.AddComponent<ItemDespawnHook>();
            hook.onDespawn = () => aliveCount--;
        }

        InvokeRepeating(nameof(SpawnOne), interval, interval);
    }

    void SpawnOne()
    {
        if (spawnPoints.Length == 0) return;
        if (maxAlive > 0 && aliveCount >= maxAlive) return;

        GameObject obj = pool.Find(o => !o.activeSelf);
        if (obj == null) return;

        Transform p = spawnPoints[Random.Range(0, spawnPoints.Length)];
        obj.transform.position = p.position;
        obj.SetActive(true);

        aliveCount++;
    }

    void OnDrawGizmosSelected()
    {
        if (spawnPoints == null) return;
        Gizmos.color = Color.yellow;
        foreach (var t in spawnPoints)
            if (t) Gizmos.DrawWireSphere(t.position, 0.25f);
    }
}
