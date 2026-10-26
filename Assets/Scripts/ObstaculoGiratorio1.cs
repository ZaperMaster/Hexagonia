using UnityEngine;

public class Rotation360Gira : MonoBehaviour
{
    public float rotateSpeed = 60f; // Grados/segundo (a 60, da una vuelta completa en 6s)

    void Update()
    {
        // Rota en torno a su eje Y local (vertical) a la velocidad indicada
        // Space.Self = sobre su propio eje
        transform.Rotate(rotateSpeed * Time.deltaTime, 0f , 0f, Space.Self);
    }
}
