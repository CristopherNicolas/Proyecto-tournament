using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granada : MonoBehaviour
{
    public Rigidbody rbGranada;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void arrojarGranada()
    {
        rbGranada.AddForce(transform.forward * 10);
        rbGranada.AddForce(transform.up * 10);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("player"))
        {
            
        }
    }
}
