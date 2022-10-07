using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// base para crear cualquier item
/// </summary>
public abstract class Item : MonoBehaviour
{
    public float tiempoSpawn, tiempoActivo;
    public virtual void Efecto()
    {

    }

}
