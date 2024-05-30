using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//public enum BattleUIState
//{
//    INACTIVE,
//    PLAYER_DECIDE,
//    ATTACK_TARGETING,
//    ITEM_MENU,
//    PAGES_MENU,
//    DETAILS_MENU,
//    IS_ATTACKING,
//    BATTLE_WIN,
//    LEVEL_UP
//}

//public class BattleUI : MonoBehaviour
//{
//    // stat text
//    [SerializeField] private List<TMP_Text> unitPlayerStats;
//    [SerializeField] private List<TMP_Text> unitEnemyStats;

//    // battle sys reference
//    [SerializeField] private BattleSystem battleSystem;


//    // item submenu stuff
//    [SerializeField] private GameObject itemSubmenu;
//    [SerializeField] private GameObject itemButtonTemplate;
//    [SerializeField] private TMP_Text itemDescription;
//    [SerializeField] private Transform inventoryItemContainer;

//    // pages submenu stuff
//    [SerializeField] private GameObject pagesSubmenu;
//    [SerializeField] private GameObject pagesButtonTemplate;
//    [SerializeField] private Image pagePaternPreview;
//    [SerializeField] private Transform pagesItemContainer;

//    // details submenu stuff
//    [SerializeField] private GameObject detailsSubmenu;
//    [SerializeField] private TMP_Text details_targetedEnemyLevel;
//    [SerializeField] private TMP_Text details_targetedEnemyName;
//    [SerializeField] private TMP_Text details_targetedEnemyAttack;
//    [SerializeField] private TMP_Text details_targetedEnemyHealth;
//    [SerializeField] private TMP_Text details_targetedEnemyDefence;
//    [SerializeField] private TMP_Text details_targetedEnemySpeed;
//    [SerializeField] private TMP_Text details_targetedEnemyLuck;
//    [SerializeField] private TMP_Text details_targetedEnemyDescription;

//    // hud values and others
//    public BattleUIState menuState;
//    [SerializeField] private EventSystem eventSystem;
//    [SerializeField] private GameObject mainUI;

//    public delegate void UnitAttacking();
//    UnitAttacking UnitAttackingButton;

//    public Button AttackButton, HealButton;

//    // Start is called before the first frame update
//    void Start()
//    {
//        mainUI.SetActive(false);
//    }

//    // Update is called once per frame
//    void Update()
//    {
//        UnitAttackingButton = PlayerAttackButton;

//    }

//    // unitStats: the unit we're getting the info from, unitIndex: where they are placed (e.g, if they're the partner)
//    public void SetHUD(Unit unitStats, int unitIndex)
//    {
//        // first check unit type
//        switch (unitStats.UnitType_)
//        {
//            case UnitType.PLAYER:
//                // then set the values
//                // NOTE: this will likely need to be changed for whenever the UI is actually finished
//                unitPlayerStats[unitIndex].text = unitStats.UnitName + "\n" + "HP: " + unitStats.CurrentHP + "/" + unitStats.MaxHP + "\n" + "Level: " + unitStats.UnitLevel;
//                break;
//            case UnitType.ENEMY:
//                unitEnemyStats[unitIndex].text = unitStats.UnitName + "\n" + "HP: " + unitStats.CurrentHP + "/" + unitStats.MaxHP + "\n" + "Level: " + unitStats.UnitLevel;
//                break;
//        }
//    }

//    // called upon selecting the item submenu
//    private void LoadItemData()
//    {
//        // copied from seams :DDDDDDDD
//        // instantiate the item button
//        for (int i  = 0; i < battleSystem.playerUnit.playerInventory.Count; i++)
//        {
//            if (battleSystem.playerUnit.playerInventory.Count > 0)
//            {
//                // instantiate the button template
//                var item_obj = Instantiate(itemButtonTemplate);

//                // add a function to the button
//                item_obj.GetComponent<Button>().onClick.AddListener(delegate { UseItem(); });

//                // set name
//                item_obj.GetComponentInChildren<TMP_Text>().text = battleSystem.playerUnit.playerInventory[i].ItemName;

//                // item icon (are we even gonna have those?)
//                item_obj.transform.GetChild(1).GetComponent<Image>().sprite = battleSystem.playerUnit.playerInventory[i].GetIconByID();

//                // set parent
//                item_obj.transform.SetParent(inventoryItemContainer);

//                // reset scale
//                item_obj.transform.localScale = Vector2.one;
//            }
//            else
//            {
//                return;
//            }
            

//        }


//        // wonder if this will work
//        TheFunny[] allObjs = FindObjectsOfType<TheFunny>();
//        if (battleSystem.playerUnit.playerInventory.Count > 0)
//        {
//            eventSystem.SetSelectedGameObject(allObjs[0].gameObject);
//        }
        
//    }

//    // hmmm how we gonna do this then?
//    public void UseItem()
//    {

//    }

//    public void OpenItemSubmenu()
//    {
//        menuState = BattleUIState.ITEM_MENU;
//        itemSubmenu.SetActive(true);
//        LoadItemData();
//    }

//    public void OpenPagesSubmenu()
//    {

//    }

//    public void OpenDetailsSubmenu()
//    {

//    }

//    private void LoadPageData()
//    {
//        // copied from items
//        for (int i = 0; i < battleSystem.playerUnit.ownedPages.Count; i++)
//        {
//            // copy from items

//        }
//    }

//    public static void PlayerAttackButton()
//    {

//    }

//    public static void ImpAttackButton()
//    {

//    }

//    public static void PlayerHealButton()
//    {

//    }

//}
