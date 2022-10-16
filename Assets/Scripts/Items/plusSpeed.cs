using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plusSpeed : Item
{
    public Personaje personaje;
    public int moreSpeed = 2;
    public GameObject speed;
    public void Start()
    {
        
    }
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            personaje.speed *= moreSpeed;
            StartCoroutine(backtoSpeed());
           

        }
    }

    IEnumerator backtoSpeed()
    {
        speed.SetActive(false);
        yield return new WaitForSeconds(tiempoSpawn);
        personaje.speed /= moreSpeed;
        speed.SetActive(true);

        yield break;
    }
}
