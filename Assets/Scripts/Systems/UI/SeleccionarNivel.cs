using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class SeleccionarNivel : UIPanel
{
    [Header("Botones de Navegación")]
    [SerializeField] private Button btnVolver;
    [SerializeField] private Button btnNivel1; 
    [SerializeField] private Button btnNivel2; 

    [Header("Referencias")]
    [SerializeField] private GestorUI gestor;

    public override void Mostrar()
    {
        gameObject.SetActive(true);

        if (gestor == null)
        {
            Debug.LogError("GestorUI no asignado en SeleccionarNivel");
            return;
        }

        // --- Configurar Botón VOLVER ---
        if (btnVolver != null)
        {
            btnVolver.onClick.RemoveAllListeners();
            btnVolver.onClick.AddListener(() => gestor.MostrarPaneles(0)); // Vuelve al menú principal
        }

        // --- Configurar Botón NIVEL 1 ---
        if (btnNivel1 != null)
        {
            btnNivel1.onClick.RemoveAllListeners();
            // Aquí le decimos que cargue "Level01"
            btnNivel1.onClick.AddListener(() => CargarEscena("Level01"));
        }

        // --- Configurar Botón NIVEL 2 ---
        if (btnNivel2 != null)
        {
            btnNivel2.onClick.RemoveAllListeners();
            // Aquí le decimos que cargue "Level02"
            btnNivel2.onClick.AddListener(() => CargarEscena("Level02"));
        }
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }

    // Función auxiliar para cargar la escena y asegurar el tiempo correcto
    private void CargarEscena(string nombreEscena)
    {
        Time.timeScale = 1f; // Aseguramos que el juego no esté pausado
        SceneManager.LoadScene(nombreEscena);
    }
}