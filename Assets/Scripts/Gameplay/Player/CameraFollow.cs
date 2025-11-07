using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;  // Asigná el Player desde el Inspector
    public float smoothSpeed = 0.125f;
    public Vector3 offset;    // Si querés ajustar la posición de la cámara respecto al Player

    void LateUpdate()
    {
        if (player == null) return;

        Vector3 desiredPosition = player.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, transform.position.z);
    }
}
