using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalMovement : MonoBehaviour
{
    Vector2 destinationP;
    bool moveable = true;
    bool checkable = true;
    void Start()
    {
        float randomx = Random.Range(0f, 10.0f);
        float randomy = Random.Range(0f, 2.5f);
        destinationP = new Vector2(randomx, randomy);
    }

    
    void Update()
    {
        if (new Vector2(transform.position.x, transform.position.y) == destinationP && checkable == true)
        {
            checkable = false;
            StartCoroutine(nextMovement());
            moveable = false;
        }
        else
        {
            
        }
        if (moveable == true)
        {
            Move();
        }


    }

    void Move()
    {
        float step = 3 * Time.deltaTime;
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x,transform.position.y),destinationP,step);
        checkable = true;
    }
    IEnumerator nextMovement()
    {
        float randomx = Random.Range(0f, 10.0f);
        float randomy = Random.Range(0f, 2.5f);
        yield return new WaitForSeconds(Random.Range(1f,3f));
        moveable = true;
        destinationP = new Vector2(randomx, randomy);
    }

}
