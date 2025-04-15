using UnityEngine;
using static SpellDataSO;

[System.Serializable]
// This class will manage the actual in-game maintenance of a spell, instantiated given an SO of base data
public class SpellInfo : ScriptableObject
{
    // these are the values that can possibly change
    private float _currTimeRemaining;

    public float cooldown;

    public float manaCost;

    public float shotSize;

    public float shotSpeed;

    public float damage;

    public float spellRange;

    public SpellDataSO baseData;  // reference to the base data

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

    public GameObject SpellEffect
    {
        get
        {
            return baseData.spellEffect;
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
    #region Getters
    public virtual float GetDamage()
    {
        return damage;
    }

    public virtual float GetShotSpeed()
    {
        return shotSpeed;
    }

    public virtual float GetShotSize()
    {
        return shotSize;
    }

    public virtual float GetSpellRange()
    {
        return spellRange;
    }
    #endregion

    public virtual SpellEffect CreateSpellEffect(SpellCombiner combiner)
    {
        SpellEffect createdSpellEffect = Instantiate(SpellEffect).GetComponent<SpellEffect>();

        if (baseData.useDefaultSpawn)
        {
            createdSpellEffect.transform.parent = combiner.transform;
        }

        createdSpellEffect.spellInfo = this;

        StartCooldown();

        return createdSpellEffect;
    }


    public SpellInfo(SpellDataSO sd)
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





