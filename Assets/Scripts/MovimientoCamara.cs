using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimientoCamara : MonoBehaviour
{
    public Transform player;
    public Vector3 offset = new Vector3(0, 20, -15);
    public float smoothSpeed = 5f;
    public float zoomSpeed = 2f;
    public float minZoom = 10f;
    public float maxZoom = 30f;

    void Start()
    {
        StartCoroutine(FindPlayer());
    }

    IEnumerator FindPlayer()
    {
        while (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                Debug.Log("✅ Jugador encontrado y cámara asignada.");
            }
            else
            {
                Debug.LogWarning("⏳ Buscando jugador...");
                yield return new WaitForSeconds(0.5f);
            }
        }
    }

    void LateUpdate()
    {
        if (player != null)
        {
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            offset.y -= scroll * zoomSpeed;
            offset.z += scroll * zoomSpeed;

            offset.y = Mathf.Clamp(offset.y, minZoom, maxZoom);
            offset.z = Mathf.Clamp(offset.z, -maxZoom, -minZoom);

            Vector3 desiredPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            transform.LookAt(player.position + Vector3.up * 2.5f);
        }
    }
}
