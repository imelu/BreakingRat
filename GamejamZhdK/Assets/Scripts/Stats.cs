using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public int LVL;
    public int MAX;

    public float ATKGrowth;
    public float DEFGrowth;
    public float HPGrowth;
    public float SPDGrowth;
    public float CRITGrowth;

    public float ATK;
    public float DEF;
    public float HP;
    public float SPD;
    public float CRIT;

    public bool demGeenes = false;
    public bool shiny = false;

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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdateStats();
    }

    private void UpdateStats()
    {
        ATK = (StatsManager.Instance.baseStatLevel + LVL) * ATKGrowth;
        DEF = (StatsManager.Instance.baseStatLevel + LVL) * DEFGrowth;
        HP = (StatsManager.Instance.baseStatLevel + LVL) * HPGrowth;
        SPD = (StatsManager.Instance.baseStatLevel + LVL) * SPDGrowth;
        CRIT = (StatsManager.Instance.baseStatLevel + LVL) * CRITGrowth;
    }
}
