using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public FightClubManager FCManager;
    public EncounterManager EncManager;
    private EnemyHPBar enemyHPBar;

    [SerializeField] List<ParticleSystem> hitParticle = new List<ParticleSystem>();


    private Stats Player;

    public List<Stats> Enemies = new List<Stats>();

    public List<Stats> TurnOrder = new List<Stats>();
    
    private bool encounterClear = true;

    private int nmbrOfAttacks = 0;

    private bool simulating = false;

    private bool playerDefeated = false;

    private float delay;
    private float maxDelay = 0.5f;

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
            encounterClear = true;
            EncManager.generateEncounter();
        }

        delay = calculateDelay(nmbrOfAttacks);
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
                    Debug.Log("enemyattack");
                    DealDamage(Unit, Player, Unit.ATK);
                    hitParticle[3].Play();
                    nmbrOfAttacks++;
                    yield return new WaitForSecondsRealtime(delay);
                }
                else
                {
                    Debug.Log("playerattack");
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
                    yield return new WaitForSecondsRealtime(delay);
                }

                foreach (Stats Enemy in Enemies)
                {
                    if (Enemy.isPoisoned)
                    {
                        Enemy.HP -= Player.poisonValue;
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

    private void DealDamage(Stats _Attacker, Stats _Target, float _ATK)
    {
        float _damage = _ATK - _Target.DEF;
        if (_damage < 0) _damage = 0;
        _Target.HP -= _damage;

        if (!_Target.isPlayer)
        {
            // if player is attacking steal life
            _Attacker.HP += _damage * _Attacker.lifestealValue;
            // if player has posion, posion enemy
            if (_Attacker.poison)
            {
                _Target.isPoisoned = true;
            }
        }
        else
        {
            // if player is attacked reflect damage
            _Attacker.HP -= _Target.DEF * _Target.reflectValue;
        }
        if (_Target.HP <= 0)
        {
            _Target.HP = 0;
            if (_Target.isPlayer)
            {
                playerDefeated = true;
                Debug.Log(EncManager.stage-1);
                Debug.Log(nmbrOfAttacks);
                simulating = false;
            }
            else
            {
                _Target.isDead = true;
                Destroy(_Target.gameObject);
            }
        }
    }

    public float calculateDelay(int _currentNmbrOfAttacks)
    {
        return maxDelay / (EncManager.CalcNmbrOfAttacks / Mathf.Sqrt(_currentNmbrOfAttacks));
    }
}
