using UnityEngine;

public interface IDamagePlayer
{
    public float Damage { get; }

    public void OnDamagePlayer(Rigidbody playerRB);

    public Rigidbody IgnoreBody { get; }
}
