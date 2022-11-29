using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;
using Unity.Collections;
using System.Linq;
using TMPro;
/// <summary>
/// Se encarga de establecer la coneecion al entrar a la escena
/// </summary>
public partial class Partida : NetworkBehaviour
{
    public bool haComenzadoLaPartida = false;
    public static Partida instance;
    private void Start()
    {
        if (GameManager.instance.estaSiendoServer) NetworkManager.Singleton.StartHost();
        else NetworkManager.Singleton.StartClient();
        if (!IsOwner) return;
        if(IsHost)
        StartCoroutine(EsperarPorLosDemas());

    }
}
/// <summary>
/// el inicio de partida
/// </summary>
public partial class Partida : NetworkBehaviour
{
    public List<GameObject> esferasEnEscena, posiciones, posicionesTeamBlue, posicionesTeamRed;
    public GameObject prefabEsferaAural;
    IEnumerator EsperarPorLosDemas()
    {
        while (NetworkManager.Singleton.ConnectedClientsList.Count < 2) yield return new WaitForEndOfFrame();
        Debug.Log("6 clientes conectados, comenzando partida");
        haComenzadoLaPartida = true;
        GenerarEsferasAurales();
        PosicionarJugadorClientRpc();
        StartCoroutine(GameFlow());
        yield break;
    }
    //instanciar 6 esferas aurales, a cada una a�adir la clase seleccionada del game manager
    public void GenerarEsferasAurales()
    {
        var arrEsferas = new List<GameObject>();
        foreach (var item in posiciones)
        {
            var obj = Instantiate(prefabEsferaAural, item.transform);
            obj.GetComponent<EsferaAural>().personajeEsfera = GameManager.instance.personajeSeleccionadoEnLobby;
            obj.GetComponent<NetworkObject>().Spawn();
            esferasEnEscena.Add(obj);
        }
    }
    [ClientRpc]
    public void PosicionarJugadorClientRpc()
    {
        UISystem.uISystem.ShowMessajeUIClientRpc ("Comienza el juego!");
        var q = GameObject.FindObjectsOfType<FirstPersonMovement>().ToList();
       var player = q.Where(o => o.GetComponent<NetworkObject>().IsOwner);
        if(IsHost||IsServer)
        {

            player.Where(o => o.IsOwnedByServer).First().transform.position =  tag  is "red" ?
            posicionesTeamRed[Random.Range(0, posicionesTeamRed.Count)].transform.position
            : posicionesTeamBlue[Random.Range(0, posicionesTeamBlue.Count)].transform.position;
            return;
        }
        player.First().transform.position = player.First().tag is "red" ? 
            posicionesTeamRed[Random.Range(0, posicionesTeamRed.Count)].transform.position
            : posicionesTeamBlue[Random.Range(0, posicionesTeamBlue.Count)].transform.position;
    }
    
}
/// <summary>
/// flujo de partida
/// </summary>
public partial class Partida : NetworkBehaviour
{
    NetworkVariable<int> ticketsRed = new NetworkVariable<int>(30, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    NetworkVariable<int> ticketsBlue = new NetworkVariable<int>(30, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public TMP_Text textTicketsBlue,textTicketsRed;


    IEnumerator GameFlow()
    {
        int blueRondasGanadas=0,RedRondasGanadas = 0;
        int rondas = 2;
        
        while (rondas > 0)
        {
            //InstaciarBanderasServerRpc();
            ActualizarMarcadorClientRpc(ticketsBlue.Value, ticketsRed.Value);
          yield return new WaitUntil(() => ticketsBlue.Value == 0 || ticketsRed.Value == 0||
          banderasAzulesCapturadas==2|| banderasRojasCapturadas==2);
            if(ticketsBlue.Value == 0)
            {
                RedRondasGanadas++;
            }
             else if(ticketsRed.Value == 0)
             {
                blueRondasGanadas++;
             }
            ticketsBlue.Value = 30;
           ticketsRed.Value = 30;
         SwapTeam();
         rondas--;

        }
        string teamGanador;
        if (blueRondasGanadas > RedRondasGanadas) teamGanador = "blue";
        else if (blueRondasGanadas < RedRondasGanadas) teamGanador = "red";
        else teamGanador = "empate";
        UISystem.uISystem.ShowMessajeUI($"juego terminado gana el equipo: {teamGanador}");
        yield break;
    }
            
            public void SwapTeam()
            {
                var teamRed = GameObject.FindGameObjectsWithTag("red").ToList();
                var teamBlue = GameObject.FindGameObjectsWithTag("blue").ToList();

                teamBlue.ForEach(p => p.transform.tag = "red");
                teamRed.ForEach(p => p.transform.tag = "blue");
            }

    [ClientRpc]
    public void  StartGameNotificationClientRpc()
    {
        UISystem.uISystem.ShowMessajeUI("Start Game!");
    }
    [ClientRpc]
    public void ActualizarMarcadorClientRpc(int blueT,int redT)
    {
        textTicketsBlue.text = blueT.ToString();
        textTicketsRed.text = redT.ToString();
    }
    [ServerRpc]
    public void QuitarTicketServerRpc(FixedString128Bytes teamAQuitar)
    {
        if (teamAQuitar.ToString() is "blue") ticketsBlue.Value -= 1;
        else if (teamAQuitar.ToString() is "red") ticketsRed.Value -= 1;
        else print("error en quitar tickets");
    }
    [ServerRpc]
    protected void InstaciarBanderasServerRpc()
    {
        //limpiar escena
        var banderas = GameObject.FindObjectsOfType<Bandera>().ToList();
        banderas.ForEach(b => b.GetComponent<NetworkObject>().Despawn());
        banderas.ForEach(b => Destroy(b));

        // instanciar banderas
        Vector3 posBanderaRed = new Vector3(), posBanderablue =  new Vector3();
        var banderaRed = Instantiate(prefabBandera, posBanderaRed, Quaternion.identity);
        var banderablue = Instantiate(prefabBandera, posBanderablue, Quaternion.identity);

        banderaRed.GetComponent<NetworkObject>().Spawn();
        banderablue.GetComponent<NetworkObject>().Spawn();

    }
    public GameObject prefabBandera;
}

/// <summary>
/// sistema de captura la bandera
/// </summary>

public partial class Partida : NetworkBehaviour
{
    public short banderasAzulesCapturadas = 0, banderasRojasCapturadas=0;
    //hacer aparecer bnanderas, considerar que al robar una bandera y llevarla a tu base se agregan 
}





