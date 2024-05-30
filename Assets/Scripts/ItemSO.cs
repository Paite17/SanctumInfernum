using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class ItemSO : ScriptableObject
{

    // THIS IS A PLACEHOLDER ITEM SCRIPTABLE OBJECT SCRIPT. REQUIRES CHANGES IF THE FINAL PROJECT IS CODED IN A DIFFERENT WAY. !!!
    // THIS IS PURELY AN ITEM RELATED SCRIPT, ITS FUNCTION IS TO CHECK HOW THE ITEM WILL AFFECT STATS WHEN USED. !!!
    // ADJUST OR REPLACE ACCORDINGLY !!!
    
    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangestat;

    public AttributeToChange attributeToChange = new AttributeToChange();
    public int amountToChangeAttribute;

    //THIS FUNCTION IS AN EXAMPLE OF HOW THE ITEM WILL CHANGE THE HEALTH STAT. THIS WILL PROBABLY NEED REPLACING WITH THE RELEVANT HEALTH SCRIPT IN THE GAME.
    //THE MAJORITY OF THESE FUNCTIONS LIKELY DO NOT WORK IN THE ACTUAL GAME, BUT REPRESENT PLACEHOLDERS FOR THE SAKE OF THE EQUIPMENT MANAGER.
    public void UseItem()
    {
        /*
        if(statToChange == StatToChange.health)
        {
            //IF THERE IS A SCRIPT FOR MANAGEMENT OF HEALTH, MANA OR OTHERWISE, CHANGE "PLAYERHEALTH" TO THE NAME OF THE RELEVANT MANAGER!
            //DUPLICATE THE FUNCTION FOR OTHER STATS LIKE MANA!
            
            GameObject.Find("HealthManager").GetComponent<PlayerHealth>().ChangeHealth(amountToChangeStat);
            
        }
        if (statToChange == StatToChange.mana)
        {
            GameObject.Find("ManaManager").GetComponent<PlayerMana>().ChangeMana(amountToChangestat);
        }
        */
    }
    

    // THESE ARE THE STATS THAT THE GAME IS CHECKING TO MODIFY WHEN THE ITEM IS CONSUMED/USED IN GAMEPLAY
    
    public enum StatToChange
    {
        none,
        health,
        mana,
    };
    

    
    public enum AttributeToChange
    {
        none,
        Strength,
        Stamina,
        Holy,
        Unholy,
    };
    
}
