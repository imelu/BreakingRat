using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public bool isPlayer = true;

    public int LVL = 1;
    public int MAX;

    public float ATKGrowth = 0.5f;
    public float DEFGrowth = 0.5f;
    public float HPGrowth = 0.5f;
    public float SPDGrowth = 0.5f;

    public float ATK;
    public float DEF;
    public float HPMAX;
    public float HP;
    public float SPD;

    public bool demGeenes = false;
    public bool shiny = false;

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
        StatsManager.Instance.UpdateStats(stats);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
