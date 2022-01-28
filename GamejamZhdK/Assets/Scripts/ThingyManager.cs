using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats
{
    public bool isPlayer = true;
    public bool isDead = false;
    public bool isPoisoned = false;

    public int LVL = 1;
    public int MAX = 10;

    public int EXPReq;
    public int EXPCurrent;
    public int expMod = 5;

    public float ATKGrowth = 0.5f;
    public float DEFGrowth = 0.15f;
    public float HPGrowth = 5f;
    public float SPDGrowth = 1f;

    public float ATK;               // deals ATK damage on every Attack
    public float DEF;               // blocks DEF damage when attacked
    public float HPMAX;             
    public float HP;
    public float SPD;               // determines who strikes first in battle

    public bool demGeenes = false;  // increases chance for inherited, mutated traits and stats by 50%
    public bool shiny = false;  
    public bool lifesteal = false;  // heals for lifestealValue * ATK on every attack
    public bool reflect = false;    // throws reflectValue of blocked damage back to the attacker
    public bool poison = false;     // deals posionValue * ATK per tick when applied to enemies
    public bool looter = false;     // gains 25% more exp from battles

    public bool weak = false;       // has 30% reduced ATK
    public bool frail = false;      // has 30% reduced DEF
    public bool slow = false;       // has 30% reduced SPD

    public float lifestealValue = 0.3f;
    public float reflectValue = 0.3f;
    public float poisonValue = 0.15f;

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
    // Start is called before the first frame update
    void Start()
    {
        stats.gameObject = gameObject;
        if(stats.isPlayer) StatsManager.Instance.UpdateStats(stats);
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

    public void AddExp(int _exp)
    {
        if(stats.LVL < stats.MAX)
        {
            while (_exp > 0)
            {
                if (_exp >= (stats.EXPReq - stats.EXPCurrent))
                {
                    _exp -= stats.EXPReq - stats.EXPCurrent;
                    if(stats.LVL < stats.MAX) LVLUP();
                }
                else
                {
                    stats.EXPCurrent += _exp;
                }
            }
        }
    }

    private void LVLUP()
    {
        stats.LVL++;
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
}
