using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public Spell[] spells = new Spell[4];

    public SpellCanvas spellCanvas;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Spell s in spells)
        {

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
