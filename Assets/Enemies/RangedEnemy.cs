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
    public float attackWindUp;
    public float attackEndLag;
    public float baseSpeed;
    public float baseTurnSpeed;
    public GameObject laser;
    public LayerMask sight;
    public Transform model;
    private bool laserActive;
    private Quaternion laserRot;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        //agent.destination = this.transform.position;
        laserActive = false;
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
                } else if (los && Time.time > lastAttack + attackCD && !isAttacking) {
                    Vector3 dir = player.transform.position + player.transform.up * 0.6f - this.transform.position;
                    laserRot = Quaternion.LookRotation(dir);
                    agent.enabled = false;
                    transform.LookAt(player.transform);
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

        if (Time.time < lastAttack + attackWindUp && isAttacking) {
            agent.speed = 0;
        } else if (Time.time > lastAttack + attackEndLag && isAttacking) {
            agent.speed = 0;
        } else if (isAttacking) {
            if (!laserActive) {
                GameObject dotHurtBox = Instantiate(laser, this.transform.position + transform.forward * 0.7f + transform.up * 0.6f, laserRot);
                dotHurtBox.transform.GetChild(0).GetChild(0).GetComponent<DotHurtBox>().Damage = attackDamage;
                laserActive = true;
            }
            anim.SetFloat("Walking", 0);  
            agent.destination = player.transform.position;  
        } else {
            agent.speed = baseSpeed;
            model.transform.localPosition = new Vector3(0,0,0); 
        }
        //Return to walking
        if (Time.time > lastAttack + attackDur && isAttacking) {
            agent.enabled = true;
            Vector3 save = model.transform.position;
            agent.Warp(save);
            model.transform.position = save;
            anim.ResetTrigger("Attack");
            isAttacking = false;
            laserActive = false;
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
