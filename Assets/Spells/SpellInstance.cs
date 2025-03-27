using UnityEngine;

public class SpellInstance : MonoBehaviour, IDamagePlayer
{
    public Spell attachedSpell;

    public Rigidbody originalRB;

    public virtual void Start()
    {
        // ignore collisions or something
    }

    public float Damage
    {
        get
        {
            return attachedSpell.GetDamage();
        }
    }

    public Rigidbody IgnoreBody
    {
        get
        {
            return originalRB;
        }
    }

    public virtual void OnDamagePlayer()
    {

    }
}
