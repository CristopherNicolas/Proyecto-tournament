using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Personaje personajeActual;
    public GameObject personajeSeleccionadoEnLobby;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }
}
