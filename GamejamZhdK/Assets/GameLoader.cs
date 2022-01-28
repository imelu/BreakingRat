using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoader : MonoBehaviour
{

    private GenericThingySpawner genThiSpawner;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        genThiSpawner = GlobalGameManager.Instance.GetComponent<GenericThingySpawner>();
        if (GlobalGameManager.Instance.continueGame)
        {
            GlobalGameManager.Instance.LoadData();
        }
        else
        {
            /*
            for(int i = 0; i<10; i++)
            {
                genThiSpawner.GenerateFatFrog(new Vector3(Random.Range(-8f, 1f), Random.Range(-2.9f, 0), 0));
                genThiSpawner.GenerateFatFrog(new Vector3(Random.Range(-8f, 1f), Random.Range(-2.9f, 0), 0));
            }*/
            
            genThiSpawner.GenerateRat(new Vector3(Random.Range(-8f, 1f), Random.Range(-2.9f, 0), 0));
            genThiSpawner.GenerateRat(new Vector3(Random.Range(-8f, 1f), Random.Range(-2.9f, 0), 0));
            /*
            genThiSpawner.GenerateFrog(new Vector3(Random.Range(-8f, 1f), Random.Range(-2.9f, 0), 0));
            genThiSpawner.GenerateFrog(new Vector3(Random.Range(-8f, 1f), Random.Range(-2.9f, 0), 0));

            genThiSpawner.GenerateWeasel(new Vector3(Random.Range(-8f, 1f), Random.Range(-2.9f, 0), 0));
            genThiSpawner.GenerateWeasel(new Vector3(Random.Range(-8f, 1f), Random.Range(-2.9f, 0), 0));

            genThiSpawner.GenerateFatFrog(new Vector3(Random.Range(-8f, 1f), Random.Range(-2.9f, 0), 0));
            genThiSpawner.GenerateFatFrog(new Vector3(Random.Range(-8f, 1f), Random.Range(-2.9f, 0), 0));*/
        }

        AudioManager.instance.Stop("MenuMusic");
        AudioManager.instance.Play("BreedMusic");

        Destroy(gameObject);
    }
}
