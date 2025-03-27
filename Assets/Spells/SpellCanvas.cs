using UnityEngine;
using UnityEngine.UI;

public class SpellCanvas : MonoBehaviour
{
    public SpellSlot[] spellSlots;

    public SpellManager spellManager;

    public Slider manaSlider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        manaSlider.maxValue = spellManager.manaMax;

        manaSlider.value = spellManager.currentMana;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateMana();
    }

    public void UpdateMana()
    {
        manaSlider.value = spellManager.currentMana;
    }

    public void HighlightSelectedSpell(Spell s)
    {
        foreach (SpellSlot slot in spellSlots)
        {
            if (slot.currentSpell == s)
            {
                slot.HighlightSpell();
            }
            else
            {
                slot.DisableHighlight();
            }
        }
    }

    public void FillSlot(int i, Spell spell)
    {
        if (i < 0 || i > 3)
        {
            return;
        }

        spellSlots[i].SetSpell(spell);
    }

    SpellSlot FindSlot(Spell s)
    {
        foreach (SpellSlot slot in spellSlots)
        {
            if (slot.currentSpell == s)
            {
                return slot;
            }
        }
        return null;
    }
}
