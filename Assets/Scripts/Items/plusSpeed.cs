using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plusSpeed : Item
{
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<FirstPersonMovement>())
        {
            var chara = other.GetComponent<FirstPersonMovement>();
            chara.runSpeed += 2;
            Debug.Log(chara);

        }
        base.OnTriggerEnter(other);
    }
}
