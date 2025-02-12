using System.Collections;
using UnityEngine;

public class DestroyPlayer : MonoBehaviour
{
    public float destroyDelay = 0.2f;  // Tiempo en segundos antes de destruir el objeto
    public Color destroyColor = Color.red; // Color al tocar al jugador

    private void Start()
    {
        // Cambiar el tag del objeto a "Superficie"
        gameObject.tag = "Superficie";
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
        yield return new WaitForSeconds(destroyDelay);

        Destroy(prefab);  // Destruir el objeto completo
    }
}
