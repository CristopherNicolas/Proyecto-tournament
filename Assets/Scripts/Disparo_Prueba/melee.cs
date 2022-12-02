using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class melee : MonoBehaviour
{
    public float weaponRange = 100f, fireRate = 0.25f, hitForce = 100f, damage = 10f, damageOriginal = 10f;
    private float nextFire;

    [SerializeField] public GameObject player, impactoEfecto;
    [SerializeField] public Camera fpscam;

    public LayerMask shieldRed;
    public LayerMask shieldBlue;

    [ServerRpc]
    public GameObject InstanciarVFXServerRpc(Vector3 POINT)
    {
        //instancias
        var obj = Instantiate(impactoEfecto, POINT, Quaternion.identity);
        obj.GetComponent<NetworkObject>().Spawn();
        return obj;
    }

    //[SerializeField] private AudioSource meleeAudio;

    // Start is called before the first frame update
    void Start()
    {
        //meleeAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lineOrigin = fpscam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawRay(lineOrigin, fpscam.transform.forward * weaponRange, Color.green);

        if (Input.GetMouseButton(0) && Time.time > nextFire)
        {
            melee_attack();
        }
    }

    void melee_attack()
    {
        if (player == null) player = transform.root.gameObject;
        nextFire = Time.time + fireRate;
        Vector3 rayOrigin = fpscam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        #region En colision Player +RED+
        if (player.gameObject.tag == "red") //tag que señala de que equipo pertenece el jugador
        {
            if (Physics.Raycast(rayOrigin, fpscam.transform.forward, out hit, weaponRange, ~shieldRed)) //Distancia y dirección del raycast, se puede cambiar el weapon range por Mathf.Infinity para hacer la distancia infinito
            {
                Debug.Log("Colision de disparo [Melee]");

                if (hit.rigidbody != null) //si colisiona con algo con rigidbody
                {
                    GameObject impactoeffectGo2 = InstanciarVFXServerRpc(hit.point);
                    //GameObject impactoeffectGo = Instantiate(impactoEfecto, hit.point, Quaternion.identity) as GameObject; //crea particulas de efecto en zona que se le disparo
                    Destroy(impactoeffectGo2, 2f);

                    #region Player VS Practice target
                    if (hit.collider.gameObject.tag == "target") //tag especificando el objeto que se le disparo
                    {
                        hit.rigidbody.AddForce(-hit.normal * hitForce); //Empuje a objeto que se le disparo
                        PracticeTarget target = hit.collider.gameObject.GetComponent<PracticeTarget>(); //Llama componente del script del target
                        target.TakeDamage(damage); //Target recibe daño
                    }
                    #endregion

                    #region +RED+ VS -Blue-
                    if (hit.collider.gameObject.tag == "blue") //tag especificando el objeto que se le disparo
                    {
                        hit.rigidbody.AddForce(-hit.normal * hitForce); //Empuje a objeto que se le disparo
                        Personaje player = hit.collider.gameObject.GetComponent<Personaje>();
                        if (player is null) return;
                        player.TakeDamage(damage);

                        Debug.Log("+RED+ HIT -BLUE-");

                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("dps"))
                        {
                            Debug.Log("+RED+ daño a DPS -Blue-");
                        }
                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("soporte"))
                        {
                            Debug.Log("+RED+ daño a Soporte -Blue-");
                        }
                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("tanque"))
                        {
                            Debug.Log("+RED+ daño a Tanque -Blue-");
                        }
                    }

                    if (hit.collider.gameObject.tag == "red") //tag especificando el objeto que se le disparo
                    {
                        Debug.Log("+RED+ HIT +RED+ [Atacaste un compañero]");
                    }
                    #endregion

                    #region +RED+ VS -Blue- Shield
                    if (hit.collider.gameObject.tag == "blueShield")//tag especificando el objeto que se le disparo
                    {
                        escudo shield = hit.collider.gameObject.GetComponent<escudo>();
                        shield.TakeDamage(damage);
                        Debug.Log("+RED+ HIT -BLUE- SHIELD");
                    }
                    #endregion
                }
            }
        }
        #endregion


        #region En colision Player +Blue+
        if (player.gameObject.tag == "blue") //tag que señala de que equipo pertenece el jugador
        {
            if (Physics.Raycast(rayOrigin, fpscam.transform.forward, out hit, weaponRange, ~shieldBlue)) //Distancia y dirección del raycast, se puede cambiar el weapon range por Mathf.Infinity para hacer la distancia infinito
            {
                Debug.Log("Colision de disparo [Melee]");

                if (hit.rigidbody != null) //si colisiona con algo con rigidbody
                {
                    GameObject impactoeffectGo2 = InstanciarVFXServerRpc(hit.point);
                    //GameObject impactoeffectGo = Instantiate(impactoEfecto, hit.point, Quaternion.identity) as GameObject; //crea particulas de efecto en zona que se le disparo
                    Destroy(impactoeffectGo2, 2f);

                    #region Player VS Practice target
                    if (hit.collider.gameObject.tag == "target") //tag especificando el objeto que se le disparo
                    {
                        hit.rigidbody.AddForce(-hit.normal * hitForce); //Empuje a objeto que se le disparo
                        PracticeTarget target = hit.collider.gameObject.GetComponent<PracticeTarget>(); //Llama componente del script del target
                        target.TakeDamage(damage); //Target recibe daño
                    }
                    #endregion

                    #region -Blue- VS +Red+
                    if (hit.collider.gameObject.tag == "red") //tag especificando el objeto que se le disparo
                    {
                        hit.rigidbody.AddForce(-hit.normal * hitForce); //Empuje a objeto que se le disparo
                        Personaje player = hit.collider.gameObject.GetComponent<Personaje>();
                        player.TakeDamage(damage);

                        Debug.Log("-BLUE- HIT +RED+");

                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("dps"))
                        {
                            Debug.Log("-Blue- daño a DPS +RED+");
                        }
                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("soporte"))
                        {
                            Debug.Log("-Blue- daño a Soporte +RED+");
                        }
                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("tanque"))
                        {
                            Debug.Log("-Blue- daño a Tanque +RED+");
                        }
                    }

                    if (hit.collider.gameObject.tag == "blue") //tag especificando el objeto que se le disparo
                    {
                        Debug.Log("+RED+ HIT +RED+ [Atacaste un compañero]");
                    }
                    #endregion

                    #region -Blue- VS +RED+ Shield
                    if (hit.collider.gameObject.tag == "redShield")//tag especificando el objeto que se le disparo
                    {
                        escudo shield = hit.collider.gameObject.GetComponent<escudo>();
                        shield.TakeDamage(damage);
                        Debug.Log("-Blue- HIT +Red+ SHIELD");
                    }
                    #endregion
                }
            }
        }
        #endregion
    }
}
