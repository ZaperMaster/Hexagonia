using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour
{
    public float destroyTime = 5f; // Tiempo en segundos antes de destruir el objeto
    public Text countdownText; // Referencia al UI Text para la cuenta regresiva
    private float timeRemaining;

    void Start()
    {
        timeRemaining = destroyTime;
        StartCoroutine(CountdownAndDestroy());
    }

    IEnumerator CountdownAndDestroy()
    {
        while (timeRemaining > 0)
        {
            DisablePlayerMovement(true); // Desactivar movimiento de los jugadores en cada iteración
            
            if (countdownText != null)
            {
                countdownText.text = "LA PARTIDA EMPIEZA " + Mathf.Ceil(timeRemaining);
            }
            yield return new WaitForSeconds(0.1f);
            timeRemaining -= 0.1f;
        }

        if (countdownText != null)
        {
            countdownText.text = "A JUGAR";
            yield return new WaitForSeconds(0.5f); // Pequeña pausa antes de ocultarlo
            countdownText.gameObject.SetActive(false); // Opción: Ocultar el texto
        }
        
        DisablePlayerMovement(false); // Reactivar movimiento de los jugadores después de los 5 segundos
        Destroy(gameObject); // Destruye el prefab
    }

    void DisablePlayerMovement(bool disable)
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player.TryGetComponent(out MonoBehaviour movementScript))
            {
                movementScript.enabled = !disable;
            }
        }
    }
}
