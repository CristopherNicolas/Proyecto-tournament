using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soporte : Personaje
{
    public GameObject bornPoint, healtSphere;
    public Rigidbody healtRB;
    public override void habilidad1()
    {
        base.habilidad1();
        GameObject healt = Instantiate(healtSphere, bornPoint.transform.position,bornPoint.transform.rotation) as GameObject;
        Rigidbody projectilHealt = healt.GetComponent<Rigidbody>();
        healtRB.velocity = Camera.main.transform.forward * 60;
    }
    public override void habilidad2()
    {
        base.habilidad2();
    }
    public override void habilidad3()
    {
        base.habilidad3();
    }
    //public override void globalPasivaSoporte()
    //{
    //    base.globalPasivaSoporte();

    //}


    //public void Update()
    //{
    //    move();

    //    if(Input.GetKeyDown(KeyCode.E))
    //    {
    //        habilidad1();
            
    //    }
    //}
}
