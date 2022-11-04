using UnityEngine;
using Unity.Netcode;
/// <summary>
/// Contiene las estadisticas del equipo ademas de la asignacion de el jugador
/// </summary>
class TeamSystem : NetworkBehaviour
{
    private string colorTeam;
    private int tickets=100;    
    public void AssignTeam(GameObject playerObject)
    {
        // cada 3 jugadores, que se unan crear un equipo
         playerObject.transform.tag = NetworkObject.OwnerClientId>3 ? "red" : "blue";
    }
        

}
