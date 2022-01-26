using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public FightClubManager FCManager;
    public EncounterManager EncManager;


    [SerializeField] private ThingyManager Player;

    public List<ThingyManager> Enemies = new List<ThingyManager>();

    public List<ThingyManager> TurnOrder = new List<ThingyManager>();

    private bool encounterClear = true;

    private int nmbrOfAttacks = 0;


    private bool playerDefeated = false;

    // Start is called before the first frame update
    void Start()
    {
        FCManager = GetComponent<FightClubManager>();
        EncManager = GetComponent<EncounterManager>();

        Player = FCManager.PlayerThingy.GetComponent<ThingyManager>();
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
        //TurnOrder.RemoveAll(item => item);
        TurnOrder.Add(Player);

        foreach (ThingyManager Enemy in Enemies)
        {
            TurnOrder.Add(Enemy);
        }
        TurnOrder.Sort(CompareSpeeds);
    }

    public void GetEnemies()
    {
        foreach (GameObject Enemy in EncManager.Enemies)
        {
            Enemies.Add(Enemy.GetComponent<ThingyManager>());
        }
        CombatOrder();
        CalculateBattle();
    }

    private int CompareSpeeds(ThingyManager _Element1, ThingyManager _Element2)
    {
        if(_Element1.stats.SPD < _Element2.stats.SPD)
        {
            return 1;
        }
        else if(_Element1.stats.SPD > _Element2.stats.SPD)
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

        foreach (ThingyManager Unit in TurnOrder)
        {
            nmbrOfAttacks++;
            if (!Unit.stats.isPlayer)
            {
                Debug.Log("enemyattack");
                DealDamage(Player, Unit.stats.ATK);
            }
            else
            {
                Debug.Log("playerattack");
                DealDamage(Enemies[Random.Range(0, Enemies.Count)], Unit.stats.ATK);
            }
        }


        if (Enemies.Count <= 0)
        {
            encounterClear = true;
        }
    }

    private void DealDamage(ThingyManager _Target, float _ATK)
    {
        float _damage = _ATK - _Target.stats.DEF;
        if (_damage < 0) _damage = 0;
        _Target.stats.HP -= _damage;
        if(_Target.stats.HP <= 0)
        {
            _Target.stats.HP = 0;
            if (_Target.stats.isPlayer)
            {
                playerDefeated = true;
            }
            else
            {
                //Destroy(_Target.gameObject);
            }
        }
    }
}
