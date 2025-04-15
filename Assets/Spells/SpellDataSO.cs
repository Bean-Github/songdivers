using UnityEngine;

// Stores default (base) data
[CreateAssetMenu(fileName = "NewSpell", menuName = "Custom/Spell")]
public class SpellDataSO : ScriptableObject
{
    public SpellType spellType;

    public string spellName;

    public float baseCooldown = 5f;

    public float baseManaCost;

    public float baseShotSize = 1f;

    public float baseShotSpeed = 30f;

    public float baseSpellRange = 10f;

    public float baseDamage = 10f;

    public Sprite uiSprite;

    public bool useDefaultSpawn = true;

    public GameObject spellEffect;

    public enum SpellType
    {
        Default
    }
}
