using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : UIPanel
{
    [SerializeField] private Button btnIniciarJuego;
    [SerializeField] private Button btnSeleccionarNivel;
    [SerializeField] private Button btnSalirJuego;
    [SerializeField] private GestorUI gestor;

    public override void Mostrar()
    {
        gameObject.SetActive(true);

        if (gestor == null)
        {
            Debug.LogError("El GestorUI no se asignó a este panel");
            return;
        }

        btnIniciarJuego.onClick.RemoveAllListeners();
        btnIniciarJuego.onClick.AddListener(() => CargarNivel1());

        btnSeleccionarNivel.onClick.RemoveAllListeners();
        btnSeleccionarNivel.onClick.AddListener(() => gestor.MostrarPaneles(1)); // muestra el panel SeleccionarNivel (índice 1)

        btnSalirJuego.onClick.RemoveAllListeners();
        btnSalirJuego.onClick.AddListener(() => gestor.Salir());
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }

    private void CargarNivel1()
    {
        SceneManager.LoadScene("Level01");
    }
}
