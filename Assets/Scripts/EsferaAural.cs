using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

public class EsferaAural : NetworkBehaviour
{
    public Personaje personajeEsfera;
    public List<Vector3> posicionesVectores;
    
    private void OnCollisionEnter(Collision collision)
    {
        if (!IsOwner) return;
        if (collision.gameObject.GetComponent<FirstPersonMovement>()
            && ! collision.transform.GetComponent<Personaje>())
        {
            //añadir personajeEsfera al fps con el que coliciona
            Destroy(gameObject);
        }
    }
    private async void Start()
    {
        await Task.Delay(2100);
        GetComponent<NetworkObject>().SpawnWithOwnership(GetComponent<NetworkObject>().OwnerClientId);
    }
    public override void OnNetworkSpawn()
    {
        Debug.Log("spawn flag");
        base.OnNetworkSpawn();
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0, 4, 0));
    }
}
