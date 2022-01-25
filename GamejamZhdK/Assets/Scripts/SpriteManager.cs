using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum Animal
{
    weasel,
    frog,
    rat
}

[System.Serializable]
public class BodyPart
{
    public Animal animal;
    public string name;
    public Sprite sprite;
}

[System.Serializable]
public class BodyPartGO
{
    public Animal animal;
    public string name;
    public GameObject prefab;
}

public class SpriteManager : MonoBehaviour
{
    [SerializeField] private GameObject ThingyPrefab;

    public List<BodyPart> Ears = new List<BodyPart>();
    public List<BodyPart> Eyes = new List<BodyPart>();
    public List<BodyPart> Mouths = new List<BodyPart>();
    public List<BodyPart> Arms = new List<BodyPart>();
    public List<BodyPart> Legs = new List<BodyPart>();
    public List<BodyPart> Tails = new List<BodyPart>();

    public List<BodyPartGO> Heads = new List<BodyPartGO>();
    public List<BodyPartGO> Bodies = new List<BodyPartGO>();

    private Dictionary<string, Sprite> EarDict = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> EyeDict = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> MouthDict = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> ArmDict = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> LegDict = new Dictionary<string, Sprite>();
    private Dictionary<string, Sprite> TailDict = new Dictionary<string, Sprite>();

    private Dictionary<string, GameObject> HeadDict = new Dictionary<string, GameObject>();
    private Dictionary<string, GameObject> BodyDict = new Dictionary<string, GameObject>();

    #region Singleton
    public static SpriteManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        FillDictionaries();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FillDictionaries()
    {
        foreach(BodyPart ear in Ears)
        {
            EarDict.Add(ear.name, ear.sprite);
        }
        foreach (BodyPart eye in Eyes)
        {
            EyeDict.Add(eye.name, eye.sprite);
        }
        foreach (BodyPart mouth in Mouths)
        {
            MouthDict.Add(mouth.name, mouth.sprite);
        }
        foreach (BodyPart arm in Arms)
        {
            ArmDict.Add(arm.name, arm.sprite);
        }
        foreach (BodyPart leg in Legs)
        {
            LegDict.Add(leg.name, leg.sprite);
        }
        foreach (BodyPart tail in Tails)
        {
            TailDict.Add(tail.name, tail.sprite);
        }
        foreach (BodyPartGO head in Heads)
        {
            HeadDict.Add(head.name, head.prefab);
        }
        foreach (BodyPartGO body in Bodies)
        {
            BodyDict.Add(body.name, body.prefab);
        }
    }

    public void GenerateThingy(string _head, string _body, string _eye1, string _eye2, string _ear1, string _ear2, string _mouth, string _arm1, string _arm2, string _leg1, string _leg2, string _tail)
    {
        BodyParts bodyParts;
        GameObject _thingy;
        GameObject body;
        GameObject head;
        GameObject temp;
        string temp2;

        Sprite eye1;
        Sprite eye2;
        Sprite ear1;
        Sprite ear2;
        Sprite mouth;

        Sprite arm1;
        Sprite arm2;
        Sprite leg1;
        Sprite leg2;
        Sprite tail;

        _thingy = Instantiate(ThingyPrefab);
        bodyParts = _thingy.GetComponent<BodyParts>();

        BodyDict.TryGetValue(_body, out temp);
        body = Instantiate(temp, _thingy.transform);

        HeadDict.TryGetValue(_head, out temp);
        head = Instantiate(temp, body.transform);
        head.transform.position = body.transform.GetChild(0).transform.position;

        EyeDict.TryGetValue(_eye1, out eye1);
        EyeDict.TryGetValue(_eye2, out eye2);

        EarDict.TryGetValue(_ear1, out ear1);
        EarDict.TryGetValue(_ear2, out ear2);

        MouthDict.TryGetValue(_mouth, out mouth);

        head.GetComponent<HeadSprites>().ChangeSprites(eye1, eye2, ear1, ear2, mouth);

        ArmDict.TryGetValue(_arm1, out arm1);
        ArmDict.TryGetValue(_arm2, out arm2);

        LegDict.TryGetValue(_leg1, out leg1);
        LegDict.TryGetValue(_leg2, out leg2);

        TailDict.TryGetValue(_tail, out tail);

        body.GetComponent<BodySprites>().ChangeSprites(arm1, arm2, leg1, leg2, tail);

        bodyParts.body = _body;
        bodyParts.head = _head;
        bodyParts.eye1 = _eye1;
        bodyParts.eye2 = _eye2;
        bodyParts.ear1 = _ear1;
        bodyParts.ear2 = _ear2;
        bodyParts.mouth = _mouth;
        bodyParts.arm1 = _arm1;
        bodyParts.arm2 = _arm2;
        bodyParts.leg1 = _leg1;
        bodyParts.leg2 = _leg2;
        bodyParts.tail = _tail;
    }

    public string getRandomBody(string animal)
    {
        return getRandomBodyPart(animal, Bodies);
    }

    public string getRandomHead(string animal)
    {
        return getRandomBodyPart(animal, Heads);
    }

    public string getRandomArm(string animal)
    {
        return getRandomBodyPart(animal, Arms);
    }

    public string getRandomLeg(string animal)
    {
        return getRandomBodyPart(animal, Legs);
    }

    public string getRandomTail(string animal)
    {
        return getRandomBodyPart(animal, Tails);
    }

    public string getRandomEye(string animal)
    {
        return getRandomBodyPart(animal, Eyes);
    }

    public string getRandomEar(string animal)
    {
        return getRandomBodyPart(animal, Ears);
    }

    public string getRandomMouth(string animal)
    {
        return getRandomBodyPart(animal, Mouths);
    }

    public string getRandomBodyPart(string animal, List<BodyPart> listOfParts) 
    {
        List<BodyPart> newList = new List<BodyPart>();
        foreach(BodyPart part in listOfParts)
        {
            if (System.Enum.GetName(typeof(Animal), part.animal).Equals(animal))
            {
                newList.Add(part);
            }
        }
        if(newList.Count > 0)
        {
            return newList[Random.Range(0, newList.Count)].name;
        }
        Debug.Log("No parts of animal type" + animal + "found");
        return "No parts of animal type" + animal + "found";
    }

    public string getRandomBodyPart(string animal, List<BodyPartGO> listOfParts)
    {
        List<BodyPartGO> newList = new List<BodyPartGO>();
        foreach (BodyPartGO part in listOfParts)
        {
            if (System.Enum.GetName(typeof(Animal), part.animal).Equals(animal))
            {
                newList.Add(part);
            }
        }
        if (newList.Count > 0)
        {
            return newList[Random.Range(0, newList.Count)].name;
        }
        Debug.Log("No parts of animal type" + animal + "found");
        return "No parts of animal type" + animal + "found";
    }
}
