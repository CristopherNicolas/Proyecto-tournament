using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tanque : Personaje
{
    public GameObject Shield, shieldPoint, granade;
    public bool canShield = true;
    public Transform shieldParent;
    public Rigidbody rbGranade;
    public Granada granada;
    
    public override void habilidad1()
    {
        base.habilidad1();

        StartCoroutine(activateShield());

        Debug.Log("Escudo desplegado!");
        
    }
    public override void habilidad2()
    {
        base.habilidad2();
       
        GameObject bomb = Instantiate(granade, shieldPoint.transform.position, Quaternion.identity) as GameObject;
        rbGranade.AddForce(transform.forward * 20, ForceMode.Impulse);
        granada.arrojarGranada();


    }
    
    public override void habilidad3()
    {
        base.habilidad3();
    }
    //public override void Pasiva()
    //{
    //    base.globalPasivaTanque();
    //}

    public void Start()
    {
        //Shield.SetActive(false);
    }

    //public void Update()
    //{

    //    move();

    //   if (Input.GetMouseButton(1))
    //   {
    //        habilidad1();
    //   }

    //   else if(Input.GetKeyDown(KeyCode.E))
    //   {
    //        habilidad2();
    //   }
    //}

    IEnumerator activateShield()
    {
        Shield.SetActive(true);
        yield return new WaitForSeconds(10);
        Shield.SetActive(false);

        yield break;

    }

    
}
