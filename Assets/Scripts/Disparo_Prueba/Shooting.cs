using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.VFX;

public class Shooting : NetworkBehaviour
{
    public float weaponRange = 100f; //Distancia máxima de disparo (raycast)
    public float fireRate = 0.25f; //Tiempo entre disparos
    private float nextFire;
    public float hitForce = 100f; //Empuja al objeto que se le dispara
    public float damage = 10f; //Daño de arma
    public float damageOriginal = 10f;
    public int maxAmmo = 10; //munición
    public int currentAmmo = 10;
    public float realoadTime = 1f; //tiempo de recarga
    private bool isReloading = false;

    //public Animator animator; //animacion para recarga faltante


    [SerializeField] public GameObject player;
    [SerializeField] public GameObject impactoEfecto; //efecto de bala de impacto en superficie [Prefab]
    [SerializeField] public VisualEffect muzzleFlash; //efecto de balas al disparar
    [SerializeField] public Camera fpscam; //Camara de donde nace el raycast
    public Transform gunEnd; //Empty de punta de arma

    public LayerMask shieldRed;
    public LayerMask shieldBlue;

    [Header("ShootSound")]
    public AudioSource disparoSound;
    public AudioClip[] disparoSoundBank;

    public GameObject InstanciarVFX(Vector3 POINT)
    {
        //instancias
        var obj = Instantiate(impactoEfecto,POINT,Quaternion.identity);
        return obj;
    }
    //[SerializeField] private AudioSource gunAudio; //(Necesita audioSource)

    // Start is called before the first frame update
    void Start()
    {
        //gunAudio = GetComponent<AudioSource>();
        currentAmmo = maxAmmo;
    }

    void OnEnable()
    {
        isReloading = false;
        //animator.SetBool("Reloading", false);
    }

    // Update is called once per frame
    void Update()
<<<<<<< Updated upstream
    {
=======
     {
>>>>>>> Stashed changes
        if (!IsOwner) return;
        #region DrawRay(viewport)
        Vector3 lineOrigin = fpscam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //Crea un vector en el centro de la camara del viewport (lineOrigin)
        Debug.DrawRay(lineOrigin, fpscam.transform.forward*weaponRange, Color.green);
        #endregion

        if (isReloading)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(Reload());
            return;
        }

        if (Input.GetMouseButton(0) && Time.time > nextFire && currentAmmo > 0)
        {
            shoot();
            AudioSystem.instance.PonerSonido(AudioSystem.instance.audiosTanke[0], AudioSystem.instance.audioSourcePlayer);
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;
        Debug.Log("Reloading...");

        //animator.SetBool("", true); // falta animacion

        yield return new WaitForSeconds(realoadTime - 0.25f);
        //animator.SetBool("", false); // falta animacion
        yield return new WaitForSeconds(0.25f);
        currentAmmo = maxAmmo;
        isReloading = false;
    }

    public void damageIncrease()
    {
        damage = damage * 2;
    }

    public void resetdamage()
    {
        damage = damageOriginal;
    }

    void shoot()
    {
        if (player == null) player = transform.root.gameObject;
        muzzleFlash.Play(); //play particulas
        currentAmmo--; //Restar munición(1)

        nextFire = Time.time + fireRate;
        Vector3 rayOrigin = fpscam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //Crea un vector en el centro de la camara del viewport (rayOrigin)


        RaycastHit hit; //Detecta si tiene algo enfrente del raycast

        #region En colision Player +RED+
        if (player.gameObject.tag == "red") //tag que señala de que equipo pertenece el jugador
        {
            if (Physics.Raycast(rayOrigin, fpscam.transform.forward, out hit, weaponRange, ~shieldRed)) //Distancia y dirección del raycast, se puede cambiar el weapon range por Mathf.Infinity para hacer la distancia infinito
            {
                Debug.Log("Colision de disparo");

                if (hit.rigidbody != null) //si colisiona con algo con rigidbody
                {
                    GameObject impactoeffectGo2 = InstanciarVFX(hit.point);
                   // GameObject impactoeffectGo = Instantiate(impactoEfecto, hit.point, Quaternion.identity) as GameObject; //crea particulas de efecto en zona que se le disparo
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
                    if(hit.collider.gameObject.tag == "blueShield")//tag especificando el objeto que se le disparo
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
                Debug.Log("Colision de disparo");

                if (hit.rigidbody != null) //si colisiona con algo con rigidbody
                {
                    Debug.Log(hit.transform.name);
                    GameObject impactoeffectGo2 = InstanciarVFX(hit.point);
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
                        var fps = hit.collider.GetComponent<FirstPersonMovement>();
                        if (player is not null)
                            player.TakeDamage(damage);
                        else fps.hp -= damage;

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

        #region PLAYER VS (NO forma equipo) NO USAR
        /*if (player.gameObject.tag == "Player")
        {

        }
        if (Physics.Raycast(rayOrigin, fpscam.transform.forward, out hit, weaponRange)) //Distancia y dirección del raycast, se puede cambiar el weapon range por Mathf.Infinity para hacer la distancia infinito
        {
            Debug.Log("Colision de disparo");

            if (hit.rigidbody != null) //si colisiona con algo con rigidbody
            {
                GameObject impactoeffectGo = Instantiate(impactoEfecto, hit.point, Quaternion.identity) as GameObject; //crea particulas de efecto en zona que se le disparo
                Destroy(impactoeffectGo, 2f);

                #region Player VS Practice target
                if (hit.collider.gameObject.tag == "target") //tag especificando el objeto que se le disparo
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce); //Empuje a objeto que se le disparo
                    PracticeTarget target = hit.collider.gameObject.GetComponent<PracticeTarget>(); //Llama componente del script del target
                    target.TakeDamage(damage); //Target recibe daño
                }
                #endregion

                #region PlayerVSPlayer

                #region PLAYER atacaste a PLAYER
                if (hit.collider.gameObject.tag == "Player") //tag especificando el objeto que se le disparo
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce); //Empuje a objeto que se le disparo
                    Personaje player = hit.collider.gameObject.GetComponent<Personaje>();
                    player.TakeDamage(damage);

                    Debug.Log("PLAYER HIT PLAYER");

                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("dps"))
                    {
                        Debug.Log("PLAYER daño a DPS PLAYER");
                    }
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("soporte"))
                    {
                        Debug.Log("PLAYER daño a Soporte PLAYER");
                    }
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("tanque"))
                    {
                        Debug.Log("PLAYER daño a Tanque PLAYER");
                    }
                }
                #endregion

                #region PLAYER atacaste a blue
                if (hit.collider.gameObject.tag == "blue") //tag especificando el objeto que se le disparo
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce); //Empuje a objeto que se le disparo
                    Personaje player = hit.collider.gameObject.GetComponent<Personaje>();
                    player.TakeDamage(damage);

                    Debug.Log("PLAYER HIT -BLUE-");

                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("dps"))
                    {
                        Debug.Log("PLAYER daño a DPS -Blue-");
                    }
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("soporte"))
                    {
                        Debug.Log("PLAYER daño a Soporte -Blue-");
                    }
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("tanque"))
                    {
                        Debug.Log("PLAYER daño a Tanque -Blue-");
                    }
                }
                #endregion

                #region PLAYER atacaste a red
                if (hit.collider.gameObject.tag == "red") //tag especificando el objeto que se le disparo
                {
                    hit.rigidbody.AddForce(-hit.normal * hitForce); //Empuje a objeto que se le disparo
                    Personaje player = hit.collider.gameObject.GetComponent<Personaje>();
                    player.TakeDamage(damage);

                    Debug.Log("PLAYER HIT +Red+");

                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("dps"))
                    {
                        Debug.Log("PLAYER daño a DPS +Red+");
                    }
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("soporte"))
                    {
                        Debug.Log("PLAYER daño a Soporte +Red+");
                    }
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("tanque"))
                    {
                        Debug.Log("PLAYER daño a Tanque +Red+");
                    }
                }
                #endregion

                #endregion

                #region shield
                if (hit.collider.gameObject.tag == "blueShield") //tag especificando el objeto que se le disparo
                {
                    escudo shield = hit.collider.gameObject.GetComponent<escudo>();
                    shield.TakeDamage(damage);
                    Debug.Log("PALYER HIT -BLUE- SHIELD");
                }
                if (hit.collider.gameObject.tag == "redShield") //tag especificando el objeto que se le disparo
                {
                    escudo shield = hit.collider.gameObject.GetComponent<escudo>();
                    shield.TakeDamage(damage);
                    Debug.Log("PLAYER HIT +RED+ SHIELD");
                }
                if (hit.collider.gameObject.tag == "Default") //tag especificando el objeto que se le disparo
                {
                    escudo shield = hit.collider.gameObject.GetComponent<escudo>();
                    shield.TakeDamage(damage);
                    Debug.Log("PLAYER HIT +RED+ SHIELD");
                }
                #endregion
            }
        }*/
        #endregion
    }

}
