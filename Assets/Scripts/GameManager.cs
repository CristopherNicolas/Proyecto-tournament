using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public Personaje personajeSeleccionadoEnLobby;
    public bool estaSiendoServer = false;
    public int numIdPlayer = 0;
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
