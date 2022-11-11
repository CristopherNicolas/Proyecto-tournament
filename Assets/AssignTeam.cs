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
        if (!IsOwner) return;
        gameObject.transform.tag =
      (GetComponent<NetworkObject>().OwnerClientId > 2) ? "red" : "blue";
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
