using UnityEngine;
using UnityEngine.UI;

public class SeleccionarNivel : UIPanel
{
    [SerializeField] private Button btnVolver;
    [SerializeField] private GestorUI gestor;

    public override void Mostrar()
    {
        gameObject.SetActive(true);

        if (gestor == null)
        {
            Debug.LogError("GestorUI no asignado en SeleccionarNivel");
            return;
        }

        btnVolver.onClick.RemoveAllListeners();
        btnVolver.onClick.AddListener(() => gestor.MostrarPaneles(0)); // vuelve al menú principal
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
}
