using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    // Bossstages
    private List<int> FixedStages = new List<int>();

    [SerializeField] private Transform[] spawnSlots = new Transform[3];

    private CombatManager CManager;

    private int maxEnemies = 3;
    private int nmbrOfEnemies;
    private int stage = 1;
    private float avgEnemyLVL;
    private float lvlGrowth = 0.3f;
    private float growthMax = 0.7f;
    private float growthMin = 0.4f;
    

    public List<GameObject> Enemies = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        CManager = GetComponent<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SpawnRandomThingy(spawnSlots[0]);
            generateEncounter();
        }

        
        /*
        if (encounterClear)
        {
            generateEncounter();
        }*/

        Enemies.RemoveAll(item => item == null);
    }

    public void generateEncounter()
    {
        //encounterClear = false;
        nmbrOfEnemies = Random.Range(1, maxEnemies + 1);
        avgEnemyLVL = lvlGrowth * stage;
        for (int i = 0; i < nmbrOfEnemies; i++)
        {
            SpawnRandomThingy(spawnSlots[i]);
        }
        CManager.GetEnemies();
        stage++;
    }

    private void SpawnRandomThingy(Transform _spawnslot)
    {
        string[] BodypartsOut = new string[12];
        GameObject _thingy;
        int _nmbrOfAnimalTypes = (int)Animal.END - 1;
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

        _thingy = SpriteManager.Instance.GenerateThingy(BodypartsOut[0], BodypartsOut[1], BodypartsOut[2], BodypartsOut[3], BodypartsOut[4], BodypartsOut[5], BodypartsOut[6], BodypartsOut[7], BodypartsOut[8], BodypartsOut[9], BodypartsOut[10], BodypartsOut[11], _spawnslot);
        //_thingy.transform.position = _spawnslot.position;
        _thingy.transform.localScale = new Vector3(-1, 1, 1);
        _thingy.GetComponent<ColorManager>().SetColor(new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f)));

        AllocateStats(_thingy.GetComponent<Stats>());

        Enemies.Add(_thingy);
    }

    private void AllocateStats(Stats _thingystats)
    {
        _thingystats.ATKGrowth = Random.Range(growthMin, growthMax);
        _thingystats.HPGrowth = Random.Range(growthMin, growthMax);
        _thingystats.SPDGrowth = Random.Range(growthMin, growthMax);
        _thingystats.CRITGrowth = Random.Range(growthMin, growthMax);
        _thingystats.DEFGrowth = 0;

        _thingystats.isPlayer = false;

        _thingystats.LVL = (int)avgEnemyLVL;

        _thingystats.UpdateStats();
    }
}
