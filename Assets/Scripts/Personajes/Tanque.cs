using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;


public class Tanque : Personaje
{
    [SerializeField] public GameObject player;
    public GameObject Shield, SkillPoint, granade, ultiBomb;
    public bool canShield = true;
    public Transform shieldParent;
    //public Rigidbody rbGranade;
    //public Granada granada;

    public override void habilidad1()
    {
        base.habilidad1();
        cooldownForCharge1 = 0;


        if (canShield == true)
        {
            canShield = false;
            activateShieldSkill();

            Debug.Log("Escudo desplegado!");
        }   
    }
    public override void habilidad2()
    {
        base.habilidad2();
        cooldownForCharge2 = 0;

        /* GameObject bomb = Instantiate(granade, shieldPoint.transform.position, Quaternion.identity) as GameObject;
         rbGranade.AddForce(transform.forward * 20, ForceMode.Impulse);
         granada.arrojarGranada();*/
        GameObject bomb = Instantiate(granade, SkillPoint.transform.position, SkillPoint.transform.rotation) as GameObject;
        Granada granada = bomb.GetComponent<Granada>();
        granada.isNormalGranade = true;
    }
    
    public override void habilidad3()
    {
        base.habilidad3();
        GameObject ulti = Instantiate(ultiBomb, SkillPoint.transform.position, SkillPoint.transform.rotation) as GameObject;
        Granada granada = ulti.GetComponent<Granada>();
        granada.isNormalGranade = false;
    }
    public void Start()
    {
        //Shield.SetActive(false);
    }

    public override void Pasivas()
    {
        if (vida < maxvida / 4)
        {
            Debug.Log("Daño recibido dividido en 2");
            damageRecibe = (damageRecibe / 2);
        }

    }

    async void activateShieldSkill()
    {
        GameObject shieldSkill = Instantiate(Shield, SkillPoint.transform.position, SkillPoint.transform.rotation) as GameObject;
        escudo shield = shieldSkill.GetComponent<escudo>();
        if (player.gameObject.tag == "red")
        {
            shield.gameObject.tag = "redShield";
            shield.gameObject.layer = LayerMask.NameToLayer("shieldRed");
        }
        if (player.gameObject.tag == "blue")
        {
            shield.gameObject.tag = "blueShield";
            shield.gameObject.layer = LayerMask.NameToLayer("shieldBlue");
        }
        shield.isShield = true;
        shieldSkill.transform.SetParent(shieldParent);
        shieldSkill.transform.SetParent(shieldParent, true);

        await Task.Delay(9000);

        shieldSkill.transform.SetParent(shieldParent, false);
        shield.isShield = false;
        shieldSkill.transform.SetParent(null);
        Destroy(shieldSkill);
        canShield = true;
    }

    //IEnumerator activateShield()
    //{
    //    GameObject shieldSkill = Instantiate(Shield, SkillPoint.transform.position, SkillPoint.transform.rotation) as GameObject;
    //    escudo shield = shieldSkill.GetComponent<escudo>();
    //    if (player.gameObject.tag == "red")
    //    {
    //        shield.gameObject.tag = "redShield";
    //        shield.gameObject.layer = LayerMask.NameToLayer("shieldRed");
    //    }
    //    if (player.gameObject.tag == "blue")
    //    {
    //        shield.gameObject.tag = "blueShield";
    //        shield.gameObject.layer = LayerMask.NameToLayer("shieldBlue");
    //    }
    //    shield.isShield = true;
    //    shieldSkill.transform.SetParent(shieldParent);
    //    shieldSkill.transform.SetParent(shieldParent, true);

    //    yield return new WaitForSecondsRealtime(30);

    //    shieldSkill.transform.SetParent(shieldParent, false);
    //    shield.isShield = false;
    //    shieldSkill.transform.SetParent(null);
    //    Destroy(shieldSkill);
    //    canShield = true;

    //    yield break;

    //}

    
}
