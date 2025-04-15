using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpellCanvasUI : MonoBehaviour
{
    public SpellSlotUI[] spellSlots;

    public Dictionary<SpellInfo, SpellSlotUI> spellSlotsDict;

    public SpellCaster spellManager;

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

    public void HighlightSelectedSpell(SpellInfo s)
    {
        foreach (SpellSlotUI slot in spellSlots)
        {
            if (slot.currentSpell == s)
            {
                slot.HighlightSpell();
            }
        }
    }

    public void Dehighlight(SpellInfo s)
    {
        foreach (SpellSlotUI slot in spellSlots)
        {
            if (slot.currentSpell == s)
            {
                slot.DisableHighlight();
            }
        }
    }

    public void DehighlightAll()
    {
        foreach (SpellSlotUI slot in spellSlots)
        {
            slot.DisableHighlight();
        }
    }

    public void FillSlot(int i, SpellInfo spell)
    {
        if (i < 0 || i > 3)
        {
            return;
        }

        spellSlots[i].SetSpell(spell);
    }

    SpellSlotUI FindSlot(SpellInfo s)
    {
        foreach (SpellSlotUI slot in spellSlots)
        {
            if (slot.currentSpell == s)
            {
                return slot;
            }
        }
        return null;
    }
}




