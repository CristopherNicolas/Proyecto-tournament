//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
/// <summary>
/// base para crear cualquier item
/// </summary>
public abstract class Item : NetworkBehaviour
{
    [ServerRpc(RequireOwnership = false)] void DestroyServerRpc()
    {
        NetworkObject.Despawn();
        Destroy(gameObject);
    }
 
    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<FirstPersonMovement>())
        {
            DestroyServerRpc();
        }

    }
    private void Update()
    {
        transform.Rotate(new Vector3(10 * Time.deltaTime, 0, 0));
    }


}
