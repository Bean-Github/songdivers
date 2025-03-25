using UnityEngine;

[CreateAssetMenu(fileName = "NewSpell", menuName = "Custom/Spell")]
public class Spell : ScriptableObject
{
    public SpellType spellType;

    public string spellName;

    public float baseCooldown;

    public float manaCost;

    public float spellSize = 1f;

    public Sprite uiSprite;

    public bool useDefaultSpawn;

    public GameObject spellObject;

    public enum SpellType
    {
        Default
    }
}
