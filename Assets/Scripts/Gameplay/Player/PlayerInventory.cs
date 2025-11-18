using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    // Esta variable nos dice si el jugador tiene la llave o no
    public bool hasKey = false;

    // Método para darle la llave al jugador
    public void GetKey()
    {
        hasKey = true;
        Debug.Log("Inventario: el jugador ahora tiene la llave.");
    }

    // Si en el futuro querés que pueda perder la llave:
    public void RemoveKey()
    {
        hasKey = false;
        Debug.Log("Inventario: el jugador perdió la llave.");
    }
}
