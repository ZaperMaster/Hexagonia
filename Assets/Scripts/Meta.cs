using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using TMPro;

public class Meta : MonoBehaviourPunCallbacks
{
    public GameObject panelVictoria;
    public TMP_Text textoEsperandoJugadores;
    public Color metaColor = Color.green;

    private bool haLlegado = false;
    private static int jugadoresEnMeta = 0;
    private static int nivelesCompletados = 0; // 🔹 Contador de niveles completados
    private static string[] niveles = { "SceneMultiplayer1", "escena2", "escena3", "escena4" }; // 🔹 Niveles disponibles
    private PhotonView photonView;

    void Start()
    {
        photonView = GetComponent<PhotonView>();

        if (photonView == null)
        {
            Debug.LogError("❌ ERROR: No se encontró PhotonView en Meta.");
        }

        if (panelVictoria != null)
        {
            panelVictoria.SetActive(false);
        }

        if (textoEsperandoJugadores != null)
        {
            textoEsperandoJugadores.gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !haLlegado)
        {
            PhotonView jugadorView = other.GetComponent<PhotonView>();

            // 🔹 Aseguramos que solo se ejecute en el jugador local
            if (jugadorView != null && jugadorView.IsMine)
            {
                haLlegado = true;
                Debug.Log("✅ ¡Victoria! Has llegado a la meta.");

                if (panelVictoria != null)
                {
                    panelVictoria.SetActive(true);
                }

                if (textoEsperandoJugadores != null)
                {
                    textoEsperandoJugadores.gameObject.SetActive(true);
                    textoEsperandoJugadores.text = "Esperando jugadores...";
                }

                // 🔹 Notificar al MasterClient que este jugador llegó
                photonView.RPC("JugadorLlegoAMeta_RPC", RpcTarget.MasterClient);
            }
        }
    }

    [PunRPC]
    void JugadorLlegoAMeta_RPC()
    {
        if (!PhotonNetwork.IsMasterClient) return;

        jugadoresEnMeta++;
        Debug.Log($"🏆 Jugadores en la meta: {jugadoresEnMeta}/{PhotonNetwork.PlayerList.Length}");

        if (jugadoresEnMeta >= PhotonNetwork.PlayerList.Length)
        {
            Debug.Log("✅ Todos los jugadores han llegado. Cambiando de nivel...");
            photonView.RPC("SeleccionarNuevoNivel", RpcTarget.All);
        }
    }

    [PunRPC]
    void SeleccionarNuevoNivel()
    {
        StartCoroutine(CambiarDeNivel());
    }

    IEnumerator CambiarDeNivel()
    {
        yield return new WaitForSeconds(3f);

        if (PhotonNetwork.IsMasterClient)
        {
            jugadoresEnMeta = 0;
            nivelesCompletados++;

            if (nivelesCompletados >= 4)
            {
                Debug.Log("🏆 4 niveles completados, volviendo al menú principal.");
                PhotonNetwork.LoadLevel("MainMenu");
            }
            else
            {
                string siguienteEscena = ObtenerNivelAleatorio();
                Debug.Log("🔄 Cambiando a la siguiente escena: " + siguienteEscena);
                PhotonNetwork.LoadLevel(siguienteEscena);
            }
        }
    }

    string ObtenerNivelAleatorio()
    {
        string escenaActual = SceneManager.GetActiveScene().name;
        List<string> nivelesDisponibles = new List<string>(niveles);
        nivelesDisponibles.Remove(escenaActual); // 🔹 Evitar repetir el mismo nivel
        return nivelesDisponibles[Random.Range(0, nivelesDisponibles.Count)];
    }
}
