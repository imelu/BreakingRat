using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public FightClubManager FCManager;
    public EncounterManager EncManager;


    [SerializeField] private Stats Player;

    public List<Stats> Enemies = new List<Stats>();

    public List<Stats> TurnOrder = new List<Stats>();

    private bool encounterClear = true;

    private int nmbrOfAttacks = 0;


    private bool playerDefeated = false;

    // Start is called before the first frame update
    void Start()
    {
        FCManager = GetComponent<FightClubManager>();
        EncManager = GetComponent<EncounterManager>();

        Player = FCManager.PlayerThingy.GetComponent<Stats>();
    }

    // Update is called once per frame
    void Update()
    {
        Enemies.RemoveAll(item => item == null);
        TurnOrder.RemoveAll(item => item == null);
        if (Input.GetKeyDown(KeyCode.C))
        {
            CalculateBattle();
        }
        if(Enemies.Count > 0 && !playerDefeated)
        {
            CalculateBattle();
        }
    }

    private void CombatOrder()
    {
        TurnOrder.RemoveAll(item => item);
        TurnOrder.Add(Player);

        foreach (Stats Enemy in Enemies)
        {
            TurnOrder.Add(Enemy);
        }
        TurnOrder.Sort(CompareSpeeds);
    }

    public void GetEnemies()
    {
        foreach (GameObject Enemy in EncManager.Enemies)
        {
            Enemies.Add(Enemy.GetComponent<Stats>());
        }
        CombatOrder();
        CalculateBattle();
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

    private void CalculateBattle()
    {
        nmbrOfAttacks = 0;

        foreach (Stats Unit in TurnOrder)
        {
            nmbrOfAttacks++;
            if (!Unit.isPlayer)
            {
                Debug.Log("enemyattack");
                DealDamage(Player, Unit.ATK);
            }
            else
            {
                Debug.Log("playerattack");
                DealDamage(Enemies[Random.Range(0, Enemies.Count)], Unit.ATK);
            }
        }


        if (Enemies.Count <= 0)
        {
            encounterClear = true;
        }
    }

    private void DealDamage(Stats _Target, float _ATK)
    {
        float _damage = _ATK - _Target.DEF;
        if (_damage < 0) _damage = 0;
        _Target.HP -= _damage;
        if(_Target.HP <= 0)
        {
            _Target.HP = 0;
            if (_Target.isPlayer)
            {
                playerDefeated = true;
            }
            else
            {
                Destroy(_Target.gameObject);
            }
        }
    }
}