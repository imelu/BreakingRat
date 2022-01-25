using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    // Bossstages
    private List<int> FixedStages = new List<int>();

    [SerializeField] private Transform[] spawnSlots = new Transform[3];

    private int maxEnemies = 3;
    private int nmbrOfEnemies;
    private int stage;
    private int avgEnemyLVL;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //SpawnRandomThingy(spawnSlots[0]);
            generateEncounter();
        }
    }

    public void generateEncounter()
    {
        nmbrOfEnemies = Random.Range(1, maxEnemies + 1);
        for(int i = 0; i<nmbrOfEnemies; i++)
        {
            SpawnRandomThingy(spawnSlots[i]);
        }


    }

    private void SpawnRandomThingy(Transform _spawnslot)
    {
        string[] BodypartsOut = new string[12];
        GameObject _thingy;
        int _nmbrOfAnimalTypes = (int)Animal.END;
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

        _thingy = SpriteManager.Instance.GenerateThingy(BodypartsOut[0], BodypartsOut[1], BodypartsOut[2], BodypartsOut[3], BodypartsOut[4], BodypartsOut[5], BodypartsOut[6], BodypartsOut[7], BodypartsOut[8], BodypartsOut[9], BodypartsOut[10], BodypartsOut[11]);
        _thingy.transform.position = _spawnslot.position;
        _thingy.transform.localScale = new Vector3(-1, 1, 1);
        _thingy.GetComponent<ColorManager>().SetColor(new Color(Random.Range(0f,1f), Random.Range(0f,1f), Random.Range(0f,1f)));
    }
}
