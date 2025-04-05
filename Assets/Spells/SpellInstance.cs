using UnityEngine;

public class SpellInstance : MonoBehaviour, IDamagePlayer
{
    public Spell attachedSpell;
    public Rigidbody originalRB;

    public virtual void Start()
    {
        // ignore collisions or something
    }
    public DamageType type {
        get {return DamageType.Instance;}
    }

    public float Damage {
        get {return attachedSpell.GetDamage();}
        set {attachedSpell.damage = value;}
    }

    public Rigidbody IgnoreBody
    {
        get {return originalRB;}
    }

    public bool canDamage
    {
        get{return true;}
    }

    public virtual void OnDamagePlayer(Rigidbody playerRB)
    {

    }
}
