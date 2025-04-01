using UnityEngine;

public interface IDamagePlayer
{
    public float Damage { get; set;}

    public void OnDamagePlayer();

    public Rigidbody IgnoreBody { get; }
}
