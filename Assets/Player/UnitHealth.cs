using UnityEngine;

public class UnitHealth
{
    private float _currentHealth;
    private float _currentMaxHealth;

    public float Health
    {
        get
        {
            return _currentHealth;
        }
        set
        {
            _currentHealth = value;
        }
    }

    public float MaxHealth
    {
        get
        {
            return _currentMaxHealth;
        }
        set
        {
            _currentMaxHealth = value;
        }
    }

    public UnitHealth(int health, int maxHealth)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHealth;
    }

    public void DamageUnit(float dmg)
    {
        _currentHealth -= dmg;

        if (_currentHealth <= 0f)
        {
            // die
        }
    }

    public void HealUnit(float heal)
    {
        if (_currentHealth < _currentMaxHealth)
        {
            _currentHealth += heal;
        }

        if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }
    }
}
