using UnityEngine;

public class DotHurtBox : MonoBehaviour, IDamagePlayer
{
    private float startTime;
    public float dur;
    public GameObject gameObject;
    private float lastTick;
    private bool hasHit;
    private float damage;

    void Start()
    {
        startTime = Time.time;
        lastTick = 0f;
    }
    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + dur) {
            Destroy(gameObject);
        }
    }

    public float Damage {
        get {return damage;}
        set {damage = value;}
    }
    public bool canDamage
    {
        get {
            return Time.time > lastTick + 0.125f;
        }
    }
    public DamageType type {
        get {return DamageType.DOT;}
    }
    public Rigidbody IgnoreBody {
        get {return null;}
    }
    public void OnDamagePlayer(Rigidbody rb) {
        lastTick = Time.time;
        print(lastTick);
    }
}
