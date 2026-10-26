using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class MainMenuLauncher : MonoBehaviourPunCallbacks
{
    public TMP_InputField usernameInput;
    public TMP_Text buttonText;
    
    private string[] levels = { "SceneMultiplayer1", "escena2", "escena3" };

    public void OnClickConnect()
    {
        if (usernameInput.text.Length >= 1)
        {
            PhotonNetwork.NickName = usernameInput.text;
            PlayerPrefs.SetString("PlayerName", usernameInput.text);
            buttonText.text = "Conectando...";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        PhotonNetwork.CreateRoom(null, new Photon.Realtime.RoomOptions { MaxPlayers = 10 });
    }

    public override void OnJoinedRoom()
    {
        StartCoroutine(WaitForPlayers());
    }

    private IEnumerator WaitForPlayers()
    {
        while (PhotonNetwork.CurrentRoom.PlayerCount < 1)
        {
            buttonText.text = $"Esperando jugadores... {PhotonNetwork.CurrentRoom.PlayerCount}/10";
            yield return new WaitForSeconds(1f);
        }

        string randomScene = levels[Random.Range(0, levels.Length)];
        PhotonNetwork.LoadLevel(randomScene);
    }
}
