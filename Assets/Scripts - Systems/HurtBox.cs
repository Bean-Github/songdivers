using UnityEngine;

public class HurtBox : MonoBehaviour, IDamagePlayer 
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float startTime;
    private float dur;
    private bool hasHit;
    private float damage;
    void Start()
    {
        startTime = Time.time;
    }
    // Update is called once per frame
    void Update()
    {

    }

    public float Damage {
        get {return damage;}
        set {damage = value;}
    }
    public bool canDamage
    {
        get{return true;}
    }
    public DamageType type {
        get {return DamageType.Instance;}
    }
    public Rigidbody IgnoreBody {
        get {return null;}
    }
    public void OnDamageEntity(EntityHealth entityHealth) {

    }

}
