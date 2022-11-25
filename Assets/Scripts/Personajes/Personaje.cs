using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
/// <summary>
/// clase que contiene la base para crear las clases del personaje
/// bug posible  : cambiar la ui cuando cambien las stats 
/// </summary>
public abstract class Personaje : MonoBehaviour
{
    Shooting shooting;

    public string nombrePersonaje;
    public float vida = 100, maxvida = 100;
    public float cooldown1 = 5, cooldown2 = 10, ultiCharge, cooldownForCharge1 = 5, cooldownForCharge2 = 10;
    public int Move;
    public int damage = 20;
    public float damageRecibe;
    public int speed = 8;
    public float h1c = 3, h2c = 7, h3c = 16;
    public float tiempoInvulnerabilidad = 5;

    public bool canPasiva = true, multiple = false, resetDamage = false, alterado = false;

    public virtual void CambiarCooldowns(float _h1c, float _h2c, float _h3c)
    {
        h1c = _h1c; h2c = _h2c; h3c = _h3c;
    }
    public virtual void habilidad1()
    {
       
    }
    public virtual void habilidad2()
    {

    }
    public virtual void habilidad3()
    {

    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            habilidad1();

        }

        else if(Input.GetKeyDown(KeyCode.E))
         {
          habilidad2();
         }
         
        else if(Input.GetKeyDown(KeyCode.Q))
        {
            habilidad3();
            ultiCharge = 0;
        }

        Pasivas();
        startCooldown();

      
    }


    public virtual void Pasivas()
    {


    }

    public virtual void startCooldown()
    {

    }

    [ServerRpc]
    public void TakeDamage(float damage)
    {
        damageRecibe = damage;

        vida -= damageRecibe;
        ultiCharge += 1.5F;
        //Debug.Log("" + damageRecibe);
        if (vida <= 0)
        {
            Debug.Log("Player Die");
        }
    }

    public void TakeHeal(float heal)
    {
        vida += heal;
        if (vida >= 100)
        {
            vida = 100;
            Debug.Log("Player is fully heal");
        }
    }

    
}
