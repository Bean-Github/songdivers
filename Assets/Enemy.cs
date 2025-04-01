using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float aggroRange = 10f;
    public bool isAggro;
    public float range;
    public bool isAttacking;
    public float attackDur;
    public float attackCD;
    public float attackDamage;
    protected float lastAttack;
    
    
}
