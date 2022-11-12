using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;
/// <summary>
/// Se encarga de establecer la coneecion al entrar a la escena
/// </summary>
public partial class Partida : NetworkBehaviour
{
    public bool haComenzadoLaPartida = false;
    public static Partida instance;
    private void Start()
    {
        if (GameManager.instance.estaSiendoServer) NetworkManager.Singleton.StartHost();
        else NetworkManager.Singleton.StartClient();
        if (!IsOwner) return;
        if(IsHost)
        StartCoroutine(EsperarPorLosDemas());
    }
}
/// <summary>
/// el inicio de partida
/// </summary>
public partial class Partida : NetworkBehaviour
{
    public List<GameObject> esferasEnEscena, posiciones, posicionesTeamBlue, posicionesTeamRed;
    public GameObject prefabEsferaAural;
    IEnumerator EsperarPorLosDemas()
    {
        while (NetworkManager.Singleton.ConnectedClientsList.Count < 2) yield return new WaitForEndOfFrame();
        Debug.Log("6 clientes conectados, comenzando partida");
        haComenzadoLaPartida = true;
        GenerarEsferasAurales();
        PosicionarJugadoresClientRpc();
        
        yield break;
    }
    //instanciar 6 esferas aurales, a cada una a�adir la clase seleccionada del game manager
    public void GenerarEsferasAurales()
    {
        var arrEsferas = new List<GameObject>();
        foreach (var item in posiciones)
        {
            var obj = Instantiate(prefabEsferaAural, item.transform);
            obj.GetComponent<EsferaAural>().personajeEsfera = GameManager.instance.personajeSeleccionadoEnLobby;
            obj.GetComponent<NetworkObject>().Spawn();
            esferasEnEscena.Add(obj);
        }
    }
    [ClientRpc]
    public void PosicionarJugadoresClientRpc()
    {
        UISystem.uISystem.ShowMessajeUI("Comienza el juego!");
        var q = GameObject.FindObjectsOfType<FirstPersonMovement>().ToList();
        q.ForEach(z => Debug.Log(z.tag));
    }
}





