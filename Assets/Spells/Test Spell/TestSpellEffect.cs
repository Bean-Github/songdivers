using UnityEngine;

public class TestSpellEffect : SpellEffect
{
    public override void ApplyEffectOnEnemy(EntityHealth enemyHealth)
    {
        print("test spell effect has hit!");
    }
}
