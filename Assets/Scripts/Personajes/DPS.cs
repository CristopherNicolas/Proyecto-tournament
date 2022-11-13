using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DPS: Personaje
{
    public Shooting shooting;
    public GameObject knife, pointKnife;
    public Transform originKnife;
    public override void habilidad1()
    {
        base.habilidad1();

        StartCoroutine(melee());
    }
    public override void habilidad2()
    {
        base.habilidad2();
    }
    public override void habilidad3()
    {
        base.habilidad3();
    }
    public override void PasivaDPS()
    {
        base.PasivaDPS();
        if (multiple == true)
        {
            shooting.damageIncrease();
            multiple = false;
        }
        if (resetDamage == true)
        {
            multiple = false;
            shooting.resetdamage();
            resetDamage = false;
        }
    }

    IEnumerator melee()
    {
        knife.SetActive(true);
        yield return new WaitForSeconds(3);
        knife.SetActive(false);


        yield break;
    }

    /*public void Update()
    {
        
    }*/

    //public void Update()
    //{
    //    move();

    //    if(Input.GetMouseButtonDown(1))
    //    {
    //        habilidad1();          
    //    }

    //}
}
