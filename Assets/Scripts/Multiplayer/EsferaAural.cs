using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

public class EsferaAural : NetworkBehaviour
{
    public Personaje personajeEsfera;    
    private void OnTriggerEnter(Collider other)
    {
       // if (!IsOwner) return;
        Debug.Log("triger enter");

        if(IsOwner)
        {   
            DestroyServerRPC();
        }
    }
    [ServerRpc] void DestroyServerRPC()
    {
        GetComponent<NetworkObject>().Despawn();
        Destroy(gameObject);
    }
   
    private void Update()
    {
        transform.Rotate(new Vector3(0, 4, 0));
    }
}
