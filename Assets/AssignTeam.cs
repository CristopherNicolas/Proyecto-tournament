using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Unity.Netcode;
public class AssignTeam : NetworkBehaviour
{
    private void Start()
    {
        //if (!IsOwner) return;
        gameObject.tag = OwnerClientId > 0 ? "red" : "blue";
        Debug.Log($"Tag owner id: {OwnerClientId}, tag ={transform.tag}");
    }
   
    /// <summary>
    /// cambiar el equipo del personaje
    /// </summary>
    [ClientRpc]
    public void SwapTeamClientRPC()
    {
        var teamRed = GameObject.FindGameObjectsWithTag("red").ToList();
        var teamBlue = GameObject.FindGameObjectsWithTag("blue").ToList();


        teamBlue.ForEach(p => p.transform.tag = "red");
        teamRed.ForEach(p => p.transform.tag = "blue");
    }
}
