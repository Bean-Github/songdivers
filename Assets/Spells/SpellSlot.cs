using UnityEngine;
using UnityEngine.UI;

public class SpellSlot : MonoBehaviour
{
    public Image image;

    public Image cooldownOverlay;

    public Spell currentSpell;

    public Image highlightOverlay;

    private void Update()
    {
        if (currentSpell != null)
        {
            if (!currentSpell.CanUse)
            {
                cooldownOverlay.enabled = true;
                cooldownOverlay.fillAmount = 1f - Mathf.Clamp01(currentSpell.CurrTimeRemaining / currentSpell.cooldown);
            }
            else
            {
                cooldownOverlay.enabled = false;
            }
        }
    }

    public void SetSpell(Spell spell)
    {
        image.sprite = spell.baseData.uiSprite;

        currentSpell = spell;
    }

    public void HighlightSpell()
    {
        highlightOverlay.enabled = true;
    }

    public void DisableHighlight()
    {
        highlightOverlay.enabled = false;
    }
}
