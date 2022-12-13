using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocion : Item
{
    public float cantidadDeVida = 25;
    public Personaje personaje;
    public override void OnTriggerEnter(Collider other)
    {
        if(GetComponent<FirstPersonMovement>() && GetComponent<Personaje>())
        {
            personaje = GetComponent<Personaje>();
            personaje.vida +=  cantidadDeVida;
            if (personaje.vida > 100)
            {
                personaje.vida = 100;
            }    
        }
       base.OnTriggerEnter(other);
    }
    
}
