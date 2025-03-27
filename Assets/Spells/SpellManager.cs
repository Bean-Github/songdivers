using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform mouseLocation;

    public float currentMana;

    public float manaRegen = 10f;

    public float manaMax = 100f;

    public SpellData[] spellData = new SpellData[4];

    public Spell[] spells = new Spell[4];

    public string[] spellKeyBinds;

    public SpellCanvas spellCanvas;

    [HideInInspector()]
    public Rigidbody playerRB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            SpellData sd = spellData[i];

            if (sd == null)
                continue;

            spells[i] = new Spell(spellData[i]);

            spellCanvas.FillSlot(i, spells[i]);
        }
    }

    public bool isCasting;

    void Update()
    {
        // mana
        currentMana += manaRegen * Time.deltaTime;

        if (currentMana > manaMax)
        {
            currentMana = manaMax;
        }

        ManageSpells();

        // mouse pos
        SetMousePos();

        // current spell stuff
        isCasting = selectedSpell != null;

        if (isCasting)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (TryCastSelectedSpell())
                {
                    selectedSpell = null;
                }
            }
        }
    }

    void ManageSpells()
    {
        // spells
        for (int i = 0; i < spellKeyBinds.Length; i++)
        {
            if (spells[i] == null)
                continue;

            if (Input.GetButtonDown(spellKeyBinds[i]))
            {
                SwitchSpell(spells[i]);
            }

            spells[i].Update();
        }
    }

    void SetMousePos()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create ray from camera to mouse position
        RaycastHit hit; //Create raycast hit struct

        // Check if the raycast hit something
        if (Physics.Raycast(ray, out hit, Mathf.Infinity))
        {
            // Check if hit a object with a collider
            if (hit.collider != null)
            {
                mouseLocation.position = hit.point;
            }
        }
    }

    Spell selectedSpell;

    // SPELL CASTING
    public void SwitchSpell(Spell s)
    {
        if (s == selectedSpell)
        {
            selectedSpell = null;
        }

        if (s.CanUse)
        {
            selectedSpell = s;

            spellCanvas.HighlightSelectedSpell(s);
        }
    }

    public bool TryCastSelectedSpell()
    {
        Spell s = selectedSpell;

        if (s == null)
            return false;

        if (s.manaCost > currentMana)
            return false;


        if (s.baseData.useDefaultSpawn)
        {
            Vector3 flatMousePos = GetFlatMousePos(spawnPoint.position);

            GameObject newSpellObj = Instantiate(s.baseData.spellObject, spawnPoint.position, Quaternion.identity);

            newSpellObj.transform.forward = flatMousePos - newSpellObj.transform.position;

            newSpellObj.GetComponent<SpellInstance>().originalRB = playerRB;
            newSpellObj.GetComponent<SpellInstance>().attachedSpell = s;
        }

        s.StartCooldown();

        currentMana -= s.manaCost;

        spellCanvas.HighlightSelectedSpell(null);

        return true;
    }

    public Vector3 GetFlatMousePos(Vector3 flatPivot)
    {
        return new Vector3(mouseLocation.position.x, flatPivot.y, mouseLocation.position.z);
    }

}
