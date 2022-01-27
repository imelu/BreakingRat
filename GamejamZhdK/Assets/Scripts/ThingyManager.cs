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

    public float ATKGrowth = 0.5f;
    public float DEFGrowth = 0.15f;
    public float HPGrowth = 5f;
    public float SPDGrowth = 1f;

    public float ATK;
    public float DEF;
    public float HPMAX;
    public float HP;
    public float SPD;

    public bool demGeenes = false;
    public bool shiny = false;
    public bool lifesteal = false;
    public bool reflect = false;
    public bool poison = false;
    public bool looter = false;

    public bool weak = false;
    public bool frail = false;
    public bool slow = false;

    public float lifestealValue = 0;
    public float reflectValue = 0;
    public float poisonValue = 0;

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
    // Start is called before the first frame update
    void Start()
    {
        stats.gameObject = gameObject;
        if(stats.isPlayer) StatsManager.Instance.UpdateStats(stats);
    }

    // Update is called once per frame
    void Update()
    {

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
                    LVLUP();
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
    }
}
