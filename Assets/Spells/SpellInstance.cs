using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

// class that interacts with the world
public class SpellInstance : MonoBehaviour, IDamagePlayer
{
    public Rigidbody originalRB;
    public SpellCombiner spellCombiner;

    public virtual void Start()
    {
        // ignore collisions or something
    }
    public DamageType type {
        get {return DamageType.Instance;}
    }

    public float Damage {
        get {return spellCombiner.GetDamage();}
    }

    public Rigidbody IgnoreBody
    {
        get {return originalRB;}
    }

    public bool canDamage
    {
        get {return true;}
    }

    public virtual void OnDamageEntity(EntityHealth entityHealth)
    {
        spellCombiner.ApplyEffectsOnEnemy(entityHealth);
    }

    public virtual void Initialize(List<SpellInfo> spellInfos, Rigidbody originalRB)
    {
        this.originalRB = originalRB;

        foreach (SpellInfo si in spellInfos)
        {
            spellCombiner.AddSpellInfo(si);
        }
        spellCombiner.CalculateAccumulatedValues();
    }
}







