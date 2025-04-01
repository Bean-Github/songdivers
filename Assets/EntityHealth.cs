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
        print("kanye west");
        currHP -= damage;
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        // if ((ignoreLayers & (1 << other.gameObject.layer)) != 0)
        // {
        //     return;
        // }

        IDamagePlayer damagePlayer;

        if (other.TryGetComponent(out damagePlayer))
        {
        print("HI");
            if (damagePlayer.IgnoreBody != attachedRB)
            {

        print("match my freak");

                this.TakeDamage(damagePlayer.Damage);

                damagePlayer.OnDamagePlayer(attachedRB);
            }
        }
    }
}
