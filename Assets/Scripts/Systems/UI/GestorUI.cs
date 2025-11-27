using UnityEngine;

public class GestorUI : MonoBehaviour
{
    [Header("Colocar los paneles en orden aqui")]
    public UIPanel[] paneles;
    public UIPanel panelActual;
    void Start()
    {
        OcultarPaneles();
        MostrarPaneles(0);
    }

    public void MostrarPaneles(int indice)
    {
        // 1. Protección básica de índice
        if (paneles == null || indice < 0 || indice >= paneles.Length)
        {
            Debug.LogError("Error: Índice incorrecto o lista vacía.");
            return;
        }

        // 2. Protección: ¿El panel que queremos mostrar EXISTE?
        if (paneles[indice] == null)
        {
            Debug.LogError("¡CUIDADO! El Elemento " + indice + " en la lista 'Paneles' está VACÍO en el Inspector.");
            return;
        }

        if (panelActual != null)
        {
            panelActual.Ocultar();
        }

        paneles[indice].Mostrar();
        panelActual = paneles[indice];
    }

    public void OcultarPaneles()
    {
        // 1. Protección: Si la lista ni siquiera existe, nos vamos.
        if (paneles == null) return;

        for (int i = 0; i < paneles.Length; i++)
        {
            // 2. Protección: Solo intentamos ocultar si el panel NO es nulo (si existe)
            if (paneles[i] != null)
            {
                paneles[i].Ocultar();
            }
        }
    }

    public void Salir()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
