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

}
