using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipWeapon : MonoBehaviour
{
    public GameObject ultiSword;

    public void Start()
    {
        gameObject.transform.Rotate(90, 0, 0);
        Rigidbody swordRB = GetComponent<Rigidbody>();
        swordRB.AddForce(transform.up * 10, ForceMode.Impulse);
    }

    public void OnTriggerEnter(Collider other)
    {
        Personaje personajes = other.GetComponent<Personaje>();

        if(personajes != null)
        {
            personajes.vida -= 50;
        }
    }

}
