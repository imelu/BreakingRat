using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigNums;

public class StatsManager : MonoBehaviour
{
    // (growth parent1 + growth parent2) / 2 * ((100 + Random.Range(-percentDown, percentUp)) / 100)
    private int percentDown = 8; 
    private int percentUp = 15;

    private ScienceNum levelAffectGrowth; // = 0.5f; // level * growth values * levelAffect = growth parentX
    private ScienceNum levelAffectMaxLevel; // = 0.15f;

    public ScienceNum baseStatLevel;// = 4; // baseStatLevel * all growth values determines base stats

    // Trait Settings
    private ScienceNum buff; // = 1.5f;
    private ScienceNum debuff; // = 0.7f;
    //private ScienceNum demGeenesMult; // = 0.5f;

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
        baseStatLevel.baseValue = 4;
        baseStatLevel.eFactor = 0;

        levelAffectGrowth.baseValue = 1;
        levelAffectGrowth.eFactor = -1;

        levelAffectMaxLevel.baseValue = 1;
        levelAffectMaxLevel.eFactor = -1;

        buff.baseValue = 1.5f;
        buff.eFactor = 0;

        debuff.baseValue = 7;
        debuff.eFactor = -1;

        //demGeenesMult.baseValue = 5;
        //demGeenesMult.eFactor = -1;
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

        ScienceNum one;
        one.baseValue = 1;
        one.eFactor = 0;

        ScienceNum two;
        two.baseValue = 2;
        two.eFactor = 0;

        ScienceNum three;
        three.baseValue = 3;
        three.eFactor = 0;

        ScienceNum _shiny = one;
        ScienceNum _frail = one;
        ScienceNum _weak = one;
        ScienceNum _slow = one;

        ScienceNum p1mult = _p1s.stats.LVL * levelAffectGrowth;
        if (p1mult < one) p1mult = one;
        if (p1mult > three) p1mult = three;
        ScienceNum p2mult = _p2s.stats.LVL * levelAffectGrowth;
        if (p2mult < one) p2mult = one;
        if (p2mult > three) p2mult = three;

        ScienceNum p1multLVL = _p1s.stats.LVL * levelAffectMaxLevel;
        if (p1multLVL < one) p1multLVL = one;
        if (p1multLVL > two) p1multLVL = two;
        ScienceNum p2multLVL = _p2s.stats.LVL * levelAffectMaxLevel;
        if (p2multLVL < one) p2multLVL = one;
        if (p2multLVL > two) p2multLVL = two;

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

        _cs.stats.ATKGrowth = _shiny * _weak * ((_p1s.stats.ATKGrowth * p1mult + _p2s.stats.ATKGrowth * p2mult) / two) * getRandomChance(_percentDown, _percentUp);
        _cs.stats.DEFGrowth = _shiny * _frail * ((_p1s.stats.DEFGrowth * p1mult + _p2s.stats.DEFGrowth * p2mult) / two) * getRandomChance(_percentDown, _percentUp);
        _cs.stats.HPGrowth = _shiny * ((_p1s.stats.HPGrowth * p1mult + _p2s.stats.HPGrowth * p2mult) / two) * getRandomChance(_percentDown, _percentUp);
        _cs.stats.SPDGrowth = _shiny * _slow * ((_p1s.stats.SPDGrowth * p1mult + _p2s.stats.SPDGrowth * p2mult) / two) * getRandomChance(_percentDown, _percentUp);

        _cs.stats.MAX = (((_p1s.stats.MAX * p1multLVL + _p2s.stats.MAX * p2multLVL) / two) * getRandomChance(_percentDown, _percentUp));

        _cs.stats.ATK = baseStatLevel * _cs.stats.ATKGrowth;
        _cs.stats.DEF = baseStatLevel * _cs.stats.DEFGrowth;
        _cs.stats.HPMAX = baseStatLevel * _cs.stats.HPGrowth;
        _cs.stats.SPD = baseStatLevel * _cs.stats.SPDGrowth;
        
        _cs.stats.LVL.baseValue = 1;
        _cs.stats.LVL.eFactor = 0;

        _cs.stats.expMod.baseValue = 5;
        _cs.stats.expMod.eFactor = 0;

        UpdateStats(_cs.stats);
    }

    private ScienceNum getRandomChance(float _percentDown, float _percentUp)
    {
        ScienceNum rand;
        float randf;
        randf = ((100 + Random.Range(-_percentDown, _percentUp)) / 100);
        if(randf >= 100)
        {
            rand.baseValue = randf / 100;
            rand.eFactor = 2;
        } else if(randf >= 10)
        {
            rand.baseValue = randf / 10;
            rand.eFactor = 1;
        }
        else if (randf < 1)
        {
            rand.baseValue = randf * 10;
            rand.eFactor = - 1;
        }
        else
        {
            rand.baseValue = randf;
            rand.eFactor = 0;
        }

        return rand;
    }

    public void UpdateStats(Stats _stats)
    {
        ScienceNum _basestatlevel;
        _basestatlevel.baseValue = 9;
        _basestatlevel.eFactor = 0;
        if (_stats.isPlayer)
        {
            _stats.ATK = (_basestatlevel + _stats.LVL) * _stats.ATKGrowth;
            _stats.DEF = (_basestatlevel + _stats.LVL) * _stats.DEFGrowth;
            _stats.HPMAX = (_basestatlevel + _stats.LVL) * _stats.HPGrowth;
            _stats.HP = _stats.HPMAX;
            _stats.SPD = (_basestatlevel + _stats.LVL) * _stats.SPDGrowth;
            _stats.EXPReq = _stats.LVL * _stats.LVL * _stats.expMod;
        }
        else
        {
            _stats.ATK = (baseStatLevel + _stats.LVL) * _stats.ATKGrowth;
            _stats.DEF = (baseStatLevel + _stats.LVL) * _stats.DEFGrowth;
            _stats.HPMAX = (baseStatLevel + _stats.LVL) * _stats.HPGrowth;
            _stats.HP = _stats.HPMAX;
            _stats.SPD = (baseStatLevel + _stats.LVL) * _stats.SPDGrowth;
            _stats.EXPReq = _stats.LVL * _stats.LVL * _stats.expMod;
        }
        
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
