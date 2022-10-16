using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;
using System.Linq;
public class SeleccionPersonaje : MonoBehaviour
{
    public string nombreASeleccionar;
    public List<GameObject> personajeList = new List<GameObject>();
    public void CambiarPersonajeAElegir(string nombre) => nombreASeleccionar = nombre;
    public void AceptarPersonaje()=>
    GameManager.instance.personajeSeleccionadoEnLobby = ObtenerClaseSeleccionada(nombreASeleccionar);
    public GameObject  ObtenerClaseSeleccionada(string nombre)
    {

        var q = from GameObject g in personajeList
                where g.GetComponent<Personaje>().nombrePersonaje ==nombre
                select g;

        Debug.Log($"{q.First()}");
        Lobby.instance.estaSeleccionandoPersonaje = false;
        return q.First();
    }
}
