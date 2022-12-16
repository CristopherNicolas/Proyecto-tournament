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
        if (instance is null) instance = this;

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
        while (NetworkManager.Singleton.ConnectedClientsList.Count < 6) yield return new WaitForEndOfFrame();
        Debug.Log("6 clientes conectados, comenzando partida");
        haComenzadoLaPartida = true;
        yield return new WaitForSecondsRealtime(1);
        GenerarEsferasAurales();
        PosicionarJugadorClientRpc();
        int blueRondasGanadas = 0, RedRondasGanadas = 0;
        int rondas = 2;
        UISystem.uISystem.ShowMessajeUIClientRpc("Comienza el juego!");
        while (rondas > 0)
        {
            InstanciarItemServerRpc();
            ActualizarMarcadorClientRpc(ticketsBlue.Value, ticketsRed.Value);
            yield return new WaitUntil(() => ticketsBlue.Value == 0 || ticketsRed.Value == 0 ||
             banderasAzulesCapturadas.Value == 2 || banderasRojasCapturadas.Value == 2);
            if (ticketsBlue.Value == 0 || banderasAzulesCapturadas.Value == 2)
                RedRondasGanadas++;
            else if (ticketsRed.Value == 0 || banderasRojasCapturadas.Value == 2)
                blueRondasGanadas++;
            banderasAzulesCapturadas.Value = 0;
            banderasRojasCapturadas.Value = 0;
            ticketsBlue.Value = 30;
            ticketsRed.Value = 30;
            UISystem.uISystem.ShowMessajeUIClientRpc("Ronda terminada, cambiando equipo");
            PosicionarJugadorClientRpc();
            rondas--;
        }
        string teamGanador;
        if (blueRondasGanadas > RedRondasGanadas) teamGanador = "blue";
        else if (blueRondasGanadas < RedRondasGanadas) teamGanador = "red";
        else teamGanador = "empate";
        UISystem.uISystem.ShowMessajeUI($"juego terminado gana el equipo: {teamGanador}");
        yield break;
    }
    //instanciar 6 esferas aurales, a cada una a�adir la clase seleccionada del game manager
    public void GenerarEsferasAurales()
    {
        var arrEsferas = new List<GameObject>();
        foreach (var item in posiciones)
        {
            var obj = Instantiate(prefabEsferaAural, item.transform);
            //aqui hay que añadir el personaje de cada gamemanager de cada cliente
            //obj.GetComponent<EsferaAural>().personajeEsfera = GameManager.instance.personajeSeleccionadoEnLobby;
            //solucin random
            int r = Random.Range(0, 2);
            if(r==0)obj.GetComponent<EsferaAural>().personajeEsfera = obj.GetComponent<EsferaAural>().tanque.GetComponent<Personaje>();
            else if(r==1) obj.GetComponent<EsferaAural>().personajeEsfera = obj.GetComponent<EsferaAural>().soporte.GetComponent<Personaje>();
            else if(r==2) obj.GetComponent<EsferaAural>().personajeEsfera = obj.GetComponent<EsferaAural>().dps.GetComponent<Personaje>(); ;


            obj.GetComponent<NetworkObject>().Spawn();
            esferasEnEscena.Add(obj);
        }
    }
    [ClientRpc]
    public void PosicionarJugadorClientRpc()
    {
        var q = GameObject.FindObjectsOfType<FirstPersonMovement>().ToList();
        var player = q.Where(o => o.GetComponent<NetworkObject>().IsOwner);
        if (IsHost || IsServer)
        {

            player.Where(o => o.IsOwnedByServer).First().transform.position =
                player.Where(o => o.IsOwnedByServer).First().transform.tag is "red" ?
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
   public NetworkVariable<int> banderasAzulesCapturadas = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public NetworkVariable<int> banderasRojasCapturadas = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);
    public TMP_Text textTicketsBlue,textTicketsRed;

    [ClientRpc] void ChangeDistintiveClientRpc()
    {
        var player = GameObject.FindObjectsOfType<FirstPersonMovement>().
            Where(p => p.GetComponent<NetworkObject>().OwnerClientId == NetworkManager.Singleton.LocalClientId).First();
        GameObject.Find("team distintive (ui manager)").GetComponent<Image>().color
            = player.CompareTag("red") ? Color.red : Color.blue;
    }

    [ClientRpc]
    public void  StartGameNotificationClientRpc()
    {
        UISystem.uISystem.ShowMessajeUI("Start Game!");
    }
    [ClientRpc]
    public void ActualizarMarcadorClientRpc(int blueT,int redT)
    {
        GameObject.Find("tickets team blue").GetComponent<TMP_Text>().text = blueT.ToString();
        GameObject.Find("tickets team red").GetComponent<TMP_Text>().text = blueT.ToString();
    }
    [ServerRpc]
    public void QuitaarTicketServerRpc(FixedString128Bytes teamAQuitar)
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
    public Transform[] posicionesItems,items;
    [ServerRpc]
    void InstanciarItemServerRpc()
    {
        var item = Instantiate(items[0].gameObject, posicionesItems[1].position
            ,Quaternion.Euler(-90,0,0));
        item.GetComponent<NetworkObject>().Spawn();

        var itemVelocidad = Instantiate(items[1].gameObject, posicionesItems[0].position
            , Quaternion.Euler(-90, 0, 0));
        itemVelocidad.GetComponent<NetworkObject>().Spawn();
    }
}





