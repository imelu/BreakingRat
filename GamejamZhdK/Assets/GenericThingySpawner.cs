using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericThingySpawner : MonoBehaviour
{
    string weasel = "weasel1";
    string frog = "frog1";
    string rat = "rat1";
    string fatfrog = "fatfrog1";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateWeasel(Vector3 _position)
    {
        List<string> BodypartsOut = new List<string>();
        for(int i = 0; i < 12; i++)
        {
            BodypartsOut.Add(weasel);
        }
        GameObject child;
        child = SpriteManager.Instance.GenerateThingy(weasel, weasel, weasel, weasel, weasel, weasel, weasel, weasel, weasel, weasel, weasel, weasel, _position, "weasel");
        StatsManager.Instance.UpdateStats(child.GetComponent<ThingyManager>().stats);
        ThingyData _data = new ThingyData(child.GetComponent<ThingyManager>().stats, BodypartsOut, "weasel");
        child.GetComponent<ThingyManager>().data = _data;
        CurrentThingies.Instance.AddThingy(_data);
        GlobalGameManager.Instance.SaveData();
    }

    public void GenerateRat(Vector3 _position)
    {
        List<string> BodypartsOut = new List<string>();
        for (int i = 0; i < 12; i++)
        {
            BodypartsOut.Add(rat);
        }
        GameObject child;
        child = SpriteManager.Instance.GenerateThingy(rat, rat, rat, rat, rat, rat, rat, rat, rat, rat, rat, rat, _position, "rat");
        StatsManager.Instance.UpdateStats(child.GetComponent<ThingyManager>().stats);
        ThingyData _data = new ThingyData(child.GetComponent<ThingyManager>().stats, BodypartsOut, "rat");
        child.GetComponent<ThingyManager>().data = _data;
        CurrentThingies.Instance.AddThingy(_data);
        GlobalGameManager.Instance.SaveData();
    }

    public void GenerateFrog(Vector3 _position)
    {
        List<string> BodypartsOut = new List<string>();
        for (int i = 0; i < 12; i++)
        {
            BodypartsOut.Add(frog);
        }
        GameObject child;
        child = SpriteManager.Instance.GenerateThingy(frog, frog, frog, frog, frog, frog, frog, frog, frog, frog, frog, frog, _position, "frog");
        StatsManager.Instance.UpdateStats(child.GetComponent<ThingyManager>().stats);
        ThingyData _data = new ThingyData(child.GetComponent<ThingyManager>().stats, BodypartsOut, "frog");
        child.GetComponent<ThingyManager>().data = _data;
        CurrentThingies.Instance.AddThingy(_data);
        GlobalGameManager.Instance.SaveData();
    }

    public void GenerateFatFrog(Vector3 _position)
    {
        List<string> BodypartsOut = new List<string>();
        for (int i = 0; i < 12; i++)
        {
            BodypartsOut.Add(fatfrog);
        }
        GameObject child;
        child = SpriteManager.Instance.GenerateThingy(fatfrog, fatfrog, fatfrog, fatfrog, fatfrog, fatfrog, fatfrog, fatfrog, fatfrog, fatfrog, fatfrog, fatfrog, _position, "fatfrog");
        StatsManager.Instance.UpdateStats(child.GetComponent<ThingyManager>().stats);
        ThingyData _data = new ThingyData(child.GetComponent<ThingyManager>().stats, BodypartsOut, "fatfrog");
        child.GetComponent<ThingyManager>().data = _data;
        CurrentThingies.Instance.AddThingy(_data);
        GlobalGameManager.Instance.SaveData();
    }
}
