using UnityEngine;
using System.Collections;
using Photon.Pun;

public class CamaraInstanciaSpawn : MonoBehaviourPunCallbacks
{
    private GameObject currentPlayer;
    public MovimientoCamara camaraScript;

    void Start()
    {
        StartCoroutine(EsperarJugadorPhoton());
    }

    IEnumerator EsperarJugadorPhoton()
    {
        float waitTime = 0f;
        float maxWaitTime = 10f;

        while (currentPlayer == null && waitTime < maxWaitTime)
        {
            yield return new WaitForSeconds(0.5f);
            waitTime += 0.5f;

            foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
            {
                if (player.GetComponent<PhotonView>()?.IsMine == true)
                {
                    currentPlayer = player;
                    break;
                }
            }
        }

        if (currentPlayer != null && camaraScript != null)
        {
            camaraScript.player = currentPlayer.transform;
            Debug.Log("✅ Jugador asignado a la cámara.");
        }
        else
        {
            Debug.LogError("⚠ No se encontró el jugador en la escena después de esperar.");
        }
    }
}
