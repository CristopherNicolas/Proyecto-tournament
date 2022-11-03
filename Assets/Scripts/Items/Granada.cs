using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{
    public float timeToExplote = 3f, radiosExplosion, forceExplosion;
    float countdown;
    public bool isNormalGranade;
    public GameObject granade;

    [SerializeField] public GameObject player;
    public void Start()
    {
        countdown = timeToExplote;
        Rigidbody rbBomb = this.GetComponent<Rigidbody>();
        rbBomb.AddForce(transform.forward * 3, ForceMode.Impulse);
    }

    public void Update()
    {
        countdown -= Time.deltaTime;

        if(countdown <= 0 && isNormalGranade)
        {
            exploteGranade();
        }

        else if(countdown <= 0 && !isNormalGranade)
        {
            exploteUltimate();
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(isNormalGranade == false)
        {
            countdown = 0;
        }
    }

    public void exploteGranade()
    {
        //VFX: crear un sistema de particulas para la granada
        //el daño de la granada debe ser leve, la fuerza que tiene esta para empujar a los enemigos puede crecer un poco más
        Collider[] colliders =  Physics.OverlapSphere(transform.position, radiosExplosion);
       
        foreach(Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddExplosionForce(forceExplosion, transform.position, radiosExplosion);
                Debug.Log("Explota");
            }

            PracticeTarget target = nearbyObject.GetComponent<PracticeTarget>();
            if (target != null)
            {
                target.targetHealth -= 5;             
            }
        }

        Destroy(granade);
    }

    public void exploteUltimate()
    {
        //VFX: crear un sistema de particulas para la ulti, esta debe ser más llamativa que la granada
        //hacer que la ulti destrulla las cajas, el daño se puede cambiar, pero debe ser alto para que se pueda considerar "letal" para la mayoria de personajes
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiosExplosion);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(forceExplosion , transform.position, radiosExplosion * 10);
                Debug.Log("Explota");
            }

            PracticeTarget target = nearbyObject.GetComponent<PracticeTarget>();
            if (target != null)
            {
                target.targetHealth -= 300;
            }
        }

        Destroy(granade);
    }

}
