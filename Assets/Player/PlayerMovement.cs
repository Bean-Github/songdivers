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

    [Header("Info Variables")]
    public Vector3 moveDir = Vector3.zero;

    public bool canDash;

    private float currSpeed;

    // Start is called before the first frame update
    void Start()
    {
        canDash = true;
    }

    private void Update()
    {
        float verticalInput = Input.GetAxisRaw("Vertical");
        float sidewaysInput = Input.GetAxisRaw("Horizontal");

        transform.forward = moveDir;

        bool isWalking = verticalInput != 0f || sidewaysInput != 0f;

        if (isWalking)
        {
            currSpeed += acceleration * Time.deltaTime;

            //animator.Play("PlayerWalk");
            moveDir = (verticalInput * Vector3.forward + sidewaysInput * Vector3.right).normalized;
        }
        else
        {
            currSpeed -= deceleration * Time.deltaTime;
            //transform.forward = -Vector3.forward;
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
