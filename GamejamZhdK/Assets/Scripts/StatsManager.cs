using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    // (growth parent1 + growth parent2) / 2 * ((100 + Random.Range(-percentDown, percentUp)) / 100)
    private int percentDown = 8; 
    private int percentUp = 15;

    private float levelAffectGrowth = 0.5f; // level * growth values * levelAffect = growth parentX
    private float levelAffectMaxLevel = 0.15f;

    public int baseStatLevel = 4; // baseStatLevel * all growth values determines base stats
    
    // Trait Settings
    private float buff = 1.5f;
    private float debuff = 0.7f;
    private float demGeenesMult = 0.5f;

    // Trait inherit settings
    private float mutationChance = 0.05f;
    private float inheritChance = 0.3f;
    private float inheritShinyChance = 0.005f;

    //Debug
    /*
    [SerializeField] private GameObject p1;
    [SerializeField] private GameObject p2;
    [SerializeField] private GameObject thingy;*/

    #region Singleton
    public static StatsManager Instance;
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
    {/*
        // Debug
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject _thingy;
            _thingy = Instantiate(thingy);
            CalculateStats(p1, p2, _thingy);
        }*/
    }

    public void CalculateStats(GameObject _Parent1, GameObject _Parent2, GameObject _Child)
    {
        ThingyManager _p1s = _Parent1.GetComponent<ThingyManager>();
        ThingyManager _p2s = _Parent2.GetComponent<ThingyManager>();
        ThingyManager _cs = _Child.GetComponent<ThingyManager>();

        float _shiny = 1;
        float _frail = 1;
        float _weak = 1;
        float _slow = 1;

        float p1mult = _p1s.stats.LVL * levelAffectGrowth;
        if (p1mult < 1) p1mult = 1;
        float p2mult = _p2s.stats.LVL * levelAffectGrowth;
        if (p2mult < 1) p2mult = 1;

        float p1multLVL = _p1s.stats.LVL * levelAffectMaxLevel;
        if (p1multLVL < 1) p1multLVL = 1;
        float p2multLVL = _p2s.stats.LVL * levelAffectMaxLevel;
        if (p2multLVL < 1) p2multLVL = 1;

        float _percentUp = percentUp;
        float _percentDown = percentDown;

        // calculate traits passed down

        CalculateTraits(_p1s.stats, _p2s.stats, _cs.stats);

        // set buffs/nerfs according to traits
        if (_cs.stats.shiny) _shiny = buff;
        if (_cs.stats.slow) _slow = debuff;
        if (_cs.stats.frail) _frail = debuff;
        if (_cs.stats.weak) _weak = debuff;


        if (_p1s.stats.demGeenes || _p2s.stats.demGeenes)
        {
            _percentUp = percentUp + percentUp * demGeenesMult;
            _percentDown = percentDown - percentDown * demGeenesMult;
        }

        _cs.stats.ATKGrowth = _shiny * _weak * ((_p1s.stats.ATKGrowth * p1mult + _p2s.stats.ATKGrowth * p2mult) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100);
        _cs.stats.DEFGrowth = _shiny * _frail * ((_p1s.stats.DEFGrowth * p1mult + _p2s.stats.DEFGrowth * p2mult) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100);
        _cs.stats.HPGrowth = _shiny * ((_p1s.stats.HPGrowth * p1mult + _p2s.stats.HPGrowth * p2mult) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100);
        _cs.stats.SPDGrowth = _shiny * _slow * ((_p1s.stats.SPDGrowth * p1mult + _p2s.stats.SPDGrowth * p2mult) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100);

        _cs.stats.MAX = (int)(((_p1s.stats.MAX * p1multLVL + _p2s.stats.MAX * p2multLVL) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100));

        _cs.stats.ATK = baseStatLevel * _cs.stats.ATKGrowth;
        _cs.stats.DEF = baseStatLevel * _cs.stats.DEFGrowth;
        _cs.stats.HPMAX = baseStatLevel * _cs.stats.HPGrowth;
        _cs.stats.SPD = baseStatLevel * _cs.stats.SPDGrowth;
    }

    public void UpdateStats(Stats _stats)
    {
        _stats.ATK = (baseStatLevel + _stats.LVL) * _stats.ATKGrowth;
        _stats.DEF = (baseStatLevel + _stats.LVL) * _stats.DEFGrowth;
        _stats.HPMAX = (baseStatLevel + _stats.LVL) * _stats.HPGrowth;
        _stats.HP = _stats.HPMAX;
        _stats.SPD = (baseStatLevel + _stats.LVL) * _stats.SPDGrowth;
        _stats.EXPReq = _stats.LVL * _stats.LVL * _stats.expMod;
    }

    private void CalculateTraits(Stats _p1s, Stats _p2s, Stats _cs)
    {
        int addedTraits = 0;
        float _inheritChance = inheritChance;
        float _mutationChance = mutationChance;
        float _inheritShinyChance = inheritShinyChance;
        if(_p1s.demGeenes || _p2s.demGeenes)
        {
            _inheritChance = inheritChance * (1+demGeenesMult);
            _mutationChance = mutationChance * (1+demGeenesMult);
            _inheritShinyChance = inheritShinyChance * (1+demGeenesMult);
        }
        List<int> checkedTraits = new List<int>();
        int rand;
        while (checkedTraits.Count < 9 && addedTraits < 3)
        {
            rand = Random.Range(0, 9);
            switch (rand)
            {
                case 0:
                    if (!checkedTraits.Contains(rand))
                    {
                        addedTraits++;
                        checkedTraits.Add(rand);
                        if ((_p1s.demGeenes || _p2s.demGeenes) && Random.Range(0f, 1f) <= _inheritChance) _cs.demGeenes = true;
                        else if (Random.Range(0f, 1f) <= _mutationChance) _cs.demGeenes = true;
                        else addedTraits--;
                    }
                    break;
                case 1:
                    if (!checkedTraits.Contains(rand))
                    {
                        addedTraits++;
                        checkedTraits.Add(rand);
                        if ((_p1s.lifesteal || _p2s.lifesteal) && Random.Range(0f, 1f) <= _inheritChance) _cs.lifesteal = true;
                        else if (Random.Range(0f, 1f) <= _mutationChance) _cs.lifesteal = true;
                        else addedTraits--;
                    }
                    break;
                case 2:
                    if (!checkedTraits.Contains(rand))
                    {
                        addedTraits++;
                        checkedTraits.Add(rand);
                        if ((_p1s.reflect || _p2s.reflect) && Random.Range(0f, 1f) <= _inheritChance) _cs.reflect = true;
                        else if (Random.Range(0f, 1f) <= _mutationChance) _cs.reflect = true;
                        else addedTraits--;
                    }
                    break;
                case 3:
                    if (!checkedTraits.Contains(rand))
                    {
                        addedTraits++;
                        checkedTraits.Add(rand);
                        if ((_p1s.poison || _p2s.poison) && Random.Range(0f, 1f) <= _inheritChance) _cs.poison = true;
                        else if (Random.Range(0f, 1f) <= _mutationChance) _cs.poison = true;
                        else addedTraits--;
                    }
                    break;
                case 4:
                    if (!checkedTraits.Contains(rand))
                    {
                        addedTraits++;
                        checkedTraits.Add(rand);
                        if ((_p1s.looter || _p2s.looter) && Random.Range(0f, 1f) <= _inheritChance) _cs.looter = true;
                        else if (Random.Range(0f, 1f) <= _mutationChance) _cs.looter = true;
                        else addedTraits--;
                    }
                    break;
                case 5:
                    if (!checkedTraits.Contains(rand))
                    {
                        addedTraits++;
                        checkedTraits.Add(rand);
                        if ((_p1s.weak || _p2s.weak) && Random.Range(0f, 1f) <= _inheritChance) _cs.weak = true;
                        else if (Random.Range(0f, 1f) <= _mutationChance) _cs.weak = true;
                        else addedTraits--;
                    }
                    break;
                case 6:
                    if (!checkedTraits.Contains(rand))
                    {
                        addedTraits++;
                        checkedTraits.Add(rand);
                        if ((_p1s.frail || _p2s.frail) && Random.Range(0f, 1f) <= _inheritChance) _cs.frail = true;
                        else if (Random.Range(0f, 1f) <= _mutationChance) _cs.frail = true;
                        else addedTraits--;
                    }
                    break;
                case 7:
                    if (!checkedTraits.Contains(rand))
                    {
                        addedTraits++;
                        checkedTraits.Add(rand);
                        if ((_p1s.slow || _p2s.slow) && Random.Range(0f, 1f) <= _inheritChance) _cs.slow = true;
                        else if (Random.Range(0f, 1f) <= _mutationChance) _cs.slow = true;
                        else addedTraits--;
                    }
                    break;
                case 8:
                    if (!checkedTraits.Contains(rand))
                    {
                        addedTraits++;
                        checkedTraits.Add(rand);
                        if ((_p1s.shiny || _p2s.shiny) && Random.Range(0f, 1f) <= _inheritShinyChance) _cs.shiny = true;
                        else addedTraits--;
                    }
                    break;
            }
        }
    }
}
