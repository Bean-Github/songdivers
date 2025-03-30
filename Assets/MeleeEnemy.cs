using UnityEngine;
using UnityEngine.AI;
public class MeleeEnemy : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private NavMeshAgent agent;
    
    public float aggroRange = 10f;
    public Animator anim;
    public bool isAggro;
    private Vector3 dest;
    private GameObject player;

    [Header("Attack")]
    public float range;
    public bool isAttacking;
    private float lastAttack;
    public float attackDur;
    public float attackCD;
    public float attackDamage;
    
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
        print(agent.velocity.magnitude);
        agent.destination = player.transform.position;
        
        isAggro = (this.transform.position - player.transform.position).magnitude < aggroRange;

        if (isAggro) {
            dest = player.transform.position;
            if ((this.transform.position - player.transform.position).magnitude > range && !isAttacking) {
                agent.destination = player.transform.position;                
            } else if (!isAttacking) {
                if (Time.time > lastAttack + attackCD) {
                    agent.destination = this.transform.position;
                    anim.SetTrigger("Attack");
                    isAttacking = true;
                    lastAttack = Time.time;
                }
            }
        }
        
        if (Time.time > lastAttack + attackDur && isAttacking) {
            anim.ResetTrigger("Attack");
            isAttacking = false;
        }

    }

    // void OnCollisionEnter(Collision collision)
    // {
    //     if (collision.gameObject.tag == "player" && isAttacking) {
    //         player.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
    //     }
    // }

}
