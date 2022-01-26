using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public bool isPlayer = true;

    public int LVL;
    public int MAX;

    public float ATKGrowth;
    public float DEFGrowth;
    public float HPGrowth;
    public float SPDGrowth;

    public float ATK;
    public float DEF;
    public float HPMAX;
    public float HP;
    public float SPD;

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
}

public class ThingyManager : MonoBehaviour
{
    public Stats stats = new Stats();
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
