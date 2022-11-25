using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System.Linq;

public class DPS: Personaje
{
    public Shooting shooting;
    public GameObject Player, skillPoint, knife;
    public bool canUlti = false;
    Personaje personajeVida;
    move_noNetwork _spdMove;
  

    public override void habilidad1()
    {
        base.habilidad1();
        spdPlus();
    }
    public override void habilidad2()
    {
        base.habilidad2();

        reduccionDMG();
    }
    public override void habilidad3()
    {
        base.habilidad3();

        RaycastHit[] hits;
        hits = Physics.RaycastAll(skillPoint.transform.position, skillPoint.transform.forward, 100);

        var cleanArr = hits.Where(x => x.transform.tag != transform.tag);

        cleanArr.ToList().ForEach(x => x.transform.GetComponent<Personaje>().vida -= 30);

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
        Debug.Log("Velocidad aumentada");

        await Task.Delay(3000);

        _spdMove.speed /= 2;
        Debug.Log("Velocidad normal");


    }

    public async override void estadoAletrado()
    {
        vida -= 2;
        await Task.Delay(4000);
        alterado = false;
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

    public override void Pasivas()
    {
        if (vida < maxvida / 2)
        {
            if (canPasiva == true)
            {
                Debug.Log("Pasiva Activada");
                multiple = true;
                canPasiva = false;
            }

        }
        if (vida >= maxvida / 2)
        {
            if (canPasiva == false)
            {
                Debug.Log("Pasiva Desactivada");
                resetDamage = true;
                canPasiva = true;
            }
        }

    }
}
