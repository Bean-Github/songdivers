using UnityEngine;
using UnityEngine.AI;
public class RangedEnemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private NavMeshAgent agent;
    public Animator anim;
    private Vector3 dest;
    private GameObject player;

    [Header("Attack")]
    public LayerMask sight;
    public GameObject projectile;
    private bool hasHit;
    public Transform model;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Walking", agent.velocity.magnitude);        
        float currDist = (this.transform.position - player.transform.position).magnitude;
        isAggro = currDist < aggroRange;
        bool los = canSeePlayer();
        print(los);

        if (isAggro) {
            dest = player.transform.position;
            if (!isAttacking) {
                //Chase if out of range
                if (!los) {
                    print("a");
                    agent.destination = player.transform.position; 
                //Attack if in range & ready               
                } else if (los && Time.time > lastAttack + attackCD) {
                    print("b");
                    agent.enabled = false;
                    transform.LookAt(player.transform);
                    //agent.destination = this.transform.position;
                    anim.SetTrigger("Attack");
                    isAttacking = true;
                    lastAttack = Time.time;
                //Kite Otherwise
                } else {
                    print("c");
                    agent.destination = -2 * (player.transform.position - this.transform.position) + this.transform.position;
                }
            }
        }
        
        //Return to walking
        if (Time.time > lastAttack + attackDur && isAttacking) {
            agent.enabled = true;
            Vector3 save = model.transform.position;
            agent.Warp(save);
            model.transform.position = save;
            anim.ResetTrigger("Attack");
            isAttacking = false;
            hasHit = false;
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" && isAttacking && !hasHit) {
            hasHit = true;
            player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    bool canSeePlayer() {
        RaycastHit hit;
        // Does the ray intersect any objects excluding the player layer
        Vector3 lookDir = (player.transform.position - this.transform.position).normalized;
        if (Physics.Raycast(transform.position, lookDir, out hit, range, sight))
        { 
            Debug.DrawRay(transform.position, lookDir * range, Color.yellow); 
            return hit.collider.gameObject.tag == "Player";
        }
        return false;
    }

}
