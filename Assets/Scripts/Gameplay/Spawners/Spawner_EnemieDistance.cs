using UnityEngine;

public class Spawner_EnemieDistance : MonoBehaviour


{
    [Header("Prefab y puntos de spawn")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;

    [Header("Timing")]
    public float initialDelay = 1f;
    public float interval = 3f;

    [Header("Límites")]
    public int maxAlive = 15;     // 0 = sin límite
    public int maxTotal = 0;      // 0 = sin límite (acumulado)

    int aliveCount = 0;
    int totalSpawned = 0;

    void OnEnable()
    {
        InvokeRepeating(nameof(SpawnTick), initialDelay, interval);
    }

    void OnDisable()
    {
        CancelInvoke(nameof(SpawnTick));
    }

    void SpawnTick()
    {
        if (enemyPrefab == null || spawnPoints.Length == 0) return;
        if (maxAlive > 0 && aliveCount >= maxAlive) return;
        if (maxTotal > 0 && totalSpawned >= maxTotal) { CancelInvoke(nameof(SpawnTick)); return; }

        SpawnOne();
    }

    void SpawnOne()
    {
        Transform p = spawnPoints[Random.Range(0, spawnPoints.Length)];
        var go = Instantiate(enemyPrefab, p.position, Quaternion.identity);

        aliveCount++;
        totalSpawned++;

        // Cuando muera el enemigo, decrementa aliveCount
        // Opción: hookear un evento/método en el enemigo:
        go.AddComponent<OnDestroyHook>().onDestroyed = () => aliveCount--;
    }

    // Dibuja gizmos para ver puntos
    void OnDrawGizmosSelected()
    {
        if (spawnPoints == null) return;
        Gizmos.color = Color.red;
        foreach (var t in spawnPoints) if (t) Gizmos.DrawWireSphere(t.position, 0.25f);
    }
}

public class OnDestroyHook : MonoBehaviour
{
    public System.Action onDestroyed;
    void OnDestroy() { onDestroyed?.Invoke(); }
}
