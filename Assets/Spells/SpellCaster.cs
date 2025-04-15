using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

// attached to players and anyone who can cast spells
public class SpellCaster : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform mouseLocation;

    public float currentMana;

    public float manaRegen = 10f;

    public float manaMax = 100f;

    public SpellDataSO[] spellData = new SpellDataSO[4];

    public SpellInfo[] spells = new SpellInfo[4];

    public string[] spellKeyBinds;

    public SpellCanvasUI spellCanvas;

    public GameObject baseSpellObject;

    [HideInInspector()]
    public Rigidbody attachedRB;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        for (int i = 0; i < 4; i++)
        {
            if (spellData[i] == null)
            {
                continue;
            }

            InsertSpellData(i, spellData[i]);
        }
    }

    public void InsertSpellData(int i, SpellDataSO spellData)
    {
        SpellInfo newSpellInfo;

        // create a spell info for each spellData
        switch (spellData.spellType)
        {
            default:
                newSpellInfo = new SpellInfo(spellData);
                break;
        }

        SetSpellInfo(i, newSpellInfo);
    }

    public void SetSpellInfo(int i, SpellInfo newSpellInfo)
    {
        spellCanvas.FillSlot(i, newSpellInfo);

        spellCanvas.spellSlots[i].currentSpell = newSpellInfo;

        spells[i] = newSpellInfo;
    }

    public bool IsCasting
    {
        get { return numSelected > 0; }
    }

    #region Update Functions
    void Update()
    {
        UpdateMana();
        UpdateSpells();

        // mouse pos
        SetMousePos();

        // current spell stuff
        if (Input.GetMouseButtonDown(0))
        {
            TryCastSelectedSpells();

            spellCanvas.DehighlightAll();
        }
    }

    void UpdateMana()
    {
        // mana
        currentMana += manaRegen * Time.deltaTime;

        if (currentMana > manaMax)
        {
            currentMana = manaMax;
        }
    }

    void UpdateSpells()
    {
        // spells
        for (int i = 0; i < spellKeyBinds.Length; i++)
        {
            if (spells[i] == null)
                continue;

            if (Input.GetButtonDown(spellKeyBinds[i]))
            {
                ToggleSpellSelection(i);
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
    #endregion

    SpellInfo[] selectedSpells = new SpellInfo[4];

    int numSelected = 0;

    // SPELL CASTING
    public void ToggleSpellSelection(int i)
    {
        SpellInfo s = spells[i];

        if (s == null) return;

        // deselect the spell
        if (selectedSpells[i] != null)
        {
            spellCanvas.Dehighlight(s);
            selectedSpells[i] = null;
            numSelected--;
            return;
        }

        // select the spell
        if (s.CanUse)
        {
            selectedSpells[i] = s;
            spellCanvas.HighlightSelectedSpell(s);
            numSelected++;
        }
    }

    public void TryCastSelectedSpells()
    {
        float totalManaCost = 0.0f;
        List<SpellInfo> castedSpellInfos = new List<SpellInfo>();

        // create a new base spell object
        for (int i = 0; i < 4; i++)
        {
            SpellInfo s = selectedSpells[i];
            if (s == null)
                continue;

            totalManaCost += s.manaCost;

            castedSpellInfos.Add(s);
        }

        bool canCast = totalManaCost <= currentMana && numSelected > 0;

        if (!canCast)
        {
            // do something to show you can't cast

            return;
        }

        // actually cast the spell
        Vector3 flatMousePos = GetFlatMousePos(spawnPoint.position);
        GameObject newSpellObj = Instantiate(baseSpellObject, spawnPoint.position, Quaternion.identity);
        newSpellObj.transform.forward = flatMousePos - newSpellObj.transform.position;


        SpellInstance spellInstance = newSpellObj.GetComponent<SpellInstance>();

        spellInstance.Initialize(castedSpellInfos, attachedRB);

        currentMana -= totalManaCost;

        ResetSpellCast();
    }

    protected void ResetSpellCast()
    {
        for (int i = 0; i < 4; i++)
        {
            selectedSpells[i] = null;
        }

        numSelected = 0;
    }


    public Vector3 GetFlatMousePos(Vector3 flatPivot)
    {
        return new Vector3(mouseLocation.position.x, flatPivot.y, mouseLocation.position.z);
    }

}



