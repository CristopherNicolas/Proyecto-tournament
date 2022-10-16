using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using Unity.Netcode;
using System.Threading.Tasks;
public class MenuPrincipal : MonoBehaviour
{
    public Button BuscarPartida;

    public void IrLobby() => SceneManager.LoadScene(1);
    public void IrLobbyComoServer()
    {
        GameManager.instance.estaSiendoServer = true;
        SceneManager.LoadScene(1);
    }
}
