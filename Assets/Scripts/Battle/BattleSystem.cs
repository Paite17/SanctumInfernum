using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//public enum BattleState
//{
//    START,
//    PLAYER_DECIDE,
//    PLAYER_ATTACK,
//    PLAYER_SPELL,
//    ENEMY_TURN,
//    WON,
//    LOST,
//    EXP,
//    LEVELUP

//}

//public class BattleSystem : MonoBehaviour
//{
//    // the state of the battle 
//    public BattleState currentState;

//    // character prefab references
//    [SerializeField] private GameObject playerPrefab;
//    [SerializeField] private PartyManager partyManager;

//    // enemy prefabs
//    [SerializeField] private List<GameObject> enemyPool;

//    // sprite positions + party manager
//    [SerializeField] private List<Transform> playerPartySpritePositions;
//    [SerializeField] private List<Transform> enemySpritePositions;
    


//    public Unit playerUnit;
//    public Unit playerPartnerUnit;
//    public List<Unit> enemyUnits;

//    // hud manager
//    [SerializeField] private BattleUI ui;

//    // keeping track of turn order
//    [SerializeField] private List<Unit> turnOrder;


//    // Start is called before the first frame update
//    void Start()
//    {
//        // setup battle
//        // The battlesystem object should remain inactive until a battle starts
//        currentState = BattleState.START;

//        StartCoroutine(SetupBattle());
//    }

//    // Update is called once per frame
//    void Update()
//    {

//    }


//    private IEnumerator SetupBattle()
//    {
//        // spawn players
//        GameObject playerObj = Instantiate(playerPrefab, playerPartySpritePositions[0]);
//        playerUnit = partyManager.currentPartyMembers[0];

//        // load party member (if in party)

//        // maybe load player data here when saving/loading is implemented

//        // first decide how many enemies there will be
//        int enemyCountDecide = Random.Range(1, 5);
//        int enemyCount = 0;

//        Debug.Log("enemyCountDecide = " + enemyCountDecide);
//        Debug.Log("enemyCount = " + enemyCount);
//        if (enemyCountDecide < 4) 
//        {
//            enemyCount = 1;
//        }
//        else if (enemyCountDecide == 4)
//        {
//            enemyCount = 2;
//        }
//        else if (enemyCountDecide == 5)
//        {
//            enemyCount = 3;
//        }

        

//        // now decide and spawn the enemies based on how many there are
//        for (int i = 0; i < enemyCount; i++)
//        {
//            // decide which enemy to spawn
//            int whichEnemy = Random.Range(0, enemyPool.Count);

//            // spawn the enemy (not sure if this will work yet, test it!)
//            GameObject enemyObj = Instantiate(enemyPool[whichEnemy], enemySpritePositions[i]);
//            enemyUnits.Add(enemyObj.GetComponent<Unit>());
//            //enemyUnits[i] = enemyObj.GetComponent<Unit>();
//            Debug.Log("Spawning " + enemyUnits[i].UnitName);
//        }


//        // set HUD displays (when implemented)


//        yield return new WaitForSeconds(2f);
//        TurnOrderDecide();
        
//        // setup UI
//        for (int i = 0; i < enemyCount; i++)
//        {
//            ui.SetHUD(enemyUnits[i], i);
//        }

//        ui.SetHUD(playerUnit, 0);

//        // partner hud
//        if (playerPartnerUnit != null)
//        {
//            ui.SetHUD(playerPartnerUnit, 1);
//        }

//        currentState = BattleState.PLAYER_DECIDE;
//        ui.menuState = BattleUIState.PLAYER_DECIDE;
//    }


//    // will sort out the turn order list to decide who's going when in that cycle
//    // when that cycle ends (when everyone has had their turn) call it again because speed buff/debuffs might be in play
//    private void TurnOrderDecide()
//    {
//        // first add every unit from the party to the queue
//        for (int i = 0; i < partyManager.currentPartyMembers.Count; i++)
//        {
//            turnOrder.Add(partyManager.currentPartyMembers[i]);
//        }

//        // add in all the enemies
//        for (int i = 0; i < enemyUnits.Count; i++)
//        {
//            turnOrder.Add(enemyUnits[i]);
//        }

//        // now sort them into an order based on agility
//        List<Unit> sortedList = turnOrder.OrderBy(unit => unit.Speed).ToList();

//        turnOrder = sortedList;
//    }

//    // activates the UI for the player
//    private void PlayerTurn()
//    {

//    }

//}