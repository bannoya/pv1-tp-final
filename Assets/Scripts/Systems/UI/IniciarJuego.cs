using UnityEngine;
using UnityEngine.UI;

public class IniciarJuego : UIPanel
{
    public override void Mostrar()
    {
        gameObject.SetActive(true);
    }

    public override void Ocultar()
    {
        gameObject.SetActive(false);
    }

}
