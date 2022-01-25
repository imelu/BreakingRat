using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitSelector : MonoBehaviour
{
    List<string> AllTraitsWithParentMainbody;
    List<string> ParentTraits;
    public List<Sprite> BodySprites;
    string parent1animal;
    string parent2animal;
    public string mainAnimalType;
    string Mainbody;
    
    string[] Bodyparts = { "head", "eye1", "eye2", "ear1", "ear2", "mouth", "tails" };
    int GetRandomValue()
    {
        float rand = Random.value;
        if (rand <= .45f)
            return 1;
        if (rand <= .9f)
            return 2;

        return 3;
    }

    void Start()
    {
        
    }
    public void SetTraits(GameObject Parent1, GameObject Parent2)
    {
        //parent1animal = Parent1
        //parent2animal = Parent2
        GameObject[] Parents = { Parent1, Parent2 };
        GameObject ChosenParent;
        int index = Random.Range(0, 2);
        ChosenParent = Parents[index];
        mainAnimalType = ChosenParent.GetComponent<TraitSelector>().mainAnimalType;

        int bodyPartCounter=0;
        foreach(string bodypart in Bodyparts)
        {
            bodyPartCounter ++;
            
            string animalType;

            //1==parent1
            //2==parent2
            //3==random
            int traitOrigin = GetRandomValue();
            if (traitOrigin == 1)
            {
                animalType = Parent1.GetComponent<TraitSelector>().Mainbody;
                //Bodyparts[bodyPartCounter]
                //get Bodypart with index bodyPartCounter of Parent1
            }
            else if(traitOrigin == 2)
            {
                animalType = Parent2.GetComponent<TraitSelector>().Mainbody;
                //get Bodypart with index bodyPartCounter of Parent2
            }
            else if (traitOrigin == 3)
            {
                index = Random.Range(0, 2);
                ChosenParent = Parents[index];
                animalType = ChosenParent.GetComponent<TraitSelector>().Mainbody;
                //get Random bodypart with bodypartindex bodyPartCounter and filtered animalType
            }

            //FilteredAnimalList.Length
            int randomPicker = Random.Range(0, ParentTraits.Count);
        }
        bodyPartCounter = 0;
        //SpriteManager.GenerateThingy()

    }
    
    void Update()
    {
        
    }
}
