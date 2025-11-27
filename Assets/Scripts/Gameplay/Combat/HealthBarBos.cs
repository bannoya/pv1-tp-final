using UnityEngine;

public class HealthBarBos : MonoBehaviour
{
    public RectTransform healthBar;

    void Start()
    {
        // Tamaño fijo de la barra
        healthBar.sizeDelta = new Vector2(700, 40);

        // Anclar a la ESQUINA INFERIOR IZQUIERDA
        healthBar.anchorMin = new Vector2(0f, 0f);
        healthBar.anchorMax = new Vector2(0f, 0f);

        // Posición desde la esquina inferior izquierda
        healthBar.anchoredPosition = new Vector2(900f, 1000f);
    }
}
