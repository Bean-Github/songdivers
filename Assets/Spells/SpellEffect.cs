using UnityEngine;

public abstract class SpellEffect : MonoBehaviour
{
    [HideInInspector]
    public SpellInfo spellInfo;

    public virtual void ApplyEffectOnEnemy(EntityHealth enemyHealth)
    {

    }
}








