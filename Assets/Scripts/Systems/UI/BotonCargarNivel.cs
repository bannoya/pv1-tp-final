using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonCargarNivel : MonoBehaviour
{
    [Header("Escribe aqui el nombre de la escena a cargar")]
    public string nombreEscena;

    public void CargarAhora()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nombreEscena);
    }
}