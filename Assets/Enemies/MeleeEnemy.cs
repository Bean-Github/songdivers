using UnityEngine;
using UnityEngine.AI;
public class MeleeEnemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private NavMeshAgent agent;
    public Animator anim;
    private Vector3 dest;
    private GameObject player;

    [Header("Attack")]
    private bool hasHit;
    public Transform model;
    public GameObject hurtBox;
    public bool attackMove;
    private float saveSpeed;
    
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        hurtBox.GetComponent<HurtBox>().Damage = attackDamage;
        //agent.destination = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Walking", agent.velocity.magnitude);        
        isAggro = (this.transform.position - player.transform.position).magnitude < aggroRange;

        if (isAggro) {
            dest = player.transform.position;
            if ((this.transform.position - player.transform.position).magnitude > range && !isAttacking) {
                agent.destination = player.transform.position;                
            } else if (!isAttacking) {
                if (Time.time > lastAttack + attackCD) {
                    if (!attackMove) {
                        //agent.enabled = false;
                        saveSpeed = agent.speed;
                        agent.speed = 0;
                    }
                    anim.SetTrigger("Attack");
                    print("Attack");
                    isAttacking = true;
                    lastAttack = Time.time;
                }
            }
        }

        if (isAttacking) {
            if (attackMove) {
                agent.destination = player.transform.position; 
            } else {
                Vector3 lookDir = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
                transform.LookAt(lookDir);
            }
            
        }

        if (!isAttacking) {
            model.transform.localPosition = new Vector3(0,0,0);
        }
        
        //Return to walking
        if (Time.time > lastAttack + attackDur && isAttacking) {
            agent.speed = saveSpeed;
            //model.transform.position = save;
            //anim.ResetTrigger("Attack");
            isAttacking = false;
            hasHit = false;
        }

    }
}
