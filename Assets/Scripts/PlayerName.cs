using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerName : MonoBehaviourPunCallbacks
{
    public TMP_Text playerName;
    // Start is called before the first frame update
    [PunRPC]

    public void SetNameText(string name){
        Debug.Log("RPC recibido. Asignando nombre: " + name); // 🔹 Depuración
        playerName.text = name;

    }

}
