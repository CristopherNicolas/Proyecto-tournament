using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Threading.Tasks;
public class MenuPrincipal : MonoBehaviour
{
    Button CrearPartida, unirsePartida;
    RectTransform panelOpciones, panelCrearParida;

    private IEnumerator Start()
    {
        CacheComponents();
        yield break;
    }

    void CacheComponents()
    {
        CrearPartida = GameObject.Find("Boton crear partida").GetComponent<Button>();
        unirsePartida = GameObject.Find("Boton Opciones").GetComponent<Button>();
        panelOpciones=GameObject.Find("Panel opciones").GetComponent<RectTransform>();
       // panelCrearParida = GameObject.Find("panel opciones").GetComponent<RectTransform>();
    }
    public void CrearPartidaFunc()
    {

    }
}
