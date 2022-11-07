using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Netcode;
public class AssignTeam : NetworkBehaviour
{
    private void Start()
    {
        //ownerClientID
        AssignTeamFunc(gameObject);
    }
    public void AssignTeamFunc(GameObject playerObject)
    {
        if (!IsOwner) return;
        // cada 3 jugadores, que se unan crear un equipo
        playerObject.transform.tag = NetworkObject.OwnerClientId > 2 ? "red" : "blue";
        Debug.Log(NetworkObject.OwnerClientId);
    }
    [ClientRpc]
    public void SwapTeamClientRPC()
    {
        var teamRed = GameObject.FindGameObjectsWithTag("red").ToList();
        var teamBlue = GameObject.FindGameObjectsWithTag("blue").ToList();

        teamBlue.ForEach(p => p.transform.tag = "red");
        teamRed.ForEach(p => p.transform.tag = "blue");
    }
}
