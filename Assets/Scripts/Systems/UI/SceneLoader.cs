using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneLoader : MonoBehaviour
{
    [Header("Configuración")]
    public string cargarEscena;

    [Header("Referencias UI")]
    public GameObject pantallaDeCarga;
    public Slider barraDeProgreso;

    [Header("Tiempo")]
    [Tooltip("Tiempo mínimo que durará la pantalla de carga")]
    public float duracionMinima = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerInventory inventory = other.GetComponent<PlayerInventory>();

            // Verificamos la llave
            if (inventory != null && inventory.hasKey)
            {
                StartCoroutine(CargarNivelAsync());
            }
            else
            {
                Debug.Log("No podés pasar, te falta la llave 🔒");
            }
        }
    }

    IEnumerator CargarNivelAsync()
    {
        // 1. Activar pantalla
        if (pantallaDeCarga != null) pantallaDeCarga.SetActive(true);
        Time.timeScale = 1f;

        // 2. Iniciar carga asíncrona pero FRENARLA
        AsyncOperation operacion = SceneManager.LoadSceneAsync(cargarEscena);

        if (operacion == null) yield break; 

        operacion.allowSceneActivation = false; 

        float tiempoTranscurrido = 0f;

        // 3. Bucle de espera
        while (!operacion.isDone)
        {
            tiempoTranscurrido += Time.unscaledDeltaTime;

            // --- TRUCO MATEMÁTICO ---
            // A. Progreso real de Unity (carga del disco)
            float progresoReal = Mathf.Clamp01(operacion.progress / 0.9f);

            // B. Progreso falso por tiempo (0% a 100% en 3 segundos)
            float progresoTiempo = Mathf.Clamp01(tiempoTranscurrido / duracionMinima);

            // Usamos el MENOR de los dos.
            // Si el nivel carga instantáneo, la barra respetará los 3 segundos.
            // Si el nivel tarda 10 segundos, la barra esperará a la carga real.
            if (barraDeProgreso != null)
            {
                barraDeProgreso.value = Mathf.Min(progresoReal, progresoTiempo);
            }

            // 4. Condición Final:
            // ¿Ya cargó Unity? (progress >= 0.9)  Y  ¿Ya pasaron los 3 seg?
            if (operacion.progress >= 0.9f && tiempoTranscurrido >= duracionMinima)
            {
                operacion.allowSceneActivation = true; // ¡Ahora sí, pasa!
            }

            yield return null;
        }
    }
}