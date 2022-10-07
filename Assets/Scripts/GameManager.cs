using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else Destroy(gameObject);
        //SpawnPlayers();!!!
    }
    /// <summary>
    /// ajustar lista al tener mapa
    /// </summary>
    public List<Transform> posiciones;
    public List<GameObject> personajes;

    /// <summary>
    /// este metodo debe ser llamado por el server, y debe ser llamado SOLO UNA VEZ, el cliente no debe usarlo
    /// </summary>
    public void SpawnPlayers()
    {
        for (int i = 0; i < posiciones.Count; i++)
        {
            Instantiate(personajes[0], posiciones[i]);
        }
    }
}
