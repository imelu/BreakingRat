using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraitSelector : MonoBehaviour
{
    List<string> AllTraitsWithParentMainbody;
    List<string> ParentTraits;
    public List<Sprite> BodySprites;
    public string mainAnimalType;
    string Mainbody;
    
    string[] BodypartsIn = { "head", "body", "eye1", "eye2", "ear1", "ear2", "mouth","arm1", "arm2", "leg1", "leg2", "tail" };
    string[] BodypartsOut = { "head", "body", "eye1", "eye2", "ear1", "ear2", "mouth", "arm1", "arm2", "leg1", "leg2", "tail" };
    //string[] BodypartsRandom = { "Heads", "Bodies", "Eyes", "Eyes", "Ears", "Ears", "Mouths", "Arms", "Arms", "Legs", "Legs", "Tails" };
    //body, head gameobject
    int GetRandomValue()
    {
        /*
        float rand = Random.value;
        if (rand <= .47f)
            return 1;
        if (rand <= .94f)
            return 2;

        return 3;*/
        float rand = Random.value;
        

        return 3;
    }

    public void SetTraits(GameObject Parent1, GameObject Parent2)
    {
        GameObject[] Parents = { Parent1, Parent2 };
        GameObject ChosenParent;
        
        int index = Random.Range(0, 2);
        /*
        ChosenParent = Parents[index];
        mainAnimalType = ChosenParent.GetComponent<TraitSelector>().mainAnimalType;*/

        int bodyPartCounter=0;
        foreach(string bodypart in BodypartsIn)
        {
           
            string animalType;

            //1==parent1
            //2==parent2
            //3==random
            int traitOrigin = GetRandomValue();
            if (traitOrigin == 1)
            {
                animalType = Parent1.GetComponent<TraitSelector>().mainAnimalType;
                print(Parent1.GetComponent<BodyParts>().GetType().GetField(BodypartsIn[bodyPartCounter]).GetValue(Parent1.GetComponent<BodyParts>()).ToString());
                BodypartsOut[bodyPartCounter] = Parent1.GetComponent<BodyParts>().GetType().GetField(BodypartsIn[bodyPartCounter]).GetValue(Parent1.GetComponent<BodyParts>()).ToString();

                //Bodyparts[bodyPartCounter]
                //get Bodypart with index bodyPartCounter of Parent1
            }
            else if(traitOrigin == 2)
            {
                print(Parent1.GetComponent<BodyParts>().GetType().GetField(BodypartsIn[bodyPartCounter]).GetValue(Parent1.GetComponent<BodyParts>()).ToString());
                BodypartsOut[bodyPartCounter] = Parent2.GetComponent<BodyParts>().GetType().GetField(BodypartsIn[bodyPartCounter]).GetValue(Parent1.GetComponent<BodyParts>()).ToString();
                //get Bodypart with index bodyPartCounter of Parent2
            }
            else if (traitOrigin == 3)
            {
                index = Random.Range(0, 2);
                ChosenParent = Parents[index];
                animalType = ChosenParent.GetComponent<TraitSelector>().mainAnimalType;

                print(animalType);
                if (bodypart == "body")
                {
                    BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomBody(animalType);
                }
                else if (bodypart == "head")
                {
                    BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomHead(animalType);
                }
                else if (bodypart == "arm")
                {
                    BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomArm(animalType);
                }
                else if (bodypart == "leg")
                {
                    BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomLeg(animalType);
                }
                else if (bodypart == "tail")
                {
                    BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomTail(animalType);
                }
                else if (bodypart == "ear")
                {
                    BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomEar(animalType);
                }
                else if (bodypart == "mouth")
                {
                    BodypartsOut[bodyPartCounter] = SpriteManager.Instance.getRandomMouth(animalType);
                }



                //get Random bodypart with bodypartindex bodyPartCounter and filtered animalType
               
            }
            bodyPartCounter++;
            //FilteredAnimalList.Length
            // int randomPicker = Random.Range(0, ParentTraits.Count);
        }
        bodyPartCounter = 0;
        SpriteManager.Instance.GenerateThingy(BodypartsOut[0], BodypartsOut[1], BodypartsOut[2], BodypartsOut[3], BodypartsOut[4], BodypartsOut[5], BodypartsOut[6], BodypartsOut[7], BodypartsOut[8], BodypartsOut[9], BodypartsOut[10], BodypartsOut[11]);


    }
    
    void Update()
    {
        
    }
}
