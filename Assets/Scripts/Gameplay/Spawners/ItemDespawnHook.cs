using UnityEngine;

public class ItemDespawnHook : MonoBehaviour
{
    public System.Action onDespawn;

    private void OnDisable()
    {
        onDespawn?.Invoke();
    }
}
