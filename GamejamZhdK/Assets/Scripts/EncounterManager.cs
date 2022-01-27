using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EncounterManager : MonoBehaviour
{
    // Bossstages
    private List<int> FixedStages = new List<int>();

    public Transform[] spawnSlots = new Transform[3];

    [SerializeField] private TMP_Text stageText;

    private int hpMult = 3;
    private int spdMult = 2;

    private CombatManager CManager;
    private CombatCalculator CCalc;

    private int maxEnemies = 3;
    private int nmbrOfEnemies;
    public int stage = 1;
    public int stagecalc = 1;
    private float avgEnemyLVL;
    private float lvlGrowth = 0.3f;
    private float growthMax = 0.7f;
    private float growthMin = 0.4f;

    public int CalcNmbrOfAttacks;

    
    [SerializeField] public List<List<Stats>> Stages = new List<List<Stats>>();

    public List<GameObject> Enemies = new List<GameObject>();
    

    // Start is called before the first frame update
    void Start()
    {
        CManager = GetComponent<CombatManager>();
        CCalc = GetComponent<CombatCalculator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SpawnRandomThingy(spawnSlots[0]);
            //generateEncounter();
            generateStage();
            //CCalc.nmbrOfAttacks = 0;
        }*/
        Enemies.RemoveAll(item => item == null);
    }

    public void generateStage()
    {
        List<Stats> StageSetup = new List<Stats>();
        nmbrOfEnemies = Random.Range(1, maxEnemies + 1);
        avgEnemyLVL = lvlGrowth * stagecalc;
        for (int i = 0; i < nmbrOfEnemies; i++)
        {
            Stats _temp = new Stats();
            AllocateEnemyStats(_temp);
            StageSetup.Add(_temp);
        }
        Stages.Add(StageSetup);
        CCalc.GetEnemies();
        stagecalc++;
    }

    public void generateEncounter()
    {
        Enemies.Clear();
        int i = 0;
        foreach (Stats Enemy in Stages[stage-1])
        {
            //Debug.Log(i);
            SpawnRandomThingy(Enemy, spawnSlots[i]);
            i++;
        }
        CManager.GetEnemies();
        stageText.text = "Stage: " + stage; 
        stage++;
    }

    private void SpawnRandomThingy(Stats _Enemy, Transform _spawnslot)
    {
        string[] BodypartsOut = new string[12];
        GameObject _thingy;
        int _nmbrOfAnimalTypes = (int)Animal.END - 1; // -1 bc of the "generic" animal type
        int _selectedAnimal;

        BodypartsOut[0] = SpriteManager.Instance.getRandomHead(System.Enum.GetName(typeof(Animal), Random.Range(0, _nmbrOfAnimalTypes)));
        BodypartsOut[1] = SpriteManager.Instance.getRandomBody(System.Enum.GetName(typeof(Animal), Random.Range(0, _nmbrOfAnimalTypes)));

        _selectedAnimal = Random.Range(0, _nmbrOfAnimalTypes);
        BodypartsOut[2] = SpriteManager.Instance.getRandomEye(System.Enum.GetName(typeof(Animal), _selectedAnimal));
        BodypartsOut[3] = SpriteManager.Instance.getRandomEye(System.Enum.GetName(typeof(Animal), _selectedAnimal));

        _selectedAnimal = Random.Range(0, _nmbrOfAnimalTypes);
        BodypartsOut[4] = SpriteManager.Instance.getRandomEar(System.Enum.GetName(typeof(Animal), _selectedAnimal));
        BodypartsOut[5] = SpriteManager.Instance.getRandomEar(System.Enum.GetName(typeof(Animal), _selectedAnimal));

        BodypartsOut[6] = SpriteManager.Instance.getRandomBody(System.Enum.GetName(typeof(Animal), Random.Range(0, _nmbrOfAnimalTypes)));

        _selectedAnimal = Random.Range(0, _nmbrOfAnimalTypes);
        BodypartsOut[7] = SpriteManager.Instance.getRandomArm(System.Enum.GetName(typeof(Animal), _selectedAnimal));
        BodypartsOut[8] = SpriteManager.Instance.getRandomArm(System.Enum.GetName(typeof(Animal), _selectedAnimal));

        _selectedAnimal = Random.Range(0, _nmbrOfAnimalTypes);
        BodypartsOut[9] = SpriteManager.Instance.getRandomLeg(System.Enum.GetName(typeof(Animal), _selectedAnimal));
        BodypartsOut[10] = SpriteManager.Instance.getRandomLeg(System.Enum.GetName(typeof(Animal), _selectedAnimal));

        BodypartsOut[11] = SpriteManager.Instance.getRandomTail(System.Enum.GetName(typeof(Animal), Random.Range(0, _nmbrOfAnimalTypes)));

        _thingy = SpriteManager.Instance.GenerateThingy(BodypartsOut[0], BodypartsOut[1], BodypartsOut[2], BodypartsOut[3], BodypartsOut[4], BodypartsOut[5], BodypartsOut[6], BodypartsOut[7], BodypartsOut[8], BodypartsOut[9], BodypartsOut[10], BodypartsOut[11], _spawnslot.position, "nuthn" );
        //_thingy.transform.position = _spawnslot.position;
        _thingy.transform.localScale = new Vector3(-1, 1, 1);
        _thingy.GetComponent<ColorManager>().SetColor(new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f)));

        // AllocateStats(_thingy.GetComponent<ThingyManager>().stats);
        // Fetch Stats
        _thingy.GetComponent<ThingyManager>().stats = _Enemy;
        _thingy.GetComponent<ThingyManager>().stats.HP = _thingy.GetComponent<ThingyManager>().stats.HPMAX;
        _thingy.GetComponent<ThingyManager>().stats.isDead = false;

        Destroy(_thingy.GetComponent<animalMovement>());

        Enemies.Add(_thingy);
    }

    private void AllocateEnemyStats(Stats _thingystats)
    {
        _thingystats.ATKGrowth = Random.Range(growthMin, growthMax);
        _thingystats.HPGrowth = Random.Range(growthMin * hpMult, growthMax * hpMult);
        _thingystats.SPDGrowth = Random.Range(growthMin * spdMult, growthMax * spdMult);
        _thingystats.DEFGrowth = 0;

        _thingystats.isPlayer = false;

        avgEnemyLVL = lvlGrowth * stage;
        _thingystats.LVL = (int)avgEnemyLVL;

        _thingystats.ATK = (StatsManager.Instance.baseStatLevel + _thingystats.LVL) * _thingystats.ATKGrowth;
        _thingystats.DEF = 0;
        _thingystats.HPMAX = (StatsManager.Instance.baseStatLevel + _thingystats.LVL) * _thingystats.HPGrowth;
        _thingystats.HP = _thingystats.HPMAX;
        _thingystats.SPD = (StatsManager.Instance.baseStatLevel + _thingystats.LVL) * _thingystats.SPDGrowth;

        StatsManager.Instance.UpdateStats(_thingystats);
    }
}
