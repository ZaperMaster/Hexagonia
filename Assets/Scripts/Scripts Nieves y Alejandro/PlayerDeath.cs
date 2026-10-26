using UnityEngine;
using UnityEngine.UI;

public class PlayerDeath : MonoBehaviour
{
    public GameObject deathMessage; // Referencia al mensaje de "HAS MUERTO"

    void Awake()
    {
        if (deathMessage != null)
        {
            deathMessage.SetActive(false); // Asegurar que el mensaje esté desactivado antes de que inicie el juego
        }
    }

    void Start()
    {
        if (deathMessage != null && deathMessage.activeSelf)
        {
            deathMessage.SetActive(false); // Forzar la desactivación si aún está activo
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión detectada con: " + collision.gameObject.name);

        if (collision.gameObject.CompareTag("Lava"))
        {
            Debug.Log("¡El jugador ha tocado la lava!");

            if (deathMessage != null)
            {
                deathMessage.SetActive(true);
                Debug.Log("Mensaje de muerte activado.");
            }
        }
    }

}
