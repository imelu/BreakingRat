using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigNums;

[System.Serializable]
public class Stats
{
    public bool isPlayer = true;
    public bool isDead = false;
    public bool isPoisoned = false;

    public ScienceNum LVL; // = 1;
    public ScienceNum MAX; // = 10;

    public ScienceNum EXPReq;
    public ScienceNum EXPCurrent;
    public ScienceNum expMod;

    public ScienceNum ATKGrowth; // 0.5f;
    public ScienceNum DEFGrowth; //= 0.15f;
    public ScienceNum HPGrowth; // = 5f;
    public ScienceNum SPDGrowth; // = 1f;

    public ScienceNum ATK;               // deals ATK damage on every Attack
    public ScienceNum DEF;               // blocks DEF damage when attacked
    public ScienceNum HPMAX;             
    public ScienceNum HP;
    public ScienceNum SPD;               // determines who strikes first in battle

    public bool demGeenes = false;  // increases chance for inherited, mutated traits and stats by 50%
    public bool shiny = false;  
    public bool lifesteal = false;  // heals for lifestealValue * ATK on every attack
    public bool reflect = false;    // throws reflectValue of blocked damage back to the attacker
    public bool poison = false;     // deals posionValue * ATK per tick when applied to enemies
    public bool looter = false;     // gains 25% more exp from battles

    public bool weak = false;       // has 30% reduced ATK
    public bool frail = false;      // has 30% reduced DEF
    public bool slow = false;       // has 30% reduced SPD

    public ScienceNum lifestealValue; //  = 0.3f;
    public ScienceNum reflectValue; // = 0.3f;
    public ScienceNum poisonValue; // = 0.15f;

    public GameObject gameObject;

    /* additional stat ideas:
     * lifesteal -> regain life on hit
     * reflect -> reflect some dmg
     * poison -> apply a DoT
     * regen -> regenerate life over time
     * avoidance -> chance to avoid hits entirely
     * looter -> get more loot from enemies
     * dem geeenes -> better chances at better stats/mutations when breeding
     * negative effect
     */
}

public class ThingyManager : MonoBehaviour
{
    public Stats stats = new Stats();
    public ThingyData data;
    public bool fighting = false;

    private ScienceNum fixedTime;
    // Start is called before the first frame update
    void Start()
    {
        stats.expMod.baseValue = 5;
        stats.expMod.eFactor = 0;

        fixedTime.baseValue = 2;
        fixedTime.eFactor = -2;
    }

    private void FixedUpdate()
    {
        //Debug.Log(stats.HP < stats.HPMAX);
        if (stats.HP < stats.HPMAX && fighting == false && stats.isPlayer)
        {
            
            ScienceNum fifteen;
            fifteen.baseValue = 1.5f;
            fifteen.eFactor = 1;

            ScienceNum five;
            five.baseValue = 5;
            five.eFactor = 0;

            ScienceNum addValue = (stats.HPMAX / fifteen + five) * fixedTime;
            //Debug.Log(addValue.baseValue + "  .  " + addValue.eFactor);
            //Debug.Log(stats.HP.baseValue + "  .  " + stats.HP.eFactor);
            stats.HP += addValue;
            //Debug.Log(stats.HP.baseValue + "  .  " + stats.HP.eFactor);
        }
        else if (stats.HP > stats.HPMAX && fighting == false && stats.isPlayer)
        {
            stats.HP = stats.HPMAX;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateThingyData();
        if (!stats.isPlayer)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    public void AddExp(ScienceNum _exp)
    {
        if(stats.LVL < stats.MAX)
        {
            while (_exp.baseValue > 0)
            {
                if (_exp >= (stats.EXPReq - stats.EXPCurrent))
                {
                    _exp -= stats.EXPReq - stats.EXPCurrent;
                    if (stats.LVL < stats.MAX) LVLUP();
                }
                else
                {
                    stats.EXPCurrent += _exp;
                    _exp.baseValue = 0;
                    _exp.eFactor = 0;
                }
                if (stats.LVL >= stats.MAX)
                {
                    return;
                }
            }
        }
    }

    private void LVLUP()
    {
        ScienceNum levelup;
        levelup.baseValue = 1;
        levelup.eFactor = 0;
        stats.LVL += levelup;
        StatsManager.Instance.UpdateStats(stats);
        UpdateThingyData();
    }

    public void UpdateThingyData()
    {
        data.stats.isPlayer = stats.isPlayer;
        data.stats.isDead = stats.isDead;
        data.stats.isPoisoned = stats.isPoisoned;
        
        data.stats.LVL = stats.LVL;
        data.stats.MAX = stats.MAX;
        
        data.stats.EXPReq = stats.EXPReq;
        data.stats.EXPCurrent = stats.EXPCurrent;
        data.stats.expMod = stats.expMod;

        data.stats.ATKGrowth = stats.ATKGrowth;
        data.stats.DEFGrowth = stats.DEFGrowth;
        data.stats.HPGrowth = stats.HPGrowth;
        data.stats.SPDGrowth = stats.SPDGrowth;

        data.stats.ATK = stats.ATK;
        data.stats.DEF = stats.DEF;
        data.stats.HPMAX = stats.HPMAX;
        data.stats.HP = stats.HP;
        data.stats.SPD = stats.SPD;

        data.stats.demGeenes = stats.demGeenes;
        data.stats.shiny = stats.shiny;
        data.stats.lifesteal = stats.lifesteal;
        data.stats.reflect = stats.reflect;
        data.stats.poison = stats.poison;
        data.stats.looter = stats.looter;

        data.stats.weak = stats.weak;
        data.stats.frail = stats.frail;
        data.stats.slow = stats.slow;

        data.stats.lifestealValue = stats.lifestealValue;
        data.stats.reflectValue = stats.reflectValue;
        data.stats.poisonValue = stats.poisonValue;
    }

    public void InitializeThingyData()
    {
        stats.isPlayer = data.stats.isPlayer;
        stats.isDead = data.stats.isDead;
        stats.isPoisoned = data.stats.isPoisoned;

        stats.LVL = data.stats.LVL;
        stats.MAX = data.stats.MAX;
        
        stats.EXPReq = data.stats.EXPReq;
        stats.EXPCurrent = data.stats.EXPCurrent;
        stats.expMod = data.stats.expMod;

        stats.ATKGrowth = data.stats.ATKGrowth;
        stats.DEFGrowth = data.stats.DEFGrowth;
        stats.HPGrowth = data.stats.HPGrowth;
        stats.SPDGrowth = data.stats.SPDGrowth;

        stats.ATK = data.stats.ATK;
        stats.DEF = data.stats.DEF;
        stats.HPMAX = data.stats.HPMAX;
        stats.HP = data.stats.HP;
        stats.SPD = data.stats.SPD;

        stats.demGeenes = data.stats.demGeenes;
        stats.shiny = data.stats.shiny;
        stats.lifesteal = data.stats.lifesteal;
        stats.reflect = data.stats.reflect;
        stats.poison = data.stats.poison;
        stats.looter = data.stats.looter;

        stats.weak = data.stats.weak;
        stats.frail = data.stats.frail;
        stats.slow = data.stats.slow;

        stats.lifestealValue = data.stats.lifestealValue;
        stats.reflectValue = data.stats.reflectValue;
        stats.poisonValue = data.stats.poisonValue;
    }

    public void InitializeNewThingyData()
    {
        stats.LVL.baseValue = 1;
        stats.LVL.eFactor = 0;

        stats.expMod.baseValue = 5;
        stats.expMod.eFactor = 0;

        stats.MAX.baseValue = 1;
        stats.MAX.eFactor = 1;

        stats.ATKGrowth.baseValue = 5;
        stats.ATKGrowth.eFactor = -1;

        stats.DEFGrowth.baseValue = 2.5f;
        stats.DEFGrowth.eFactor = -1;

        stats.SPDGrowth.baseValue = 1;
        stats.SPDGrowth.eFactor = 0;

        stats.HPGrowth.baseValue = 5;
        stats.HPGrowth.eFactor = 0;

        stats.lifestealValue.baseValue = 3;
        stats.lifestealValue.eFactor = -1;

        stats.reflectValue.baseValue = 3;
        stats.reflectValue.eFactor = -1;

        stats.poisonValue.baseValue = 1.5f;
        stats.poisonValue.eFactor = -1;

        stats.gameObject = gameObject;
        //if (stats.isPlayer) StatsManager.Instance.UpdateStats(stats);
        StatsManager.Instance.UpdateStats(stats);
    }
}
