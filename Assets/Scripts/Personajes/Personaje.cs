using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// clase que contiene la base para crear las clases del personaje
/// bug posible  : cambiar la ui cuando cambien las stats 
/// </summary>
public abstract class Personaje : MonoBehaviour
{
    Shooting shooting;

    public string nombrePersonaje;
    public float vida = 100, maxvida = 100,  energia = 100;
    public int Move;
    public int damage = 20;
    public float damageRecibe;
    public int speed = 8;
    public float h1c = 3, h2c = 7, h3c = 16;
    public float tiempoInvulnerabilidad = 5;

    public bool canPasiva = true, multiple = false, resetDamage = false;

    public virtual void CambiarCooldowns(float _h1c, float _h2c, float _h3c)
    {
        h1c = _h1c; h2c = _h2c; h3c = _h3c;
    }
    public virtual void habilidad1()
    {
        energia -= 10;
    }
    public virtual void habilidad2()
    {
        energia -= 20;

    }
    public virtual void habilidad3()
    {
        energia -= 30;

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
        }


        PasivaSupport();

        PasivaDPS();

        PasivaTank();
      
    }

    #region Pasiva

    #region DPS_Pasiva
    public virtual void PasivaDPS()
    {
        if (vida < maxvida/2)
        {
            if (canPasiva == true)
            {
                Debug.Log("halflife2");
                multiple = true;
                canPasiva = false;
            }
            
        }
        if (vida >= maxvida/2) 
        {
            if (canPasiva == false)
            {
                Debug.Log("halflife3 confirm");
                resetDamage = true;
                canPasiva = true;
            }
        }
    }
    #endregion

    #region TankPasiva
    public virtual void PasivaTank()
    {
        if (vida < maxvida/4)
        {
            Debug.Log("Daño recibido dividido en 2");
            damageRecibe = (damageRecibe / 2);
        }
    }
    #endregion

    #region SupportPasiva
    public virtual void PasivaSupport()
    {
        if (vida < maxvida / 2)
        {
            if (canPasiva == true)
            {
                StartCoroutine(autoheal());
                canPasiva = false;
            }
        }
    }
    IEnumerator autoheal()
    {
        Debug.Log("I heal 1 every 5 seconds [Auto]");
        vida += 1;
        yield return new WaitForSecondsRealtime(5);
        canPasiva = true;
    }
    #endregion

    #endregion

    public void TakeDamage(float damage)
    {
        damageRecibe = damage;

        vida -= damageRecibe;
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
