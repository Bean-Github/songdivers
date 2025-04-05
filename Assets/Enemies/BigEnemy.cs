using UnityEngine;
using UnityEngine.AI;
public class BigEnemy : Enemy
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
    public float dashSpeed;
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
        agent.speed = baseSpeed;
        if (isAggro) {
            dest = player.transform.position;
            if ((this.transform.position - player.transform.position).magnitude > range && !isAttacking) {
                agent.destination = player.transform.position;                
            } else if (!isAttacking) {
                if (Time.time > lastAttack + attackCD) {
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

        if (Time.time < lastAttack + attackWindUp && isAttacking) {
            agent.speed = 0;
        } else if (Time.time > lastAttack + attackEndLag && isAttacking) {
            agent.speed = 0;
        } else if (isAttacking) {
            agent.speed = dashSpeed;
            anim.SetFloat("Walking", 0);  
            agent.destination = player.transform.position;  
        } else {
            agent.speed = baseSpeed;
            model.transform.localPosition = new Vector3(0,0,0); 
        }
        
        //Return to walking
        if (Time.time > lastAttack + attackDur && isAttacking) {
            Vector3 save = model.transform.position;
            agent.Warp(save);
            model.transform.position = save;
            anim.ResetTrigger("Attack");
            isAttacking = false;
        }

    }
}
