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
    NetworkVariable<bool> estaEsperandoPorLosDemas,estaListoParaComenzar;
    bool estaSeleccionando=true;
    TMP_Text txt;
    public RectTransform SeleccionDePersonajePanel, pantallaDeCarga;
    void Awake() => txt = GameObject.Find("conceccion").GetComponent<TMP_Text>();
    public IEnumerator Start()
    {
        //fase de seleccion
        StartLobby();
        yield return new WaitUntil(() => !estaSeleccionando);
        SeleccionDePersonajePanel.DOScale(Vector3.zero, .5f);         
        yield break;
    }
    public async void StartLobby()
    {
        estaEsperandoPorLosDemas.Value = true;
        while (estaEsperandoPorLosDemas.Value)
        {
            Debug.Log(NetworkManager.Singleton.ConnectedClientsList.Count);
            await Task.Delay(100);
            if (NetworkManager.ConnectedClients.Count == 6)
                StartCoroutine(ComenzarPartida());
            else txt.text = $"espeando, {NetworkManager.ConnectedClients.Count} clientes conectados";
        }    
    }
    public bool escenaCargada= false;
    AsyncOperation ao;
    private  IEnumerator ComenzarPartida ()
    {
        //mostrar pantalla de carga mientras se carga la escena de forma ascincrona.
        ao = SceneManager.LoadSceneAsync(2, LoadSceneMode.Single);
        while (!ao.isDone)
        {
            if(ao.progress>=1)
            {
                ao.allowSceneActivation=true;
                 //establecerse como server.
            }
            yield return null;
        }
        escenaCargada = ao.isDone;
        estaListoParaComenzar.Value = escenaCargada;
        yield break;
    }


    public void ConectarCliente() => NetworkManager.Singleton.StartClient();
    public void ConectarHost() => NetworkManager.Singleton.StartHost();
    public void ConectarServer() => NetworkManager.Singleton.StartServer();
}
