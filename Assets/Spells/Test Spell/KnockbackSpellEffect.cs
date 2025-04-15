using UnityEngine;

public class KnockbackSpellEffect : SpellEffect
{
    public float force;

    public override void ApplyEffectOnEnemy(EntityHealth enemyHealth)
    {
        enemyHealth.attachedRB.AddForce(transform.forward * force, ForceMode.Impulse);
    }
}
