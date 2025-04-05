using UnityEngine;

public interface IDamagePlayer
{
    
    public DamageType type {get;}
    public float Damage {get; set;}
    public void OnDamagePlayer(Rigidbody playerRB);

    public Rigidbody IgnoreBody {get; }
    public bool canDamage {get; }
}

public enum DamageType
{
   Instance,
    DOT
}
