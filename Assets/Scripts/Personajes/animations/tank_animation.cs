using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tank_animation : MonoBehaviour
{
    public Animator animator;

    public void Awake()
    {
        animator = GetComponent<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            isWalkingAnimActive(); //anim
        }
        else
        {
            isWalkingAnimFalse(); //anim
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            inJumpAnimActive();
        }
    }


    public void isWalkingAnimActive()
    {
        animator.SetBool("isWalking", true);
    }
    public void isWalkingAnimFalse()
    {
        animator.SetBool("isWalking", false);
    }


    public void onShieldAnimActive()
    {
        animator.SetBool("onShield", true);
    }
    public void onShieldAnimFalse()
    {
        animator.SetBool("onShield", false);
    }



    public void onUltimateAnimActive()
    {
        animator.SetTrigger("onUltimate");
    }
    public void inJumpAnimActive()
    {
        animator.SetTrigger("inJump");
    }
    public void bombShootAnimActive()
    {
        animator.SetTrigger("bombShoot");
    }

    public void onShootAnimActive()
    {
        animator.SetTrigger("onShoot");
    }


}
