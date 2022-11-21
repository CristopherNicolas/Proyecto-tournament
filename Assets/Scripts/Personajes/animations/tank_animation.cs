using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class tank_animation : NetworkBehaviour
{
    public Animator animator;

    //public void Awake() => animator = GetComponent<Animator>();
    public void Start() => animator = GetComponent<Animator>();
    
    void Update()
    {
        if (!IsOwner) return;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)) isWalkingAnimActive(); //anim
        else isWalkingAnimFalse(); //anim

        if (Input.GetKeyDown(KeyCode.Space))  inJumpAnimActive();
    }
    public void isWalkingAnimActive() => animator.SetBool("isWalking", true);
    public void isWalkingAnimFalse() => animator.SetBool("isWalking", false);

    public void onShieldAnimActive() => animator.SetBool("habilidad1", true);
    public void onShieldAnimFalse() => animator.SetBool("habilidad1", false);



    public void onUltimateAnimActive() => animator.SetTrigger("habilidad3");
    public void inJumpAnimActive() => animator.SetTrigger("inJump");
    public void bombShootAnimActive() => animator.SetTrigger("habilidad2");
    

    public void onShootAnimActive() => animator.SetTrigger("onShoot");


}
