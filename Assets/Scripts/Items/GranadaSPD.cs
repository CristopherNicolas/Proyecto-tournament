using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadaSPD : MonoBehaviour
{
    public GameObject spdGranade;
    public float radiosExplosion, forceExplosion;
    public bool grandaforSPD;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void Update()
    {

    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radiosExplosion);

    }

    void OnCollisionEnter(Collision collision)
    {
        if (grandaforSPD)
        {
            exploteSPD();

        }

        else
        {
            exploteDamage();
        }
    }

    #region
    IEnumerator lessSpd()
    {
       
        yield return new WaitForSeconds(5);



        yield break;
    }

    public void exploteSPD()
    {
        //VFX: crear un sistema de particulas para la granada
        //el daño de la granada debe ser leve, la fuerza que tiene esta para empujar a los enemigos puede crecer un poco más
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiosExplosion);

        foreach (Collider nearbyObject in colliders)
        {

            if (nearbyObject.transform.tag == "red" || nearbyObject.transform.tag == "blue")
            {
                FirstPersonMovement _spdMove = nearbyObject.transform.GetComponent<FirstPersonMovement>();

                if (_spdMove != null)
                {
                    _spdMove.speed *= 2;
                    Debug.Log("soy " + nearbyObject.name + " y mi velocidad es de " + nearbyObject.transform.GetComponent<FirstPersonMovement>().speed);

                }

            }

        }
        Destroy(spdGranade);

    }
    #endregion

    public void exploteDamage()
    {
        //VFX: crear un sistema de particulas para la granada
        //el daño de la granada debe ser leve, la fuerza que tiene esta para empujar a los enemigos puede crecer un poco más
        Collider[] colliders = Physics.OverlapSphere(transform.position, radiosExplosion);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(forceExplosion, transform.position, radiosExplosion);
                Debug.Log("Explota");

                if (nearbyObject.gameObject.tag == "red")
                {

                    rb.AddExplosionForce(forceExplosion, transform.position, radiosExplosion);
                    Debug.Log("Explota");

                }

                if (nearbyObject.gameObject.tag == "blue")
                {

                    rb.AddExplosionForce(forceExplosion, transform.position, radiosExplosion);
                    Debug.Log("Explota");

                }
            }

            PracticeTarget target = nearbyObject.GetComponent<PracticeTarget>();
            if (target != null)
            {
                target.targetHealth -= 5;

                if (nearbyObject.gameObject.tag == "red")
                {

                    target.targetHealth -= 5;

                }

                if (nearbyObject.gameObject.tag == "blue")
                {

                    target.targetHealth -= 5;
                }
            }

            Destroy(spdGranade);
        }

    }

}
