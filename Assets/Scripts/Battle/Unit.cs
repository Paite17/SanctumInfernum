using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType
{
    PLAYER,
    ENEMY
}

// The base unit class that will contain all base stats required for a unit
public class Unit : MonoBehaviour
{
    [SerializeField] private string unitName;
    [SerializeField][TextArea] private string unitDescription; 
    [SerializeField] private float strength;
    [SerializeField] private float baseStrength;
    [SerializeField] private float magicStrength;
    [SerializeField] private float baseMagicStrength;
    [SerializeField] private float defence;
    [SerializeField] private float baseDefence;
    [SerializeField] private float speed;
    [SerializeField] private float baseSpeed;
    [SerializeField] private float luck;
    [SerializeField] private float currentHP;
    [SerializeField] private float maxHP;
    [SerializeField] private float currentSP;
    [SerializeField] private float maxSP;
    [SerializeField] private bool keyMember;
    [SerializeField] private UnitType unitType_;
    [SerializeField] private int unitLevel;
    
    // maybe these should be in a player class
    // oh well
    public List<Pages> ownedPages;
    public List<Item> playerInventory;



    public string UnitName
    {
        get { return unitName; }
    }

    public float Strength
    {
        get { return strength; }
        set { strength = value; }
    }

    public float BaseStrength
    {
        get { return baseStrength; }
        set { baseStrength = value; }
    }

    public float MagicStrength
    {
        get { return magicStrength; }
        set { magicStrength = value; }
    }

    public float BaseMagicStrength
    {
        get { return baseMagicStrength; }
        set { baseMagicStrength = value; }
    }

    public float Defence
    {
        get { return defence; }
        set { defence = value; }
    }

    public float BaseDefence
    {
        get { return baseDefence; }
        set { baseDefence = value; }
    }

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public float BaseSpeed
    {
        get { return baseSpeed; }
        set { baseSpeed = value; }
    }

    public float Luck
    {
        get { return luck; }
        set { luck = value; }
    }

    public bool KeyMember
    {
        get { return keyMember; }
    }

    public UnitType UnitType_
    {
        get { return  unitType_; }
    }

    public float CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }

    public float MaxHP
    {
        get { return  maxHP; }
        set { maxHP = value; }
    }

    public float CurrentSP
    {
        get { return currentSP; }
        set { currentSP = value; }
    }

    public float MaxSP
    {
        get { return maxSP; }
        set { maxSP = value; }
    }

    public int UnitLevel
    {
        get { return unitLevel; }
        set { unitLevel = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // used for the player units to load their data
    public void LoadPlayerData(Unit unit)
    {

    }

    // deal damage to target units using stats
    public void TakeDamage(Unit targetUnit, Unit attackingUnit, float noteaccuracy, bool pageAttack)
    {

    }

    // heal the targeted unit
    public void HealUnit(Unit target, float amount)
    {
        target.currentHP += amount * target.maxHP;
    }

    public void ReduceSP(Unit target, float reducedSP)
    {
        target.currentSP -= reducedSP;
    }

    // The following functions are used to increase the stats of the units whenever they level up
    public void IncreaseStrength(Unit target, float StrengthIncrease)
    {
        target.baseStrength += StrengthIncrease;
    }

    public void IncreaseSpeed(Unit target, float SpeedIncrease)
    {
        target.baseSpeed += SpeedIncrease;
    }

    public void IncreaseMagicPower(Unit target, float MpIncrease)
    {
        target.baseMagicStrength += MpIncrease;
    }

    public void IncreaseLuck(Unit target, float LuckIncrease)
    {
        target.luck += LuckIncrease;
    }

    public void IncreaseDefence(Unit target, float DefenceIncrease)
    {
        target.baseDefence += DefenceIncrease;
    }

    public void IncreaseMaxHP(Unit target, float HpIncrease)
    {
        target.maxHP += HpIncrease;
    }

    public void IncreaseMaxSP(Unit target, float SpIncrease)
    {
        target.maxSP += SpIncrease;
    }

    public void LevelUp(Unit target, int LevelIncrease)
    {
        target.unitLevel += LevelIncrease;
    }
}
