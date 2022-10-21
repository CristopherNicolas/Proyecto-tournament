using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Linq;

public class Partida : MonoBehaviour
{
    public static Partida instance;
    public List<EsferaAural> esferasEnEscena;
    private void Start()
    {
        instance ??= this;
        //puede que se instancien mas prefabs a causa de que start se esta ejecutando
        // cada vez que un cliennte se une, en cada cliente.
        var chara =Instantiate( GameManager.instance.personajeSeleccionadoEnLobby);
        chara.GetComponent<NetworkObject>().Spawn();
        if (chara.GetComponent<NetworkObject>().IsOwner == true)
        {
            //remover clase y añadirla a una esfera aural vacia.
            var q = from e in Partida.instance.esferasEnEscena
                    where e.personajeEsfera == null
                    select e;
            esferasEnEscena.First().personajeEsfera = GameManager.instance.personajeSeleccionadoEnLobby.GetComponent<Personaje>();
            Destroy(chara.GetComponent<Personaje>());
        }
            if (chara.GetComponent<NetworkObject>().IsOwner==false)
        {
            chara.transform.GetChild(0).GetComponent<Camera>().enabled=false;
        }

     
    }
}
