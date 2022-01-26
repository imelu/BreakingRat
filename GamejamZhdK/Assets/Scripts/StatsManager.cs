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

    private float demGeenesMult = 0.5f;

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

        if(_p1s.stats.demGeenes || _p2s.stats.demGeenes)
        {
            _percentUp = percentUp + percentUp * demGeenesMult;
            _percentDown = percentDown - percentDown * demGeenesMult;
        }

        _cs.stats.ATKGrowth = ((_p1s.stats.ATKGrowth * p1mult + _p2s.stats.ATKGrowth * p2mult) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100);
        _cs.stats.DEFGrowth = ((_p1s.stats.DEFGrowth * p1mult + _p2s.stats.DEFGrowth * p2mult) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100);
        _cs.stats.HPGrowth = ((_p1s.stats.HPGrowth * p1mult + _p2s.stats.HPGrowth * p2mult) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100);
        _cs.stats.SPDGrowth = ((_p1s.stats.SPDGrowth * p1mult + _p2s.stats.SPDGrowth * p2mult) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100);

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
    }
}
