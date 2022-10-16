using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curacion : MonoBehaviour
{
    public Rigidbody rbCuracion;
    public GameObject character;

    // Update is called once per frame
    void Update()
    {

        rbCuracion.AddForce(transform.forward * 40);

    }


}
