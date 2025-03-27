using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [Header("References")]
    public Animator animator;
    public Rigidbody rb;

    public MeshTrail meshTrail;

    public SpellManager spellManager;

    [Header("Info Variables")]
    public Vector3 moveDir = Vector3.zero;

    public bool canDash;

    public bool isCastingSpell;

    private float currSpeed;

    // Start is called before the first frame update
    void Start()
    {
        canDash = true;

        spellManager.playerRB = rb;
    }

    private void Update()
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

        if (!Physics.Raycast(transform.position, moveDir, collisionMoveDistance, layerMask))
        {
            transform.position += moveDir * currSpeed * Time.deltaTime;
        }

        // Dashing
        if (Input.GetButtonDown("Dash") && canDash)
        {
            Dash();
        }
    }

    void Dash()
    {
        Vector3 originalPos = rb.position;
        Vector3 target = rb.position + dashDistance * moveDir;

        // target dash position

        StartCoroutine(DashMovement(originalPos, target));

        meshTrail.ActivateTrail();

        StartCoroutine(DashCooldown());
    }

    private IEnumerator DashMovement(Vector3 start, Vector3 target)  // s is start, t is target
    {
        Vector3 dir = (target - start).normalized;

        while (Vector3.Distance(transform.position, start) < dashDistance)
        {
            transform.position += dir * dashSpeed * Time.deltaTime;

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
