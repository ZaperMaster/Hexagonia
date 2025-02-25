using UnityEngine;
using UnityEngine.UI; // Para manejar UI
using System.Collections; // Para usar corrutinas

public class DestruirAlContacto : MonoBehaviour
{
    public GameObject deathImage; // Imagen de muerte en UI
    public AudioSource deathSound; // Audio de muerte
<<<<<<< HEAD
    public AudioSource backgroundMusic; // Música de fondo
=======
<<<<<<< HEAD
=======
    public AudioSource backgroundMusic; // Música de fondo
>>>>>>> bf18c930 (First commit)
>>>>>>> 364e33dd1f5d98446bbeda5cd3a4a07909ded996
    public float fadeDuration = 1.5f; // Duración del fade-in

    private void Start()
    {
        if (deathImage != null)
        {
            deathImage.SetActive(false); // Se asegura de que inicie desactivada
        }
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
>>>>>>> 364e33dd1f5d98446bbeda5cd3a4a07909ded996

        if (deathSound != null)
        {
            deathSound.Stop(); // Asegura que el sonido de muerte no se reproduzca al inicio
        }

        if (backgroundMusic != null)
        {
            backgroundMusic.Play(); // Reproduce la música de fondo al inicio
        }
<<<<<<< HEAD
=======
>>>>>>> bf18c930 (First commit)
>>>>>>> 364e33dd1f5d98446bbeda5cd3a4a07909ded996
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

<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
>>>>>>> 364e33dd1f5d98446bbeda5cd3a4a07909ded996
            if (backgroundMusic != null)
            {
                backgroundMusic.Stop(); // Detiene la música de fondo
            }

<<<<<<< HEAD
=======
>>>>>>> bf18c930 (First commit)
>>>>>>> 364e33dd1f5d98446bbeda5cd3a4a07909ded996
            if (deathSound != null) 
            {
                deathSound.Play(); // Reproduce el sonido de muerte
            }

            Destroy(objeto); // Destruye al Player
        }
    }

    IEnumerator FadeIn()
    {
<<<<<<< HEAD

=======
<<<<<<< HEAD
        Image img = deathImage.GetComponent<Image>();
=======
>>>>>>> 364e33dd1f5d98446bbeda5cd3a4a07909ded996
        if (deathImage == null)
        {
            Debug.LogError("⚠️ deathImage no está asignado en el Inspector.");
            yield break; // Sale de la corrutina sin continuar
        }

        Image img = deathImage.GetComponent<Image>();
        
        if (img == null)
        {
            Debug.LogError("⚠️ deathImage no tiene un componente Image.");
            yield break;
        }

<<<<<<< HEAD
=======
>>>>>>> bf18c930 (First commit)
>>>>>>> 364e33dd1f5d98446bbeda5cd3a4a07909ded996
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
