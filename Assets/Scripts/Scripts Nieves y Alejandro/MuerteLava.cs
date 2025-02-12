using UnityEngine;
using UnityEngine.UI; // Para manejar UI
using System.Collections; // Para usar corrutinas

public class DestruirAlContacto : MonoBehaviour
{
    public GameObject deathImage; // Imagen de muerte en UI
    public AudioSource deathSound; // Audio de muerte
    public float fadeDuration = 1.5f; // Duración del fade-in

    private void Start()
    {
        if (deathImage != null)
        {
            deathImage.SetActive(false); // Se asegura de que inicie desactivada
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject objeto = collision.gameObject;

        if (objeto.GetComponent<Rigidbody>() != null && objeto.CompareTag("Player")) // Solo afecta a "Player"
        {
            if (deathImage != null) 
            {
                deathImage.SetActive(true); // Activa la imagen de muerte
                StartCoroutine(FadeIn()); // Efecto fade-in
            }

            if (deathSound != null) 
            {
                deathSound.Play(); // Reproduce el sonido de muerte
            }

            Destroy(objeto); // Destruye al Player
        }
    }

    IEnumerator FadeIn()
    {
        Image img = deathImage.GetComponent<Image>();
        Color color = img.color;
        float elapsedTime = 0;

        while (elapsedTime < fadeDuration)
        {
            color.a = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            img.color = color;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        color.a = 1;
        img.color = color;
    }
}
