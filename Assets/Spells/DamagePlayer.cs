using UnityEngine;

public interface IDamagePlayer
{
    public float Damage { get; }

    public void OnDamagePlayer();

    public Rigidbody IgnoreBody { get; }
}
