using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;
using UnityEngine.UI;
using UnityEngine.Events;

// a basic version of a spell instance
public class BasicProjectile : SpellInstance
{
    public bool destroyOnCollision;

    public LayerMask collisionLayers;

    public Rigidbody rb;

    public float destroyDelay;

    public UnityEvent OnDestroy;

    Vector3 originalPos;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        originalPos = transform.position;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (isDestroying) return;

        transform.position = (transform.position + transform.forward * spellCombiner.GetShotSpeed() * Time.deltaTime);

        if (Vector3.Distance(transform.position, originalPos) > spellCombiner.GetSpellRange())
        {
            DestroyProjectile();
        }
    }

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody == IgnoreBody)
        {
            return;
        }

        if ((collisionLayers & (1 << other.gameObject.layer)) != 0)
        {
            // do something first
            DestroyProjectile();
        }
    }

    public override void OnDamageEntity(EntityHealth entityHealth)
    {
        base.OnDamageEntity(entityHealth);

        DestroyProjectile();
    }

    public bool isDestroying;

    public virtual void DestroyProjectile()
    {
        isDestroying = true;

        OnDestroy.Invoke();

        Destroy(gameObject, destroyDelay);
    }
}



