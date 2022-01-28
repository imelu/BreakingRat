using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TraitSelector : MonoBehaviour
{
    List<string> AllTraitsWithParentMainbody;
    List<string> ParentTraits;
    public List<Sprite> BodySprites;
    public string mainAnimalType;
    string mainChildAnimalType;
    GameObject eyeparent;
    GameObject earparent;
    GameObject armparent;
    GameObject legparent;

    GameObject Parent1;
    GameObject Parent2;
    public string SLegparent1;
    public string SLegparent2;

    string[] BodypartsIn = { "head", "body", "eye1", "eye2", "ear1", "ear2", "mouth","arm1", "arm2", "leg1", "leg2", "tail" };
    string[] BodypartsOut = { "head", "body", "eye1", "eye2", "ear1", "ear2", "mouth", "arm1", "arm2", "leg1", "leg2", "tail" };
    //string[] BodypartsRandom = { "Heads", "Bodies", "Eyes", "Eyes", "Ears", "Ears", "Mouths", "Arms", "Arms", "Legs", "Legs", "Tails" };
    //body, head gameobject

    int GetRandomValue()
    {
        /*
        int randomanimal = Random.Range(1, 4);
        if (randomanimal==1)
        {
            mainAnimalType =  "weasel"; 
        }
        else if (randomanimal == 2)
        {
            mainAnimalType = "frog";
        }
        else if (randomanimal == 3)
        {
            mainAnimalType = "rat";
        }
        */
        
        float rand = Random.value;
        if (rand <= .47f)
            return 1;
        if (rand <= .94f)
            return 2;
        
        return 3;
    }
    
    public void SetTraits(GameObject Parent1Local, GameObject Parent2Local)
    {
        Parent1 = Parent1Local;
        Parent2 = Parent2Local;
        GameObject[] Parents = { Parent1, Parent2 };

        /*
        GameObject ChosenParent;
        int index = Random.Range(0, 2);
        ChosenParent = Parents[index];
        */


        int bodyPartCounter=0;
        foreach(string bodypart in BodypartsIn)
        {
           
            

            //1==parent1
            //2==parent2
            //3==random
            int traitOrigin = GetRandomValue();

            if (bodypart == "eye1")
            {
                bodyPart1(traitOrigin, bodyPartCounter, Parents, bodypart);
            }
            else if (bodypart == "eye2")
            {
                bodyPart2(traitOrigin, bodyPartCounter, bodypart);
            }

            else if (bodypart == "ear1")
            {
                bodyPart1(traitOrigin, bodyPartCounter, Parents, bodypart);
            }
            else if (bodypart == "ear2")
            {
                bodyPart2(traitOrigin, bodyPartCounter, bodypart);
            }

            else if (bodypart == "arm1")
            {
                bodyPart1(traitOrigin, bodyPartCounter, Parents, bodypart);
            }
            else if (bodypart == "arm2")
            {
                bodyPart2(traitOrigin, bodyPartCounter, bodypart);
            }

            else if (bodypart == "leg1")
            {
                bodyPart1(traitOrigin, bodyPartCounter, Parents, bodypart);
            }
            else if (bodypart == "leg2")
            {
                bodyPart2(traitOrigin, bodyPartCounter, bodypart);
            }
            else
            {
                otherBodyParts(traitOrigin, bodyPartCounter, Parents, bodypart);
            }
            bodyPartCounter++;
            //FilteredAnimalList.Length
            // int randomPicker = Random.Range(0, ParentTraits.Count);
        }
        bodyPartCounter = 0;
        GameObject child;
        child = SpriteManager.Instance.GenerateThingy(BodypartsOut[0], BodypartsOut[1], BodypartsOut[2], BodypartsOut[3], BodypartsOut[4], BodypartsOut[5], BodypartsOut[6], BodypartsOut[7], BodypartsOut[8], BodypartsOut[9], BodypartsOut[10], BodypartsOut[11], Parent1.transform.position, mainChildAnimalType);
        StatsManager.Instance.CalculateStats(Parent1,Parent2,child);
        ThingyData _data = new ThingyData(child.GetComponent<ThingyManager>().stats, BodypartsOut.ToList(), mainChildAnimalType);
        child.GetComponent<ThingyManager>().data = _data;
        CurrentThingies.Instance.AddThingy(_data);
        GlobalGameManager.Instance.SaveData();
    }   

    
    void bodyPart1(int traitOrigin, int bodyPartCounter, GameObject[] Parents, string bodypart)
    {

        string animalType;
        GameObject ChosenParent;
        if(traitOrigin<3)
        {
            ChosenParent = Parents[traitOrigin-1];
            if (bodypart == "arm1")
            {
                armparent = ChosenParent;
            }
            else if (bodypart == "leg1")
            {
                legparent = ChosenParent;
            }
            else if (bodypart == "eye1")
            {
                eyeparent = ChosenParent;
            }
            else if (bodypart == "ear1")
            {
                earparent = ChosenParent;
            }
        }
        if (traitOrigin == 1)
        {
            animalType = Parent1.GetComponent<TraitSelector>().mainAnimalType;
            BodypartsOut[bodyPartCounter] = Parent1.GetComponent<BodyParts>().GetType().GetField(BodypartsIn[bodyPartCounter]).GetValue(Parent1.GetComponent<BodyParts>()).ToString();
            //Bodyparts[bodyPartCounter]
            //get Bodypart with index bodyPartCounter of Parent1
        }
        else if (traitOrigin == 2)
        {
            animalType = Parent2.GetComponent<TraitSelector>().mainAnimalType;
            BodypartsOut[bodyPartCounter] = Parent2.GetComponent<BodyParts>().GetType().GetField(BodypartsIn[bodyPartCounter]).GetValue(Parent2.GetComponent<BodyParts>()).ToString();
            //get Bodypart with index bodyPartCounter of Parent2
        }
        else if (traitOrigin == 3)
        {
            int index = Random.Range(0, 2);
            ChosenParent = Parents[index];
            
            animalType = ChosenParent.GetComponent<TraitSelector>().mainAnimalType;

           
            if (bodypart == "arm1")
            {
                armparent = ChosenParent;
                BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomArm(animalType);
            }
            else if (bodypart == "leg1")
            {
                legparent = ChosenParent;
                BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomLeg(animalType);
            }
            else if (bodypart == "eye1")
            {
                eyeparent = ChosenParent;
                BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomEye(animalType);
            }
            else if (bodypart == "ear1")
            {
                earparent = ChosenParent;
                BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomEar(animalType);
            }
            //augen: andere Augen, aber anderes Auge von gleichem Tier
        }
    }



    void bodyPart2(int traitOrigin, int bodyPartCounter, string bodypart)
    {
        string animalType;
        GameObject ChosenParent = null;
        if (bodypart == "arm2")
        {

            ChosenParent = armparent;
        }
        else if (bodypart == "leg2")
        {
            
            ChosenParent = legparent;
            SLegparent2 = ChosenParent.GetComponent<TraitSelector>().mainAnimalType;
            Debug.Log(SLegparent2);
        }
        else if (bodypart == "eye2")
        {
            ChosenParent = eyeparent;
        }
        else if (bodypart == "ear2")
        {
            ChosenParent = earparent;
        }

        if (traitOrigin < 3)
        {
            animalType = ChosenParent.GetComponent<TraitSelector>().mainAnimalType;
            BodypartsOut[bodyPartCounter] = ChosenParent.GetComponent<BodyParts>().GetType().GetField(BodypartsIn[bodyPartCounter]).GetValue(ChosenParent.GetComponent<BodyParts>()).ToString();
            //Bodyparts[bodyPartCounter]
            //get Bodypart with index bodyPartCounter of Parent1
        }
        else if (traitOrigin == 3)
        {

            animalType = ChosenParent.GetComponent<TraitSelector>().mainAnimalType;

            if (bodypart == "arm2")
            {
                BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomArm(animalType);
            }
            else if (bodypart == "leg2")
            {
                SLegparent2 = ChosenParent.GetComponent<TraitSelector>().mainAnimalType;
                BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomLeg(animalType);
            }
            else if (bodypart == "eye2")
            {
                BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomEye(animalType);
            }
            else if (bodypart == "ear2")
            {
                BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomEar(animalType);
            }
            //augen: andere Augen, aber anderes Auge von gleichem Tier


        }
    }
    void otherBodyParts(int traitOrigin, int bodyPartCounter, GameObject[] Parents, string bodypart)
    {
            string animalType;
        GameObject ChosenParent;
        if (traitOrigin == 1)
        {
            if (bodypart == "body")
            {
                mainChildAnimalType = Parent1.GetComponent<TraitSelector>().mainAnimalType;
            }
            animalType = Parent1.GetComponent<TraitSelector>().mainAnimalType;
            BodypartsOut[bodyPartCounter] = Parent1.GetComponent<BodyParts>().GetType().GetField(BodypartsIn[bodyPartCounter]).GetValue(Parent1.GetComponent<BodyParts>()).ToString();
            //Bodyparts[bodyPartCounter]
            //get Bodypart with index bodyPartCounter of Parent1
        }
        else if (traitOrigin == 2)
        {
            if (bodypart == "body")
            {
                mainChildAnimalType = Parent2.GetComponent<TraitSelector>().mainAnimalType;
            }
            animalType = Parent2.GetComponent<TraitSelector>().mainAnimalType;
            BodypartsOut[bodyPartCounter] = Parent2.GetComponent<BodyParts>().GetType().GetField(BodypartsIn[bodyPartCounter]).GetValue(Parent2.GetComponent<BodyParts>()).ToString();
            //get Bodypart with index bodyPartCounter of Parent2
        }
        else if (traitOrigin == 3)
        {
            int index = Random.Range(0, 2);
            ChosenParent = Parents[index];





            animalType = ChosenParent.GetComponent<TraitSelector>().mainAnimalType;

            if (bodypart == "body")
            {
                mainChildAnimalType = ChosenParent.GetComponent<TraitSelector>().mainAnimalType;
                BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomBody(animalType);
            }
            else if (bodypart == "head")
            {
                BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomHead(animalType);
            }
            else if (bodypart == "tail")
            {
                BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomTail(animalType);
            }
            else if (bodypart == "mouth")
            {
                BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomMouth(animalType);
            }
            //augen: andere Augen, aber anderes Auge von gleichem Tier


            //get Random bodypart with bodypartindex bodyPartCounter and filtered animalType

        }
    }

}
