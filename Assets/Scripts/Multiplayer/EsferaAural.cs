using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

public class EsferaAural : NetworkBehaviour
{
    public Personaje personajeEsfera;
    public GameObject tanque, dps, soporte,colision;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FirstPersonMovement>() != null)
        {
            if (IsOwner)
            {
                int i=0; if (personajeEsfera == tanque.GetComponent<Tanque>()) i = 0;
                else if (personajeEsfera == tanque.GetComponent<soporte>()) i = 1;
                //destruir al collider para
                colision = other.gameObject;
                DestroyPlayerServerRpc();
                InstantiatePersonajeServerRpc(i,other.GetComponent<NetworkObject>().OwnerClientId, other.CompareTag("red"));
                DestroyServerRPC();
            }
        }
    }
    [ServerRpc] void DestroyPlayerServerRpc()
    {
        colision.GetComponent<NetworkObject>().Despawn();
        Destroy(colision);
    }
    [ServerRpc]void InstantiatePersonajeServerRpc(int i,ulong ownerId,bool isRedTeam)
    {
        GameObject tmp;
        switch (i)
        {
            case 0: tmp = Instantiate(tanque) ;break;
            case 1: tmp = Instantiate(soporte) ;break;
            default: print("error en InstantiatePersonaje"); return;
        }
        tmp.GetComponent<NetworkObject>().SpawnWithOwnership(ownerId);
        tmp.transform.tag = isRedTeam ? "red" : "blue";
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
