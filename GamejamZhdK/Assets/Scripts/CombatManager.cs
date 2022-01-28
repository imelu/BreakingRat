using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigNums;

public class CombatManager : MonoBehaviour
{
    public FightClubManager FCManager;
    public EncounterManager EncManager;
    private EnemyHPBar enemyHPBar;

    [SerializeField] List<ParticleSystem> hitParticle = new List<ParticleSystem>();


    private Stats Player;

    public List<Stats> Enemies = new List<Stats>();

    public List<Stats> TurnOrder = new List<Stats>();

    private int nmbrOfAttacks = 0;

    private bool simulating = false;

    private bool playerDefeated = false;

    
    private float maxDelay = 1f;
    private float minDelay = 0.01f;
    private float delay = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        FCManager = GetComponent<FightClubManager>();
        EncManager = GetComponent<EncounterManager>();
        enemyHPBar = GetComponent<EnemyHPBar>();

        Player = FCManager.PlayerThingy.GetComponent<ThingyManager>().stats;
    }

    // Update is called once per frame
    void Update()
    {
        Enemies.RemoveAll(item => item == null);
        //TurnOrder.RemoveAll(item => item == null);
        if (Input.GetKeyDown(KeyCode.V))
        {
            //CalculateBattle();
        }
        

        if (Enemies.Count <= 0 && simulating)
        {
            foreach(Stats Enemy in TurnOrder)
            {
                if (!Enemy.isPlayer)
                {
                    Destroy(Enemy.gameObject);
                }
            }
            TurnOrder.Clear();
            if(EncManager.Stages.Count > EncManager.stage-1)
            {
                EncManager.generateEncounter();
            }
            else
            {
                Debug.Log("caught null error");
                playerDefeated = true;
                simulating = false;
                Player.HP = Player.HPMAX; // DEBUG, change to 1
                YeetEnemies();
                Player.gameObject.GetComponent<ThingyManager>().AddExp(FCManager.expGained);
                if (GlobalGameManager.Instance.maxStage < EncManager.stage) GlobalGameManager.Instance.maxStage = EncManager.stage;
                AudioManager.instance.Play("Victory");
                GlobalGameManager.Instance.EndFightCub();
            }
            
        }
    }

    private void CombatOrder()
    {
        TurnOrder.Clear();
        TurnOrder.Add(Player);

        foreach (Stats Enemy in Enemies)
        {
            TurnOrder.Add(Enemy);
        }
        TurnOrder.Sort(CompareSpeeds);
    }

    public void GetEnemies()
    {
        simulating = true;
        Player.isDead = false;
        foreach (GameObject Enemy in EncManager.Enemies)
        {
            Enemy.GetComponent<ThingyManager>().stats.isDead = false;
            Enemies.Add(Enemy.GetComponent<ThingyManager>().stats);
        }
        foreach(Stats Enemy in Enemies)
        {
            Enemy.isDead = false;
        }
        enemyHPBar.FetchHP();
        CombatOrder();
        StartCoroutine(CalculateBattle());
    }

    private int CompareSpeeds(Stats _Element1, Stats _Element2)
    {
        if(_Element1.SPD < _Element2.SPD)
        {
            return 1;
        }
        else if(_Element1.SPD > _Element2.SPD)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    IEnumerator CalculateBattle()
    {
        bool clear = true;
        foreach (Stats Unit in Enemies)
        {
            if (!Unit.isDead)
            {
                clear = false;
            }
        }
        if (clear)
        {
            Enemies.Clear();
            yield break;
        }

        foreach (Stats Unit in TurnOrder)
        {
            if (playerDefeated) yield break;

            if (!Unit.isDead)
            {
                if (!Unit.isPlayer)
                {
                    //Debug.Log("enemyattack");
                    DealDamage(Unit, Player, Unit.ATK);
                    hitParticle[3].Play();
                    nmbrOfAttacks++;
                    delay = calculateDelay(nmbrOfAttacks);
                    yield return new WaitForSecondsRealtime(delay);
                }
                else
                {
                    //Debug.Log("playerattack");
                    for (int i = 0; i < Enemies.Count; i++)
                    {
                        if (!Enemies[i].isDead && Enemies[i] != null)
                        {
                            DealDamage(Unit, Enemies[i], Unit.ATK);
                            hitParticle[i].Play();
                            nmbrOfAttacks++;
                            break;
                        }
                    }
                    delay = calculateDelay(nmbrOfAttacks);
                    yield return new WaitForSecondsRealtime(delay);
                }

                foreach (Stats Enemy in Enemies)
                {
                    if (Enemy.isPoisoned)
                    {
                        Enemy.HP -= Player.poisonValue;
                        if (Enemy.HP.baseValue <= 0)
                        {
                            Enemy.isDead = true;
                            Destroy(Enemy.gameObject);
                        }
                    }
                }
            }
        }

        yield return null;
        if (Enemies.Count > 0 && !playerDefeated)
        {
            StartCoroutine(CalculateBattle());
        }
    }

    private void DealDamage(Stats _Attacker, Stats _Target, ScienceNum _ATK)
    {
        ScienceNum _damage = _ATK - _Target.DEF;
        if (_damage.baseValue < 0)
        {
            _damage.baseValue = 0;
            _damage.eFactor = 0;
        }
        _Target.HP -= _damage;

        if (GlobalGameManager.Instance.CameraFightClub.isActiveAndEnabled)
        {
            //AudioManager.instance.Play("Hit");
        }

        if (_Attacker.isPlayer)
        {
            // if player is attacking steal life
            if(_Attacker.lifesteal) _Attacker.HP += _damage * _Attacker.lifestealValue;
            if (_Attacker.HP > _Attacker.HPMAX) _Attacker.HP = _Attacker.HPMAX;
            // if player has posion, posion enemy
            if (_Attacker.poison)
            {
                _Target.isPoisoned = true;
            }
        }
        else
        {
            // if player is attacked reflect damage
            if (_Target.reflect)
            {
                _Attacker.HP -= _Target.DEF * _Target.reflectValue;
                if (_Attacker.HP.baseValue <= 0)
                {
                    _Attacker.isDead = true;
                    Destroy(_Attacker.gameObject);
                }
            }
        }
        if (_Target.HP.baseValue <= 0)
        {
            //Debug.Log("cheesus chrisp");
            _Target.HP.baseValue = 0;
            if (_Target.isPlayer)
            {
                //Debug.Log("fuck that");
                playerDefeated = true;
                //Debug.Log(EncManager.stage-1);
                //Debug.Log(nmbrOfAttacks);
                simulating = false;
                Player.HP = Player.HPMAX; // DEBUG, change to 1
                YeetEnemies();
                Player.gameObject.GetComponent<ThingyManager>().AddExp(FCManager.expGained);
                if(GlobalGameManager.Instance.maxStage < EncManager.stage) GlobalGameManager.Instance.maxStage = EncManager.stage;
                AudioManager.instance.Play("Victory");
                GlobalGameManager.Instance.EndFightCub();
            }
            else
            {
                //Debug.Log("fuck this");
                _Target.isDead = true;
                Destroy(_Target.gameObject);
            }
        }
    }

    public float calculateDelay(int _currentNmbrOfAttacks)
    {
        float _delay;

        _delay = maxDelay / (5*EncManager.CalcNmbrOfAttacks / Mathf.Sqrt(_currentNmbrOfAttacks));
        if (_delay < minDelay) _delay = minDelay;

        return _delay;
    }

    public void YeetEnemies()
    {
        foreach (Stats Enemy in Enemies)
        {
            Destroy(Enemy.gameObject);
        }
    }
}
