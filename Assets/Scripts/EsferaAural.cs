using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EsferaAural : MonoBehaviour
{
    public Personaje personajeEsfera;
    public List<Vector3> posicionesVectores;
    /// <summary>
    /// se llama cuando se inicia la escena 
    /// </summary>
    /// <param name="obj"></param>
    public void Personaje(GameObject obj)
    {
        Personaje personaje = obj.GetComponent<Personaje>();
        // remover script personaje del fps
        personajeEsfera = personaje;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<FirstPersonMovement>()
            && ! collision.transform.GetComponent<Personaje>())
        {
            //añadir personajeEsfera al fps con el que coliciona
            Destroy(gameObject);
        }
    }
    private void Update()
    {
        transform.Rotate(new Vector3(0, 4, 0));
    }
}
