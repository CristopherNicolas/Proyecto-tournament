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
    private void Start()
    {
        StartCoroutine(EsperarPorLosDemas());
    }
    //{
    //    if (GameManager.instance.estaSiendoServer) ConectarHost();
    //    else ConectarCliente();
    //    StartCoroutine(EsperarPorLosDemas());
    //}
    //public void ConectarCliente() => NetworkManager.Singleton.StartClient();
    //public void ConectarHost() => NetworkManager.Singleton.StartHost();
    //public void ConectarServer() => NetworkManager.Singleton.StartServer();

}
/// <summary>
/// se encarga de Comenzar la partida cuando los jugadores sean 6
/// </summary>
public partial class Partida : NetworkBehaviour
{
    public List<GameObject> esferasEnEscena,posiciones;
    public GameObject prefabEsferaAural;
    IEnumerator EsperarPorLosDemas()
    {
        if (!IsHost) yield break;
        while (NetworkManager.Singleton.ConnectedClientsList.Count < 6) yield return new WaitForEndOfFrame();
        Debug.Log("6 clientes conectados, comenzando partida");
        GenerarEsferasAurales();
        yield break;
    }
    //instanciar 6 esferas aurales, a cada una añadir la clase seleccionada del game manager
    public void GenerarEsferasAurales()
    {
        var arrEsferas = new List<GameObject>();

        for (int i = 0; i < 6; i++)
        {
            //isntanciar en un empty, osea comprobar si hay childs, si hay pasar a otra
            foreach (var item in posiciones)
            {
               var obj =  Instantiate(prefabEsferaAural, item.transform);
                // bug, deben agregarse los personajes de cada cliente
                obj.GetComponent<EsferaAural>().personajeEsfera = GameManager.instance.personajeSeleccionadoEnLobby;
                obj.GetComponent<NetworkObject>().Spawn();
                esferasEnEscena.Add(obj);
            }
        }
    }
}





