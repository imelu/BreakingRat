using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ThingyData
{
    public StatsData stats;
    public List<string> Bodyparts;
    public string mainAnimalType;

    public ThingyData(Stats _stats, List<string> _Bodyparts, string _mainAnimalType)
    {
        StatsData _statsdata = new StatsData(_stats);
        stats = _statsdata;
        Bodyparts = _Bodyparts;
        mainAnimalType = _mainAnimalType;
    }
}

[System.Serializable]
public class StatsData
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

    public StatsData(Stats _stats)
    {
        isPlayer = _stats.isPlayer;
        isDead = _stats.isDead;
        isPoisoned = _stats.isPoisoned;

        LVL = _stats.LVL;
        MAX = _stats.MAX;

        EXPReq = _stats.EXPReq;
        EXPCurrent = _stats.EXPCurrent;
        expMod = _stats.expMod;

        ATKGrowth = _stats.ATKGrowth;
        DEFGrowth = _stats.DEFGrowth;
        HPGrowth = _stats.HPGrowth;
        SPDGrowth = _stats.SPDGrowth;

        ATK = _stats.ATK;
        DEF = _stats.DEF;
        HPMAX = _stats.HPMAX;
        HP = _stats.HP;
        SPD = _stats.SPD;

        demGeenes = _stats.demGeenes;
        shiny = _stats.shiny;
        lifesteal = _stats.lifesteal;
        reflect = _stats.reflect;
        poison = _stats.poison;
        looter = _stats.looter;

        weak = _stats.weak;
        frail = _stats.frail;
        slow = _stats.slow;

        lifestealValue = _stats.lifestealValue;
        reflectValue = _stats.reflectValue;
        poisonValue = _stats.poisonValue;
    }
}

public class CurrentThingies : MonoBehaviour
{
    public List<ThingyData> thingies = new List<ThingyData>();

    #region Singleton
    public static CurrentThingies Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddThingy(ThingyData _thingy)
    {
        thingies.Add(_thingy);
    }

    public void RemoveThingy(ThingyData _thingy)
    {
        thingies.Remove(_thingy);
    }
}
