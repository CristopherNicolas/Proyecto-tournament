using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocion : Item
{
    public float cantidadDeVida = 25;
    public Personaje personaje;
    public GameObject potion;

    public void Start()
    {
        consumibleObject = potion;
    }
    public override void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && personaje.vida < 100)
        {
            personaje.vida += transform.localScale.x * cantidadDeVida;
            StartCoroutine(pocionRespawn());
            


            if (personaje.vida > 100)
            {
                personaje.vida = 100;
            }    
        }

        IEnumerator pocionRespawn()
        {
            potion.SetActive(false);
            yield return new WaitForSeconds(5);
            potion.SetActive(true);

            yield break;
        }
    }
}
