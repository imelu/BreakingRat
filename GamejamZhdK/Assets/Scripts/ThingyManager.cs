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
        UpdateThingyData();
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
        data.isPlayer = stats.isPlayer;
        data.isDead = stats.isDead;
        data.isPoisoned = stats.isPoisoned;

        data.LVL = stats.LVL;
        data.MAX = stats.MAX;

        data.EXPReq = stats.EXPReq;
        data.EXPCurrent = stats.EXPCurrent;
        data.expMod = stats.expMod;

        data.ATKGrowth = stats.ATKGrowth;
        data.DEFGrowth = stats.DEFGrowth;
        data.HPGrowth = stats.HPGrowth;
        data.SPDGrowth = stats.SPDGrowth;

        data.ATK = stats.ATK;
        data.DEF = stats.DEF;
        data.HPMAX = stats.HPMAX;
        data.HP = stats.HP;
        data.SPD = stats.SPD;

        data.demGeenes = stats.demGeenes;
        data.shiny = stats.shiny;
        data.lifesteal = stats.lifesteal;
        data.reflect = stats.reflect;
        data.poison = stats.poison;
        data.looter = stats.looter;

        data.weak = stats.weak;
        data.frail = stats.frail;
        data.slow = stats.slow;

        data.lifestealValue = stats.lifestealValue;
        data.reflectValue = stats.reflectValue;
        data.poisonValue = stats.poisonValue;
    }
}
