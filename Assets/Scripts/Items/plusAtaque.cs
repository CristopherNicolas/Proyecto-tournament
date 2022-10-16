using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plusAtaque : Item
{
    public Personaje personaje;
    public int moreAttack = 2;
    public GameObject attack;
    public override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            personaje.damage *= moreAttack;
            StartCoroutine(backtoAttack());
            


        }
    }

    IEnumerator backtoAttack()
    {
        attack.SetActive(false);
        yield return new WaitForSeconds(tiempoSpawn);
        personaje.damage /= moreAttack;
        attack.SetActive(true);


        yield break;
    }
}
