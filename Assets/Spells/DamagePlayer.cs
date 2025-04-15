using UnityEngine;

public interface IDamagePlayer
{
    public DamageType type {get;}
    public float Damage {get;}
    public void OnDamageEntity(EntityHealth entityHealth);

    public Rigidbody IgnoreBody {get; }
    public bool canDamage {get; }
}

public enum DamageType
{
   Instance,
    DOT
}


