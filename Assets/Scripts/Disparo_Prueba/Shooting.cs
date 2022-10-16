using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    public float weaponRange = 100f; //Distancia máxima de disparo (raycast)
    public float fireRate = 0.25f; //Tiempo entre disparos
    private float nextFire;
    public float hitForce = 100f; //Empuja al objeto que se le dispara
    public float damage = 10f; //Daño de arma

    public int maxAmmo = 10; //munición
    public int currentAmmo = 10;
    public float realoadTime = 1f; //tiempo de recarga
    private bool isReloading = false;

    //public Animator animator; //animacion para recarga faltante

    [SerializeField] public GameObject impactoEfecto; //efecto de bala de impacto en superficie [Prefab]
    [SerializeField] public ParticleSystem muzzleFlash; //efecto de balas al disparar
    [SerializeField] public Camera fpscam; //Camara de donde nace el raycast
    public Transform gunEnd; //Empty de punta de arma


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
    {
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



    void shoot()
    {
        muzzleFlash.Play(); //play particulas
        currentAmmo--; //Restar munición(1)

        nextFire = Time.time + fireRate;
        Vector3 rayOrigin = fpscam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0)); //Crea un vector en el centro de la camara del viewport (rayOrigin)


        RaycastHit hit; //Detecta si tiene algo enfrente del raycast

        #region En_colision
        if (Physics.Raycast(rayOrigin, fpscam.transform.forward, out hit, weaponRange)) //Distancia y dirección del raycast, se puede cambiar el weapon range por Mathf.Infinity para hacer la distancia infinito
        {
            Debug.Log("Colision de disparo");

            if (hit.rigidbody != null) //si colisiona con algo con rigidbody
            {
                GameObject impactoeffectGo = Instantiate(impactoEfecto, hit.point, Quaternion.identity) as GameObject; //crea particulas de efecto en zona que se le disparo
                Destroy(impactoeffectGo, 2f);

                hit.rigidbody.AddForce(-hit.normal * hitForce); //Empuje a objeto que se le disparo


                if (hit.collider.gameObject.tag == "target") //tag especificando el objeto que se le disparo
                {
                    PracticeTarget target = hit.collider.gameObject.GetComponent<PracticeTarget>(); //Llama componente del script del target
                    target.TakeDamage(damage); //Target recibe daño
                }
            }
        }
        #endregion
    }

}
