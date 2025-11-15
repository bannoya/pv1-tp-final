using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem; // ¡¡AÑADIR ESTA LÍNEA!!

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject player;

    private bool isPaused = false;
    private PlayerInput playerInput;

    void Start()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (player != null)
        {
            playerInput = player.GetComponent<PlayerInput>();
        }
    }

    void Update()
    {
        // ¡ESTA ES LA LÍNEA QUE CAMBIAMOS!
        // Revisa si el teclado existe Y si se presionó "escape" en este frame
        if (Keyboard.current != null && Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        if (playerInput != null)
        {
            playerInput.enabled = false;
        }
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (playerInput != null)
        {
            playerInput.enabled = true;
        }
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}