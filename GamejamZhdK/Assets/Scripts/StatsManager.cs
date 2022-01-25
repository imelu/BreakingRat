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
        Stats _p1s = _Parent1.GetComponent<Stats>();
        Stats _p2s = _Parent2.GetComponent<Stats>();
        Stats _cs = _Child.GetComponent<Stats>();

        float p1mult = _p1s.LVL * levelAffectGrowth;
        if (p1mult < 1) p1mult = 1;
        float p2mult = _p2s.LVL * levelAffectGrowth;
        if (p2mult < 1) p2mult = 1;

        float p1multLVL = _p1s.LVL * levelAffectMaxLevel;
        if (p1multLVL < 1) p1multLVL = 1;
        float p2multLVL = _p2s.LVL * levelAffectMaxLevel;
        if (p2multLVL < 1) p2multLVL = 1;

        float _percentUp = percentUp;
        float _percentDown = percentDown;

        if(_p1s.demGeenes || _p2s.demGeenes)
        {
            _percentUp = percentUp + percentUp * demGeenesMult;
            _percentDown = percentDown - percentDown * demGeenesMult;
        }

        _cs.ATKGrowth = ((_p1s.ATKGrowth * p1mult + _p2s.ATKGrowth * p2mult) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100);
        _cs.DEFGrowth = ((_p1s.DEFGrowth * p1mult + _p2s.DEFGrowth * p2mult) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100);
        _cs.HPGrowth = ((_p1s.HPGrowth * p1mult + _p2s.HPGrowth * p2mult) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100);
        _cs.SPDGrowth = ((_p1s.SPDGrowth * p1mult + _p2s.SPDGrowth * p2mult) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100);
        _cs.CRITGrowth = ((_p1s.CRITGrowth * p1mult + _p2s.CRITGrowth * p2mult) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100);

        _cs.MAX = (int)(((_p1s.MAX * p1multLVL + _p2s.MAX * p2multLVL) / 2) * ((100 + Random.Range(-_percentDown, _percentUp)) / 100));

        _cs.ATK = baseStatLevel * _cs.ATKGrowth;
        _cs.DEF = baseStatLevel * _cs.DEFGrowth;
        _cs.HP = baseStatLevel * _cs.HPGrowth;
        _cs.SPD = baseStatLevel * _cs.SPDGrowth;
        _cs.CRIT = baseStatLevel * _cs.CRITGrowth;
    }
}
