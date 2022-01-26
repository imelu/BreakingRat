using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCalculator : MonoBehaviour
{
    public FightClubManager FCManager;
    public EncounterManager EncManager;


    [SerializeField] private Stats Player;

    public List<Stats> Enemies = new List<Stats>();
    public List<Stats> TurnOrder = new List<Stats>();

    private bool encounterClear = true;

    public int nmbrOfAttacks = 0;

    private bool calculating = true;
    private bool playerDefeated = false;

    // Start is called before the first frame update
    void Start()
    {
        FCManager = GetComponent<FightClubManager>();
        EncManager = GetComponent<EncounterManager>();

        Player = FCManager.PlayerThingy.GetComponent<ThingyManager>().stats;
    }

    // Update is called once per frame
    void Update()
    {
        //Enemies.RemoveAll(item => item == null);
        TurnOrder.RemoveAll(item => item == null);
        

        if (Input.GetKeyDown(KeyCode.C))
        {
            //CalculateBattle();
        }

        if (Enemies.Count <= 0 && calculating)
        {
            //encounterClear = true;
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
        foreach (Stats Enemy in EncManager.Stages[EncManager.stagecalc - 1])
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
                    Debug.Log("enemyattack");
                    DealDamage(Player, Unit.ATK);
                    nmbrOfAttacks++;
                }
                else
                {
                    Debug.Log("playerattack");
                    for(int i = 0; i < Enemies.Count; i++)
                    {
                        if (!Enemies[i].isDead && Enemies[i] != null)
                        {
                            DealDamage(Enemies[i], Unit.ATK);
                            nmbrOfAttacks++;
                            break;
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

    private void DealDamage(Stats _Target, float _ATK)
    {
        float _damage = _ATK - _Target.DEF;
        if (_damage < 0) _damage = 0;
        _Target.HP -= _damage;
        if (_Target.HP <= 0)
        {
            _Target.HP = 0;
            if (_Target.isPlayer)
            {
                playerDefeated = true;
                Debug.Log(EncManager.stagecalc);
                Debug.Log(nmbrOfAttacks);
                _Target.HP = _Target.HPMAX;
                calculating = false;
                EncManager.generateEncounter();
            }
            else
            {
                _Target.isDead = true;
            }
        }
    }
}
