using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class Partida : MonoBehaviour
{
    private void Start()
    {
        var chara = Instantiate(GameManager.instance.personajeSeleccionadoEnLobby);
        chara.GetComponent<NetworkObject>().Spawn();
    }
}
