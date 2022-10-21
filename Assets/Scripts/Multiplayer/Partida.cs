using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;

public class Partida : MonoBehaviour
{
    private void Start()
    {
        var chara =Instantiate( GameManager.instance.personajeSeleccionadoEnLobby);
        chara.GetComponent<NetworkObject>().Spawn();
        if(chara.GetComponent<NetworkObject>().IsOwner==false)
        {
            chara.transform.GetChild(0).GetComponent<Camera>().enabled=false;
        }

     
    }
}
