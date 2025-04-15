using UnityEngine;
using System.Collections.Generic;

// deals with spell combining logic. This is a default spell combiner, there is room for different kinds
public class SpellCombiner : MonoBehaviour
{
    public List<SpellEffect> spellEffects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public virtual void AddSpellInfo(SpellInfo s)
    {
        SpellEffect newSpellEffect = s.CreateSpellEffect(this);

        spellEffects.Add(newSpellEffect);
    }

    // sums up stuff
    public void CalculateAccumulatedValues()
    {
        dmg = 0.0f;
        spellRange = 0.0f;
        shotSpeed = 0.0f;

        foreach (SpellEffect spellEffect in spellEffects)
        {
            dmg += spellEffect.spellInfo.GetDamage();
            spellRange += spellEffect.spellInfo.GetSpellRange();
            shotSpeed += spellEffect.spellInfo.GetDamage();
        }
    }

    public void ApplyEffectsOnEnemy(EntityHealth enemyHealth)
    {
        foreach (SpellEffect spellEffect in spellEffects)
        {
            spellEffect.ApplyEffectOnEnemy(enemyHealth);
        }
    }

    protected float dmg;

    protected float shotSpeed;

    protected float spellRange;

    public float GetDamage()
    {
        return dmg;
    }

    public float GetShotSpeed()
    {
        return shotSpeed;
    }

    public float GetSpellRange()
    {
        return spellRange;
    }
}




