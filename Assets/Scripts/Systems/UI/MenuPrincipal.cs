using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuPrincipal : UIPanel
{
    [SerializeField] private Button btnIniciarJuego;
    [SerializeField] private Button btnSeleccionarNivel;
    [SerializeField] private Button btnSalirJuego;
    [SerializeField] private GestorUI gestor;
    AudioManagerMenu audioManager;

    public override void Mostrar()
    {
        gameObject.SetActive(true);

        if (gestor == null)
        {
            Debug.LogError("El GestorUI no se asignó a este panel");
            return;
        }

        btnIniciarJuego.onClick.RemoveAllListeners();
        btnIniciarJuego.onClick.AddListener(() => {
            if (audioManager != null) audioManager.PlaySFX(audioManager.button);
            IrAInstrucciones();
        });

        btnSeleccionarNivel.onClick.RemoveAllListeners();
        btnSeleccionarNivel.onClick.AddListener(() => {
            if (audioManager != null) audioManager.PlaySFX(audioManager.button);
            gestor.MostrarPaneles(1); 
        });

        btnSalirJuego.onClick.RemoveAllListeners();
        btnSalirJuego.onClick.AddListener(() => {
            if (audioManager != null) audioManager.PlaySFX(audioManager.button);
            gestor.Salir();
        });

    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }
    private void IrAInstrucciones()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("instructions");
    }

    private void Awake()
    {
        GameObject audioObj = GameObject.FindGameObjectWithTag("Audio");
        if (audioObj != null)
        {
            audioManager = audioObj.GetComponent<AudioManagerMenu>();
        }
    }
}