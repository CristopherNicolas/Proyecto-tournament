using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PracticeTarget : MonoBehaviour
{
    public float targetHealth = 100f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
 
    }
    public void TakeDamage(float damage)
    {
        targetHealth -= damage;
        if (targetHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
