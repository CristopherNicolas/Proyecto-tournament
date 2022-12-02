using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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
    public float ultiCharge;
    public int Move;
    public int damage = 20,cooldown1, cooldown2;
    public float damageRecibe;
    public int speed = 8;
    public float h1c = 3, h2c = 7, h3c = 16;
    public float tiempoInvulnerabilidad = 5;

    public bool canPasiva = true, multiple = false, resetDamage = false, alterado = false, skill1 = true, skill2 = true;

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
        if (Input.GetMouseButtonDown(1) && skill1)
        {
            habilidad1();
            skill1 = false;
        }

        else if(Input.GetKeyDown(KeyCode.E) && skill2)
        {
            habilidad2();
            skill2 = false;
        }

        else if(Input.GetKeyDown(KeyCode.Q) && ultiCharge >= 100)
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

    public async void startCooldown()
    {

        if(!skill1)
        {
            Debug.Log("Recargando habilidad 1");
            await Task.Delay(cooldown1);
            skill1 = true;
            Debug.Log("Habilidad 1 lista");

        }

        if(!skill2)
        {
            Debug.Log("Recargando habilidad 2");
            await Task.Delay(cooldown2);
            skill2 = true;
            Debug.Log("Habilidad 2 lista");
        }

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
