using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNavigation : MonoBehaviour
{
    // lleva al Menú Principal
    public void VolverAMenuPrincipal()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}