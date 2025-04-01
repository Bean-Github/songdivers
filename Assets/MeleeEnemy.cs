using UnityEngine;
using UnityEngine.AI;
public class MeleeEnemy : Enemy
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private NavMeshAgent agent;
    public float aggroRange = 10f;
    public Animator anim;
    private Vector3 dest;
    private GameObject player;

    [Header("Attack")]
    private bool hasHit;
    public Transform model;
    public GameObject hurtBox;
    
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
                    agent.enabled = false;
                    //agent.destination = this.transform.position;
                    anim.SetTrigger("Attack");
                    // Transform hb = Instantiate(hurtBox, this.transform).transform;
                    // hb.position = hb.position + transform.forward;
                    // hb.GetComponent<HurtBox>().Set(1f, attackDamage);
                    // hb.localScale = new Vector3 (1.5f, 2f, 1.5f);
                    isAttacking = true;
                    lastAttack = Time.time;
                }
            }
        }

        if (isAttacking) {
            transform.LookAt(player.transform.position);
        }
        
        //Return to walking
        if (Time.time > lastAttack + attackDur && isAttacking) {
            agent.enabled = true;
            Vector3 save = model.transform.position;
            //model.transform.position = this.transform.position;
            agent.Warp(save);
            model.transform.position = save;
            anim.ResetTrigger("Attack");
            isAttacking = false;
            hasHit = false;
        }

    }
}
