using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escudo : MonoBehaviour
{
    public bool isShield = false;
    public float lifeOfShield = 1500f;
    public GameObject shield;

    public void Start()
    {
       while(!isShield && lifeOfShield < 1500)
       {
            lifeOfShield += Time.deltaTime * 2;
       }
    }

    public void Update()
    {
        //Hacer que el escudo reciba daño y de destrulla
        //VFX: hacer que el escudo cambie de color cuando llegue a la mitad de su vida
        if (lifeOfShield <= 750 && lifeOfShield > 0)
        {
            //shield.GetComponent<SpriteRenderer>();
        }

        else if(lifeOfShield <= 0)
        {
            Destroy(shield);
        }
    }

    public void shieldLife()
    {
       
    }

    public void TakeDamage(float damage)
    {
        lifeOfShield -= damage;
        if (lifeOfShield <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Escudo Roto");
        }
    }

}
