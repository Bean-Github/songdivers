using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EntityHealth : MonoBehaviour
{
    public float maxHP;
    public float currHP;

    public Rigidbody attachedRB;
    public LayerMask ignoreLayers;

    public virtual void TakeDamage(float damage)
    {
        currHP -= damage;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        IDamagePlayer damagePlayer;
        if (other.TryGetComponent(out damagePlayer))
        {
            if (damagePlayer.type == DamageType.Instance) {
                if (damagePlayer.IgnoreBody != attachedRB && damagePlayer.canDamage) {
                    this.TakeDamage(damagePlayer.Damage);
                    damagePlayer.OnDamagePlayer(attachedRB);
                }
            }
        }
            
    }

    protected virtual void OnTriggerStay(Collider other) {
        IDamagePlayer damagePlayer;
        if (other.TryGetComponent(out damagePlayer))
        {
            if (damagePlayer.type != DamageType.DOT) {
                return;
            }
            if (damagePlayer.IgnoreBody != attachedRB && damagePlayer.canDamage)
            {
                this.TakeDamage(damagePlayer.Damage);
                damagePlayer.OnDamagePlayer(attachedRB);
            }
        }
    }
}
