using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;
using System.Threading.Tasks;

public class EsferaAural : NetworkBehaviour
{
    public Personaje personajeEsfera;    
    private void OnCollisionEnter(Collision collision)
    {
        //if (!IsOwner) return;
        if (collision.gameObject.GetComponent<FirstPersonMovement>()
            && ! collision.transform.GetComponent<Personaje>())
        {
            //añadir personajeEsfera al fps con el que coliciona
            Destroy(gameObject);
            switch (personajeEsfera)
            {
                case DPS: collision.gameObject.AddComponent<DPS>(); break;
                case soporte: collision.gameObject.AddComponent<soporte>(); break;
                case Tanque: collision.gameObject.AddComponent<Tanque>(); break;
                default:
                    break;
            }
        }
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
