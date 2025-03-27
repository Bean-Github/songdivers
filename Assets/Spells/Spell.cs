using UnityEngine;
using static SpellData;

[System.Serializable]
// This class will manage the actual in-game maintenance of a spell, instantiated given an SO of base data
public class Spell
{
    // these are the values that can possibly change
    private float _currTimeRemaining;

    public float cooldown;

    public float manaCost;

    public float shotSize;

    public float shotSpeed;

    public float damage;

    public float spellRange;

    public SpellData baseData;  // reference to the base data

    public float CurrTimeRemaining
    {
        get
        {
            return _currTimeRemaining;
        }
        set
        {
            _currTimeRemaining = value;
        }
    }

    public bool CanUse
    {
        get
        {
            return _currTimeRemaining >= cooldown;
        }
    }

    public void Update()
    {
        _currTimeRemaining += Time.deltaTime;
    }

    public void StartCooldown()
    {
        _currTimeRemaining = 0f;
    }
    
    // getters for getting scaled values based on buffs and stuff
    public float GetDamage()
    {
        return damage;
    }

    public float GetShotSpeed()
    {
        return shotSpeed;
    }

    public float GetShotSize()
    {
        return shotSize;
    }

    public float GetSpellRange()
    {
        return spellRange;
    }

    public Spell(SpellData sd)
    {
        cooldown = sd.baseCooldown;
        shotSize = sd.baseShotSize;
        shotSpeed = sd.baseShotSpeed;

        manaCost = sd.baseManaCost;

        CurrTimeRemaining = cooldown;

        baseData = sd;

        spellRange = sd.baseSpellRange;
        damage = sd.baseDamage;
    }
}
