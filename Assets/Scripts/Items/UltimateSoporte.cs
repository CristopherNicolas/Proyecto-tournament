using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UltimateSoporte : MonoBehaviour
{
    public GameObject ultimate;
    public float radiosExplosion, forceExplosion, point0 = 1, point1 = 3;
    public Collider[] colliders;

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, radiosExplosion);

    }

    public void Start()
    {
        StartCoroutine(desactiveUltimate());
    }

    public void Update()
    {
        ultimateOnWork();
    }

    public void ultimateOnWork()
    {
        colliders = Physics.OverlapSphere(transform.position, radiosExplosion);

        foreach (Collider nearbyObject in colliders)
        {

            if (nearbyObject.transform.tag == "red" || nearbyObject.transform.tag == "blue")
            {
                Personaje _hpPersonaje = nearbyObject.transform.GetComponent<Personaje>();

                if (_hpPersonaje != null && _hpPersonaje.vida < 100)
                {
                    _hpPersonaje.vida += 2;
                    Debug.Log("Estas siendo sanado " + _hpPersonaje.vida);

                    if(_hpPersonaje.vida >=  100)
                    {
                        _hpPersonaje.vida = 100;
                        Debug.Log("Sanado completamente");

                    }

                }

            }

        }
    }

    IEnumerator desactiveUltimate()
    {
        yield return new WaitForSecondsRealtime(20);
        Destroy(ultimate);

        yield break;
    }
}
