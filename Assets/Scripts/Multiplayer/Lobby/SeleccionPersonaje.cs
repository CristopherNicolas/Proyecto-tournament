using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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
        var q= personajeList.Where
            (p => p.GetComponent<Personaje>()
            .nombrePersonaje == nombre).First();
        Lobby.instance.estaSeleccionandoPersonaje = false;
        return q;
    }
}
