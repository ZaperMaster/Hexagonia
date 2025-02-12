using System.Collections;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
<<<<<<< HEAD
    public float initialDestroyDelay = 2f;  // Tiempo inicial en segundos antes de destruir el objeto
    public float minDestroyDelay = 0.7f; // Tiempo mínimo de destrucción
    public float speedDecreaseDuration = 120f; // Tiempo en segundos para llegar al mínimo (más lento)
    public Color destroyColor = Color.red; // Color al tocar al jugador
    private static float currentDestroyDelay;
    private static float startTime;
=======
<<<<<<< HEAD
    public float destroyDelay = 0.2f;  // Tiempo en segundos antes de destruir el objeto
    public Color destroyColor = Color.red; // Color al tocar al jugador
=======
    public float initialDestroyDelay = 2f;  // Tiempo inicial en segundos antes de destruir el objeto
    public float minDestroyDelay = 0.7f; // Tiempo mínimo de destrucción
    public float speedDecreaseDuration = 180f; // Tiempo en segundos para llegar al mínimo (más lento)
    public Color destroyColor = Color.red; // Color al tocar al jugador
    private static float currentDestroyDelay;
    private static float startTime;
>>>>>>> bf18c930 (First commit)
>>>>>>> 364e33dd1f5d98446bbeda5cd3a4a07909ded996

    private void Start()
    {
        // Cambiar el tag del objeto a "Superficie"
        gameObject.tag = "Superficie";
<<<<<<< HEAD
=======
<<<<<<< HEAD
=======
>>>>>>> 364e33dd1f5d98446bbeda5cd3a4a07909ded996
        
        // Inicializar el tiempo de destrucción si es la primera vez
        if (currentDestroyDelay == 0)
        {
            currentDestroyDelay = initialDestroyDelay;
            startTime = Time.time;
        }
<<<<<<< HEAD
=======
>>>>>>> bf18c930 (First commit)
>>>>>>> 364e33dd1f5d98446bbeda5cd3a4a07909ded996
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Transform parentPrefab = transform.parent; // Obtener el prefab padre
            if (parentPrefab != null)
            {
                StartCoroutine(DestroyPrefabAfterDelay(parentPrefab.gameObject));
            }
            else
            {
                StartCoroutine(DestroyPrefabAfterDelay(gameObject));
            }
        }
    }

    private IEnumerator DestroyPrefabAfterDelay(GameObject prefab)
    {
        Renderer[] renderers = prefab.GetComponentsInChildren<Renderer>();

        // Cambiar el color de todos los renderers a rojo
        foreach (Renderer rend in renderers)
        {
            if (rend != null && rend.material.HasProperty("_Color"))
            {
                rend.material.color = destroyColor;
            }
        }

        // Esperar el tiempo antes de la destrucción
<<<<<<< HEAD
=======
<<<<<<< HEAD
        yield return new WaitForSeconds(destroyDelay);

        Destroy(prefab);  // Destruir el objeto completo
=======
>>>>>>> 364e33dd1f5d98446bbeda5cd3a4a07909ded996
        yield return new WaitForSeconds(currentDestroyDelay);

        Destroy(prefab);  // Destruir el objeto completo

        // Reducir el tiempo de destrucción progresivamente a lo largo de 3 minutos (más lento)
        float elapsedTime = Time.time - startTime;
        float t = Mathf.Clamp01(elapsedTime / speedDecreaseDuration);
        currentDestroyDelay = Mathf.Lerp(initialDestroyDelay, minDestroyDelay, t);
<<<<<<< HEAD
=======
>>>>>>> bf18c930 (First commit)
>>>>>>> 364e33dd1f5d98446bbeda5cd3a4a07909ded996
    }
}
