using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plusAtaque : Item
{

    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
         //   personaje.damage *= moreAttack;
        }
    }

}
