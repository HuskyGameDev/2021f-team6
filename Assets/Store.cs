using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    //Gold Amount
    public static int Gold = 0;
    public Text GoldText;
    public GameObject NeedGoldAlert;

    //Elemental Spells
    public Text[] ESpellCost = new Text[7];
    public static bool[] ESpellOwned = new bool[7];

    //Ability Upgrades
    public Text[] UpgradeAmount = new Text[1];
    public Text[] UpgradeCost = new Text[1];
    private int[] UpgradeCostInt = new int[1];
    public int[] UpgradeOwned = new int[1];

    //Power Spells
    //public Text[] PSpellCost = new Text[10];
    //public bool[] PSpellOwned = new bool[10];

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
        Gold = 150;
        //Upgrade defaults
        for (int i = 0; i < 1; i++)
        {
            UpgradeOwned[i] = 0;
            UpgradeAmount[i].text = UpgradeOwned[i] + "/10";
            UpgradeCostInt[i] = 100;
            UpgradeCost[i].text = 100 + "gp";
        }
        //Elemental Spell defaults
        for(int i = 0; i <= 6; i++)
        {
            ESpellOwned[i] = false;
        }

        ESpellCost[0].text = "150 gp";
        ESpellCost[1].text = "200 gp";
        ESpellCost[2].text = "200 gp";
        ESpellCost[3].text = "100 gp";
        ESpellCost[4].text = "300 gp";
        ESpellCost[5].text = "200 gp";
        ESpellCost[6].text = "300 gp";
        /*//Power Spell defaults
        for (int i = 0; i < 10; i++)
        {
            PSpellCost[i].text = "100 gp";
            PSpellOwned[i] = false;
        }*/
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
        else if (UpgradeOwned[0] < 10 && Gold < UpgradeCostInt[0]) { ShowNeedGoldAlert(); }
    }
    /*public void magica() ---NO LONGER BEING USED---
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
    }*/

    //Elemental Spell Methods----------------------------------
    public void Fireball()
    {
        //Fireball spell is ESpell[0]
        if (!ESpellOwned[0] && Gold >= 150) //if not owned and can buy
        {
            ESpellCost[0].text = "Owned";
            Gold -= 150;
            ESpellOwned[0] = true;
        }
        else if(!ESpellOwned[0] && Gold < 100) { ShowNeedGoldAlert(); }
    }
    public void Lightingbolt()
    {
        //Lightingbolt spell is ESpell[1]
        if (!ESpellOwned[1] && Gold >= 200) //if not owned and can buy
        {
            ESpellCost[1].text = "Owned";
            Gold -= 200;
            ESpellOwned[1] = true;
        }
        else if (!ESpellOwned[1] && Gold < 100) { ShowNeedGoldAlert(); }
    }
    public void Frostwave()
    {
        //Frostwave spell is ESpell[2]
        if (!ESpellOwned[2] && Gold >= 200) //if not owned and can buy
        {
            ESpellCost[2].text = "Owned";
            Gold -= 200;
            ESpellOwned[2] = true;
        }
        else if (!ESpellOwned[2] && Gold < 100) { ShowNeedGoldAlert(); }
    }
    public void IceSpray()
    {
        // Ice Spray spell is ESpell[3]
        if (!ESpellOwned[3] && Gold >= 100) //if not owned and can buy
        {
            ESpellCost[3].text = "Owned";
            Gold -= 100;
            ESpellOwned[3] = true;
        }
        else if (!ESpellOwned[3] && Gold < 100) { ShowNeedGoldAlert(); }
    }
    public void MeteorShower()
    {
        // MeteorShower spell is ESpell[4]
        if (!ESpellOwned[4] && Gold >= 300) //if not owned and can buy
        {
            ESpellCost[4].text = "Owned";
            Gold -= 300;
            ESpellOwned[4] = true;
        }
        else if (!ESpellOwned[4] && Gold < 300) { ShowNeedGoldAlert(); }
    }
    public void FlameDash()
    {
        // FlameDash spell is ESpell[5]
        if (!ESpellOwned[5] && Gold >= 200) //if not owned and can buy
        {
            ESpellCost[5].text = "Owned";
            Gold -= 200;
            ESpellOwned[5] = true;
        }
        else if (!ESpellOwned[5] && Gold < 200) { ShowNeedGoldAlert(); }
    }
    public void TitdalWave()
    {
        // Titdalwave spell is ESpell[6]
        if (!ESpellOwned[6] && Gold >= 300) //if not owned and can buy
        {
            ESpellCost[6].text = "Owned";
            Gold -= 300;
            ESpellOwned[6] = true;
        }
        else if (!ESpellOwned[6] && Gold < 300) { ShowNeedGoldAlert(); }
    }


    public void ShowNeedGoldAlert()
    {
        NeedGoldAlert.SetActive(true);
        StartCoroutine(HideObjSec(1, NeedGoldAlert));
        //NeedGoldAlert.SetActive(false);
    }
    private IEnumerator HideObjSec(int sec, GameObject obj)
    {
        //yield on a new YieldInstruction that waits for sec seconds.
        yield return new WaitForSecondsRealtime(sec);
        obj.SetActive(false);
    }
    /*
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
    }*/
}
