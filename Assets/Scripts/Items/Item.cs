//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// base para crear cualquier item
/// </summary>
public abstract class Item : MonoBehaviour
{
    public float tiempoSpawn = 2, tiempoActivo;
    public GameObject consumibleObject;
    public virtual void Efecto()
    {
        consumibleObject.SetActive(false);
    }

    public virtual void respawn()
    {
        consumibleObject.SetActive(true);

    }

    public virtual void OnTriggerEnter(Collider other)
    {


    }



}
