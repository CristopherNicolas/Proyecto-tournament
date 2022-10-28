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
    public bool haComenzadoLaPartida=false; 
    public static Partida instance;
    public List<EsferaAural> esferasEnEscena;
    private void Start()
    {
        if (GameManager.instance.estaSiendoServer) ConectarHost();
        else ConectarCliente();
        StartCoroutine(EsperarPorLosDemas());
    }
    public void ConectarCliente() => NetworkManager.Singleton.StartClient();
    public void ConectarHost() => NetworkManager.Singleton.StartHost();
    public void ConectarServer() => NetworkManager.Singleton.StartServer();

}
/// <summary>
/// se encarga de Comenzar la partida cuando los jugadores sean 6
/// </summary>
public partial class Partida : NetworkBehaviour
{
    public void DesactivarAudioListeners()
    {
        
    }
    IEnumerator EsperarPorLosDemas()
    {
        if(!IsHost) yield break;
        while (NetworkManager.Singleton.ConnectedClientsList.Count < 6) yield return new WaitForEndOfFrame();
                Debug.Log("6 clientes conectados, comenzando partida");
            yield break;
    }
}





