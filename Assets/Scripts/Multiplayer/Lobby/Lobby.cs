using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using UnityEngine.UI;
using DG.Tweening;
using System.Linq;
using TMPro;
public class Lobby : NetworkBehaviour
{
    
    public bool estaSeleccionandoPersonaje=true;
    public static Lobby instance;
    bool isServer;
    //seleccionar personaje
    private IEnumerator Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
        yield return new WaitUntil(() => !estaSeleccionandoPersonaje);
        Debug.Log($"ha seleccionado personaje: {GameManager.instance.personajeSeleccionadoEnLobby.name}");
        StartCoroutine(Coneccion());
        yield break;
    }
    //establecer coneccion
    public IEnumerator Coneccion()
    {
        //ir a la escena index=3
        SceneManager.LoadScene(2);
        if (GameManager.instance.estaSiendoServer) ConectarServer();
        else ConectarCliente();
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if(IsHost)
            print("clientes conectados ="+NetworkManager.Singleton.ConnectedClients.Count);
        }

        //yield break;

    }
    //mostrar pantalla de loby
    // cuando los 6 integrantes esten conectados, cerrar la pantalla de carga
    //comenzar juego


    public void ConectarCliente() => NetworkManager.Singleton.StartClient();
    public void ConectarHost() => NetworkManager.Singleton.StartHost();
    public void ConectarServer() => NetworkManager.Singleton.StartServer();
}
