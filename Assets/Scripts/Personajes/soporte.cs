using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soporte : Personaje
{
    public GameObject skillPoint, healtSphere, ultimate;
    public override void habilidad1()
    {
        base.habilidad1();
        GameObject healt = Instantiate(healtSphere, skillPoint.transform.position, skillPoint.transform.rotation) as GameObject;
        Rigidbody projectilHealt = healt.GetComponent<Rigidbody>();
        GranadaSPD granades = healt.GetComponent<GranadaSPD>();
        granades.grandaforSPD = true;
        projectilHealt.AddForce(transform.forward * 20, ForceMode.Impulse);

    }
    public override void habilidad2()
    {
        base.habilidad2();
        GameObject healt = Instantiate(healtSphere, skillPoint.transform.position, skillPoint.transform.rotation) as GameObject;
        Rigidbody projectilHealt = healt.GetComponent<Rigidbody>();
        GranadaSPD granades = healt.GetComponent<GranadaSPD>();
        granades.grandaforSPD = false;
        projectilHealt.AddForce(transform.forward * 20, ForceMode.Impulse);
    }
    public override void habilidad3()
    {
        base.habilidad3();
        GameObject ulti = Instantiate(ultimate, skillPoint.transform.position, skillPoint.transform.rotation) as GameObject;
        UltimateSoporte areaUlti = ulti.GetComponent<UltimateSoporte>();

    }
    public override void PasivaSupport()
    {
       base.PasivaSupport();
      
    }
    


    //public void Update()
    //{
    //    move();

    //    if(Input.GetKeyDown(KeyCode.E))
    //    {
    //        habilidad1();

    //    }
    //}
}
