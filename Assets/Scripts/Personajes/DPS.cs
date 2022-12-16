using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class DPS : Personaje
{
    public Shooting shooting;
    public GameObject Player, skillPoint, ultiSword;
    public Rigidbody playerRB, swordRB;
    public bool secondHability = true;
    Personaje personajeVida;
    move_noNetwork _spdMove;
    melee melee;


    public override void habilidad1()
    {
        base.habilidad1();
        melee = GetComponentInChildren<melee>();
        playerRB.AddForce(transform.forward * 100, ForceMode.Impulse);
        spdPlus();
    }
    public override void habilidad2()
    {
        //problema: se puede atacar aun con el click de disparo normal mientras realiza el ataque

        base.habilidad2();
        melee = GetComponentInChildren<melee>();

        if (secondHability == true)
        {
            autoAttack();
        }
      
    }
    public override void habilidad3()
    {
        base.habilidad3();
        GameObject ultiAttack = Instantiate(ultiSword, skillPoint.transform.position, skillPoint.transform.rotation) as GameObject;
    }

    public async void reduccionDMG()
    {
        damageRecibe = (damageRecibe / 2);
        Debug.Log("Reduccion lista");

        await Task.Delay(5000);

        damageRecibe = damage;
        Debug.Log("Reduccion terminada");


    }

    public async void spdPlus()
    {
        _spdMove = gameObject.GetComponent<move_noNetwork>();
        _spdMove.speed *= 2;
        melee.fireRate = 0.05f;
        Debug.Log("Velocidad aumentada");

        await Task.Delay(3000);

        melee.fireRate = 0.10f;
        _spdMove.speed /= 2;
        Debug.Log("Velocidad normal");


    }

    public async void autoAttack()
    {
        secondHability = false;
        melee.automelee_attack();
        await Task.Delay(500);
        melee.automelee_attack();
        await Task.Delay(500);
        melee.automelee_attack();

        Debug.Log("Terminado");

        await Task.Delay(8000);
        secondHability = true;
        Debug.Log("habilidad 2 lista");
    }

 
}


#region Reduccion
//IEnumerator damageReduction()
//{
//    damageRecibe = (damageRecibe / 2);
//    Debug.Log("Reduccion lista");

//    yield return new WaitForSecondsRealtime(5);

//    damageRecibe = damage;
//    Debug.Log("Reduccion desactivada");

//    yield break;
//}

#endregion

//public override void Pasivas()
//{
//    if (vida < maxvida / 2)
//    {
//        if (canPasiva == true)
//        {
//            Debug.Log("Pasiva Activada");
//            multiple = true;
//            canPasiva = false;
//        }

//    }
//    if (vida >= maxvida / 2)
//    {
//        if (canPasiva == false)
//        {
//            Debug.Log("Pasiva Desactivada");
//            resetDamage = true;
//            canPasiva = true;
//        }
//    }

//}

