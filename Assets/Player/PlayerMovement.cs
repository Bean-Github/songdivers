using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Values")]
    public float moveSpeed;
    public float acceleration;
    public float deceleration;
    public float dashSpeed;
    public float dashDistance;
    public float dashCooldown;
    public float collisionMoveDistance;
    public LayerMask layerMask;
    private CharacterController cc; 

    [Header("References")]
    public Animator animator;
    public Rigidbody rb;
    public MeshTrail meshTrail;
    public SpellManager spellManager;

    [Header("Info Variables")]
    public Vector3 moveDir = Vector3.zero;
    private Vector3 move;
    public bool canDash;
    public bool isCastingSpell;
    private float currSpeed;

    public Vector3 velocity;
    public float groundCheckDistance;
    public bool isGrounded; 
    public float gravityForce;


    // Start is called before the first frame update
    void Start()
    {
        canDash = true;

        spellManager.playerRB = rb;
        cc = this.GetComponent<CharacterController>();
    }

    void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float sidewaysInput = Input.GetAxisRaw("Horizontal");

        // Rotation
        if (spellManager.isCasting)
        {
            transform.forward = spellManager.GetFlatMousePos(transform.position) - transform.position;
        }
        else
        {
            transform.forward = moveDir;
        }

        // Movement
        bool isWalking = verticalInput != 0f || sidewaysInput != 0f;

        if (isWalking)
        {
            currSpeed += acceleration * Time.deltaTime;

            moveDir = (verticalInput * Vector3.forward + sidewaysInput * Vector3.right).normalized;
            moveDir = Quaternion.AngleAxis(-45, Vector3.up) * moveDir;
        }
        else
        {
            currSpeed -= deceleration * Time.deltaTime;
        }

        currSpeed = Mathf.Clamp(currSpeed, 0f, moveSpeed);

        // if (!Physics.Raycast(transform.position, moveDir, collisionMoveDistance, layerMask))
        // {
        //     transform.position += moveDir * currSpeed * Time.deltaTime;
        // }
        ApplyGravity();
        Vector3 dir = moveDir * currSpeed * Time.deltaTime;
        move = new Vector3(dir.x, 0, dir.z);
        cc.Move(move);
        cc.Move(velocity);


        // Dashing
        if ((Input.GetButtonDown("Dash") || Input.GetKeyDown(KeyCode.LeftShift)) && canDash)
        {
            Dash();
        }
    }

    void Dash()
    {
        Vector3 originalPos = transform.position;
        Vector3 target = transform.position + dashDistance * moveDir;

        // target dash position

        StartCoroutine(DashMovement(originalPos, target));

        meshTrail.ActivateTrail();

        StartCoroutine(DashCooldown());
    }

    void ApplyGravity()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, layerMask);
        if (isGrounded) {
            velocity = new Vector3(0f, -0.1f, 0f);
        } else {
            velocity.y -= gravityForce * Time.deltaTime; // Apply gravity
        }
        cc.Move(velocity * Time.deltaTime);
    }

    private IEnumerator DashMovement(Vector3 start, Vector3 target)  // s is start, t is target
    {
        Vector3 dir = (target - start).normalized;

        while (Vector3.Distance(transform.position, start) < dashDistance)
        {
            cc.Move(dir * dashSpeed * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }
    }

    private IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

}
