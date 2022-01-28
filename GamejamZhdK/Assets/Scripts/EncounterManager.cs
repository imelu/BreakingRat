using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using BigNums;

public class EncounterManager : MonoBehaviour
{
    // Bossstages

    public Transform[] spawnSlots = new Transform[3];

    [SerializeField] private TMP_Text stageText;

    private int hpMult = 3;
    private int spdMult = 2;

    private CombatManager CManager;
    private CombatCalculator CCalc;

    private int maxEnemies = 3;
    private int nmbrOfEnemies;
    public int stage = 1;
    public ScienceNum stagecalc;// = 1;
    private ScienceNum one;
    private ScienceNum avgEnemyLVL;
    private ScienceNum lvlGrowth;// = 5f;
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

        stagecalc.baseValue = 1;
        stagecalc.eFactor = 0;

        lvlGrowth.baseValue = 5;
        lvlGrowth.eFactor = 0;

        one.baseValue = 1;
        one.eFactor = 0;
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
        stagecalc += one;
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

        _selectedAnimal = Random.Range(0, _nmbrOfAnimalTypes);
        string animalColor = System.Enum.GetName(typeof(Animal), (Animal)_selectedAnimal);
        BodypartsOut[1] = SpriteManager.Instance.getRandomBody(System.Enum.GetName(typeof(Animal), _selectedAnimal));

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

        _thingy = SpriteManager.Instance.GenerateThingy(BodypartsOut[0], BodypartsOut[1], BodypartsOut[2], BodypartsOut[3], BodypartsOut[4], BodypartsOut[5], BodypartsOut[6], BodypartsOut[7], BodypartsOut[8], BodypartsOut[9], BodypartsOut[10], BodypartsOut[11], _spawnslot.position, animalColor);
        //_thingy.transform.position = _spawnslot.position;
        _thingy.transform.localScale = new Vector3(-1, 1, 1);
        //_thingy.GetComponent<ColorManager>().SetColor(new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f)));

        // AllocateStats(_thingy.GetComponent<ThingyManager>().stats);
        // Fetch Stats
        _thingy.GetComponent<ThingyManager>().InitializeNewThingyData();

        _thingy.GetComponent<ThingyManager>().stats = _Enemy;
        _thingy.GetComponent<ThingyManager>().stats.isPoisoned = false;
        _thingy.GetComponent<ThingyManager>().stats.HP = _thingy.GetComponent<ThingyManager>().stats.HPMAX;
        _thingy.GetComponent<ThingyManager>().stats.isDead = false;

        _thingy.GetComponent<ThingyManager>().stats.gameObject = _thingy;

        Destroy(_thingy.GetComponent<animalMovement>());
        Destroy(_thingy.GetComponent<layerOrderScript>());

        _thingy.transform.parent = GlobalGameManager.Instance.FightClub.transform;

        Enemies.Add(_thingy);
    }

    private void AllocateEnemyStats(Stats _thingystats)
    {
        _thingystats.ATKGrowth.baseValue = Random.Range(growthMin, growthMax);
        _thingystats.ATKGrowth.eFactor = 0;
        _thingystats.HPGrowth.baseValue = Random.Range(growthMin * hpMult, growthMax * hpMult);
        _thingystats.HPGrowth.eFactor = 0;
        _thingystats.SPDGrowth.baseValue = Random.Range(growthMin * spdMult, growthMax * spdMult);
        _thingystats.SPDGrowth.eFactor = 0;
        _thingystats.DEFGrowth.baseValue = 0;
        _thingystats.DEFGrowth.eFactor = 0;

        _thingystats.isPlayer = false;

        avgEnemyLVL = lvlGrowth * stagecalc;// * stagecalc;
        if (avgEnemyLVL < one) avgEnemyLVL = one;
        _thingystats.LVL = avgEnemyLVL;

        _thingystats.ATK = (StatsManager.Instance.baseStatLevel + _thingystats.LVL) * _thingystats.ATKGrowth;
        _thingystats.DEF.baseValue = 0;
        _thingystats.DEF.eFactor = 0;
        _thingystats.HPMAX = (StatsManager.Instance.baseStatLevel + _thingystats.LVL) * _thingystats.HPGrowth;
        _thingystats.HP = _thingystats.HPMAX;
        _thingystats.SPD = (StatsManager.Instance.baseStatLevel + _thingystats.LVL) * _thingystats.SPDGrowth;

        StatsManager.Instance.UpdateStats(_thingystats);
    }
}
