using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class Launcher : MonoBehaviourPunCallbacks
{
    public PhotonView playerPrefab;
    public Transform spawnPoint;

    void Start()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Conectando a Photon...");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("✅ Conectado al Master Server, intentando unirse a una sala...");
        PhotonNetwork.JoinRandomOrCreateRoom();
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("✅ Se ha unido a la sala correctamente.");
        InstanciarJugador();
    }

    void InstanciarJugador()
    {
        // 🔹 Buscar cualquier jugador existente antes de instanciar uno nuevo
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            PhotonView view = player.GetComponent<PhotonView>();
            if (view != null && view.IsMine)
            {
                Debug.Log("🗑 Eliminando jugador antiguo antes de instanciar uno nuevo.");
                PhotonNetwork.Destroy(player);
            }
        }

        Debug.Log("🎮 Instanciando nuevo jugador...");
        GameObject nuevoJugador = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, spawnPoint.rotation);
        PhotonNetwork.LocalPlayer.TagObject = nuevoJugador;

        PhotonView nuevoView = nuevoJugador.GetComponent<PhotonView>();
        if (nuevoView != null)
        {
            Debug.Log("✅ PhotonView encontrado, asignando nombre...");
            nuevoView.RPC("SetNameText", RpcTarget.AllBuffered, PlayerPrefs.GetString("PlayerName"));
        }
        else
        {
            Debug.LogError("⚠️ ERROR: No se encontró PhotonView en el jugador.");
        }
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (PhotonNetwork.InRoom)
        {
            InstanciarJugador(); // 🔹 Ahora instanciamos un jugador completamente nuevo
        }
    }
}
