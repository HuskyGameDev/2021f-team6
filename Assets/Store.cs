using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour
{
    //UI Object for hidding
    public GameObject UI;

    //Gold Amount
    public int Gold = 0;

    //Elemental Spells
    public Text[] ESpellCost = new Text[4];
    private bool[] ESpellOwned = new bool[4];

    //Ability Upgrades
    public Text[] UpgradeAmount = new Text[2];
    private int[] UpgradeOwned = new int[2];

    //Power Spells
    public Text[] PSpellCost = new Text[10];
    private bool[] PSpellOwned = new bool[10];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Restores Store Default values
    private void StoreDefault()
    {

    }

    //Ability Upgrade Methods----------------------------------
    public void health()
    {
        //Health is Upgrade[0]
        if(UpgradeOwned[0] < 10 && Gold >= 100) //if not full and can buy
        {
            UpgradeOwned[0] += 1;
            UpgradeAmount[0].text = UpgradeOwned[0] + "/10";
            Gold -= 100;
        }
        //Magica is Upgrade[1]
        if (UpgradeOwned[1] < 10 && Gold >= 100) //if not full and can buy
        {
            UpgradeOwned[1] += 1;
            UpgradeAmount[1].text = UpgradeOwned[1] + "/10";
            Gold -= 100;
        }
    }

    //Elemental Spell Methods----------------------------------
    public void Ice()
    {
        //Ice spell is ESpell[0]
        if (!ESpellOwned[0] && Gold >= 100) //if not owned and can buy
        {
            ESpellCost[0].text = "\nOwned";
            Gold -= 100;
            ESpellOwned[0] = true;
        }
    }
    public void Earth()
    {
        //Earth spell is ESpell[1]
        if (!ESpellOwned[1] && Gold >= 100) //if not owned and can buy
        {
            ESpellCost[1].text = "\nOwned";
            Gold -= 100;
            ESpellOwned[1] = true;
        }
    }
    public void Lighting()
    {
        //Lighting spell is ESpell[2]
        if (!ESpellOwned[2] && Gold >= 100) //if not owned and can buy
        {
            ESpellCost[2].text = "\nOwned";
            Gold -= 100;
            ESpellOwned[2] = true;
        }
    }
    public void Fire()
    {
        //Fire spell is ESpell[3]
        if (!ESpellOwned[3] && Gold >= 100) //if not owned and can buy
        {
            ESpellCost[3].text = "\nOwned";
            Gold -= 100;
            ESpellOwned[3] = true;
        }
    }

    //Power Spell Methods-------------------------------------

}
