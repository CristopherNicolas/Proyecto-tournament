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
    bool isServer;
    public static Lobby instance;
   
    //seleccionar personaje
    private IEnumerator Start()
    {
        instance ??= this;
        yield return new WaitUntil(() => !estaSeleccionandoPersonaje);
        Debug.Log($"ha seleccionado personaje: {GameManager.instance.personajeSeleccionadoEnLobby.name}");
        StartCoroutine(Coneccion());
        yield break;
    }
    //Ir a La escena de juego
    public IEnumerator Coneccion()
    {
        //ir a la escena index=3
        SceneManager.LoadScene(2);
        //yield return new WaitForSecondsRealtime(2);
        yield break;
    }
}
