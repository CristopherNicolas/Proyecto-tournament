using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.Netcode;
using System.Linq;

public class FirstPersonMovement : NetworkBehaviour
{
    public float speed = 5;

    [Header("Running")]
    public bool canRun = true;
    public bool IsRunning { get; private set; }
    public float runSpeed = 9;
    public KeyCode runningKey = KeyCode.LeftShift;

    Rigidbody rb;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    void Awake()
    {
        // Get the rigidbody on this.
        rb = GetComponent<Rigidbody>();
    }
    public  override void OnNetworkSpawn()
    {
        if (IsHost || IsServer)
            StartCoroutine(DestroyListeners());
        base.OnNetworkSpawn();
    }
    IEnumerator DestroyListeners()
    {
       //destruir audioslisteners
        yield break;
    }
    void FixedUpdate()
    {
        if (!IsOwner)
        {
            transform.GetChild(0).GetComponent<Camera>().enabled = false;
            enabled = false;
        return;
        }
        // Update IsRunning from input.
        IsRunning = canRun && Input.GetKey(runningKey);

        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2( Input.GetAxis("Horizontal") * targetMovingSpeed, Input.GetAxis("Vertical") * targetMovingSpeed);

        // Apply movement.
        rb.velocity = transform.rotation * new Vector3(targetVelocity.x, rb.velocity.y, targetVelocity.y);
    }
}