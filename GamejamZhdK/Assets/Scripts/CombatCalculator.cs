using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigNums;

public class CombatCalculator : MonoBehaviour
{
    public FightClubManager FCManager;
    public EncounterManager EncManager;
    private CombatManager CManager;
    [SerializeField] private Timer timer;

    [SerializeField] private Stats Player;

    public List<Stats> Enemies = new List<Stats>();
    public List<Stats> TurnOrder = new List<Stats>();

    public int nmbrOfAttacks = 0;

    private bool calculating = true;
    private bool playerDefeated = false;

    private float timeOfBattle;

    private ScienceNum gainedExp;
    private ScienceNum expMult;

    // Start is called before the first frame update
    void Start()
    {
        FCManager = GetComponent<FightClubManager>();
        EncManager = GetComponent<EncounterManager>();
        CManager = GetComponent<CombatManager>();
        Player = FCManager.PlayerThingy.GetComponent<ThingyManager>().stats;
        expMult.baseValue = 8f;
        if (Player.looter) expMult.baseValue *= 0.25f;
    }

    // Update is called once per frame
    void Update()
    {
        //Enemies.RemoveAll(item => item == null);
        TurnOrder.RemoveAll(item => item == null);
        

        if (Input.GetKeyDown(KeyCode.V))
        {
            //CalculateBattle();
            //calculating = true;
        }

        if (Enemies.Count <= 0 && calculating)
        {
            TurnOrder.Clear();
            EncManager.generateStage();
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
        Player.isDead = false;
        foreach (Stats Enemy in EncManager.Stages[(int)EncManager.stagecalc.Conversion() - 1])
        {
            Enemies.Add(Enemy);
        }
        CombatOrder();
        CalculateBattle();
    }

    private int CompareSpeeds(Stats _Element1, Stats _Element2)
    {
        if (_Element1.SPD < _Element2.SPD)
        {
            return 1;
        }
        else if (_Element1.SPD > _Element2.SPD)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    private void CalculateBattle()
    {
        bool clear = true;
        foreach(Stats Unit in Enemies)
        {
            if (!Unit.isDead)
            {
                clear = false;
            }
        }
        if (clear)
        {
            foreach (Stats Unit in Enemies)
            {
                Unit.isDead = false;
            }
            Enemies.Clear();
            return;
        }

        foreach (Stats Unit in TurnOrder)
        {
            if (playerDefeated) return;
            if (!Unit.isDead)
            {
                if (!Unit.isPlayer)
                {
                    //Debug.Log("enemyattack");
                    DealDamage(Unit, Player, Unit.ATK);
                    nmbrOfAttacks++;
                }
                else
                {
                    //Debug.Log("playerattack");
                    for(int i = 0; i < Enemies.Count; i++)
                    {
                        if (!Enemies[i].isDead && Enemies[i] != null)
                        {
                            DealDamage(Unit, Enemies[i], Unit.ATK);
                            nmbrOfAttacks++;
                            break;
                        }
                    }
                }

                foreach(Stats Enemy in Enemies)
                {
                    if (Enemy.isPoisoned && !Enemy.isPlayer)
                    {
                        Enemy.HP -= Player.poisonValue;
                        if(Enemy.HP.baseValue <= 0)
                        {
                            gainedExp += expMult * Enemy.LVL;
                            Enemy.isDead = true;
                        }
                    }   
                }
            }
        }

        if (Enemies.Count > 0 && !playerDefeated)
        {
            CalculateBattle();
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

        if (_Attacker.isPlayer)
        {
            // if player is attacking steal life
            if (_Attacker.lifesteal) _Attacker.HP += _damage * _Attacker.lifestealValue;
            if (_Attacker.HP > _Attacker.HPMAX) _Attacker.HP = _Attacker.HPMAX;
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
                    gainedExp += expMult * _Target.LVL;
                    _Attacker.isDead = true;
                }
            }
        }
        if (_Target.HP.baseValue <= 0)
        {
            _Target.HP.baseValue = 0;
            if (_Target.isPlayer)
            {
                playerDefeated = true;
                Debug.Log(EncManager.stagecalc);
                Debug.Log(nmbrOfAttacks);
                EncManager.CalcNmbrOfAttacks = nmbrOfAttacks;
                _Target.HP = _Target.HPMAX;
                calculating = false;

                for(int i = 0; i<nmbrOfAttacks; i++)
                {
                    float _time = CManager.calculateDelay(i);
                    if (_time == 0.01f) _time *= 3;
                    timeOfBattle += _time;
                }
                timer.StartTimer(timeOfBattle);
                FCManager.expGained = gainedExp;
                GlobalGameManager.Instance.SetFightClubCamera();
                EncManager.generateEncounter();
            }
            else
            {
                gainedExp += expMult * _Target.LVL;
                _Target.isDead = true;
            }
        }
    }
}
