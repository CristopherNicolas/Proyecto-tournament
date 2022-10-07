using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocion : Item
{
    public float cantidadDeVida = 10;
    public Personaje personaje;
    public override void Efecto()
    {
        base.Efecto();
        personaje.vida = cantidadDeVida;
    }
}
