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
        gameObject.tag = OwnerClientId > 2 ? "red" : "blue";
        Debug.Log($"Tag owner id: {OwnerClientId}, tag ={transform.tag}");
        CambiarColorDistintivoClient();
    }
   
     void CambiarColorDistintivoClient ()
    {
        if (!IsOwner) return;
        UISystem.uISystem.teamDistintive.color = OwnerClientId > 2 ? Color.red : Color.blue;
    }

}
