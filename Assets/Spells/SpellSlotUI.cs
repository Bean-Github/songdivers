using UnityEngine;
using UnityEngine.UI;

public class SpellSlotUI : MonoBehaviour
{
    public Image image;

    public Image cooldownOverlay;

    public SpellInfo currentSpell;

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

    public void SetSpell(SpellInfo spell)
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
