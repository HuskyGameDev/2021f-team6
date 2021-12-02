using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    //Gold Amount
    public int Gold = 1000;
    public Text GoldText;

    //Elemental Spells
    public Text[] ESpellCost = new Text[4];
    public bool[] ESpellOwned = new bool[4];

    //Ability Upgrades
    public Text[] UpgradeAmount = new Text[2];
    public Text[] UpgradeCost = new Text[2];
    private int[] UpgradeCostInt = new int[2];
    public int[] UpgradeOwned = new int[2];

    //Power Spells
    public Text[] PSpellCost = new Text[10];
    public bool[] PSpellOwned = new bool[10];

    // Start is called before the first frame update
    void Start()
    {
        StoreDefault();
    }

    // Update is called once per frame
    void Update()
    {
        GoldText.text = "Gold: " + Gold;
    }

    //Restores Store Default values
    public void StoreDefault()
    {
        Gold = 0;
        //Upgrade defaults
        for (int i = 0; i < 2; i++)
        {
            UpgradeOwned[i] = 0;
            UpgradeAmount[i].text = UpgradeOwned[i] + "/10";
            UpgradeCostInt[i] = 100;
            UpgradeCost[i].text = 100 + "gp";
        }
        //Elemental Spell defaults
        for(int i = 0; i < 4; i++)
        {
            ESpellCost[i].text = "100 gp";
            ESpellOwned[i] = false;
        }
        //Power Spell defaults
        for (int i = 0; i < 10; i++)
        {
            PSpellCost[i].text = "100 gp";
            PSpellOwned[i] = false;
        }
    }

    //Ability Upgrade Methods----------------------------------
    public void health()
    {
        //Health is Upgrade[0]
        if(UpgradeOwned[0] < 10 && Gold >= UpgradeCostInt[0]) //if not full and can buy
        {
            UpgradeOwned[0] += 1;
            UpgradeAmount[0].text = UpgradeOwned[0] + "/10";
            Gold -= UpgradeCostInt[0];
            UpgradeCostInt[0] += 50;
            UpgradeCost[0].text = UpgradeCostInt[0] + "gp";
        }
    }
    public void magica()
    {
        //Magica is Upgrade[1]
        if (UpgradeOwned[1] < 10 && Gold >= UpgradeCostInt[1]) //if not full and can buy
        {
            UpgradeOwned[1] += 1;
            UpgradeAmount[1].text = UpgradeOwned[1] + "/10";
            Gold -= UpgradeCostInt[1];
            UpgradeCostInt[1] += 50;
            UpgradeCost[1].text = UpgradeCostInt[1] + "gp";
        }
    }

    //Elemental Spell Methods----------------------------------
    public void Ice()
    {
        //Ice spell is ESpell[0]
        if (!ESpellOwned[0] && Gold >= 100) //if not owned and can buy
        {
            ESpellCost[0].text = "Owned";
            Gold -= 100;
            ESpellOwned[0] = true;
        }
    }
    public void Earth()
    {
        //Earth spell is ESpell[1]
        if (!ESpellOwned[1] && Gold >= 100) //if not owned and can buy
        {
            ESpellCost[1].text = "Owned";
            Gold -= 100;
            ESpellOwned[1] = true;
        }
    }
    public void Lighting()
    {
        //Lighting spell is ESpell[2]
        if (!ESpellOwned[2] && Gold >= 100) //if not owned and can buy
        {
            ESpellCost[2].text = "Owned";
            Gold -= 100;
            ESpellOwned[2] = true;
        }
    }
    public void Fire()
    {
        //Fire spell is ESpell[3]
        if (!ESpellOwned[3] && Gold >= 100) //if not owned and can buy
        {
            ESpellCost[3].text = "Owned";
            Gold -= 100;
            ESpellOwned[3] = true;
        }
    }

    //Power Spell Methods-------------------------------------
    public void Pspell_1()
    {
        if(!PSpellOwned[0] && Gold >= 100)
        {
            PSpellCost[0].text = "Owned";
            Gold -= 100;
            PSpellOwned[0] = true;
        }
    }

    public void Pspell_2()
    {
        if (!PSpellOwned[1] && Gold >= 100)
        {
            PSpellCost[1].text = "Owned";
            Gold -= 100;
            PSpellOwned[1] = true;
        }
    }

    public void Pspell_3()
    {
        if (!PSpellOwned[2] && Gold >= 100)
        {
            PSpellCost[2].text = "Owned";
            Gold -= 100;
            PSpellOwned[2] = true;
        }
    }

    public void Pspell_4()
    {
        if (!PSpellOwned[3] && Gold >= 100)
        {
            PSpellCost[3].text = "Owned";
            Gold -= 100;
            PSpellOwned[3] = true;
        }
    }

    public void Pspell_5()
    {
        if (!PSpellOwned[4] && Gold >= 100)
        {
            PSpellCost[4].text = "Owned";
            Gold -= 100;
            PSpellOwned[4] = true;
        }
    }

    public void Pspell_6()
    {
        if (!PSpellOwned[5] && Gold >= 100)
        {
            PSpellCost[5].text = "Owned";
            Gold -= 100;
            PSpellOwned[5] = true;
        }
    }

    public void Pspell_7()
    {
        if (!PSpellOwned[6] && Gold >= 100)
        {
            PSpellCost[6].text = "Owned";
            Gold -= 100;
            PSpellOwned[6] = true;
        }
    }

    public void Pspell_8()
    {
        if (!PSpellOwned[7] && Gold >= 100)
        {
            PSpellCost[7].text = "Owned";
            Gold -= 100;
            PSpellOwned[7] = true;
        }
    }

    public void Pspell_9()
    {
        if (!PSpellOwned[8] && Gold >= 100)
        {
            PSpellCost[8].text = "Owned";
            Gold -= 100;
            PSpellOwned[8] = true;
        }
    }

    public void Pspell_10()
    {
        if (!PSpellOwned[9] && Gold >= 100)
        {
            PSpellCost[9].text = "Owned";
            Gold -= 100;
            PSpellOwned[9] = true;
        }
    }
}
