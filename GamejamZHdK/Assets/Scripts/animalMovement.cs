using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animalMovement : MonoBehaviour
{
    public Vector2 destinationP;
    bool moveable = false;
    bool checkable = true;
    private bool isDragging;
    int mergeable;
    GameObject MergeObject;
    void Start()
    {
        float randomx = Random.Range(-8.3f, 8.3f);
        float randomy = Random.Range(-2.8f, 1.25f);
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(randomx, randomy, 0), Vector3.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(randomx, randomy, 0), Vector3.forward * 1000);

            while (hit.collider.gameObject.name.Equals("Collider"))
            {
                randomx = Random.Range(-8.3f, 8.3f);
                randomy = Random.Range(-2.8f, 1.25f);

                if (!Physics.Raycast(new Vector3(randomx, randomy, 0), Vector3.forward, out hit, Mathf.Infinity))
                {
                    break;
                }
            }
        }

        destinationP = new Vector2(randomx, randomy);
    }

    public void OnMouseDown()
    {
        isDragging = true;
        moveable = false;
    }
    public void OnMouseUp()
    {
        isDragging = false;
        moveable = false;
        if(mergeable==1)
        {
            TraitSelector createChild = gameObject.GetComponent<TraitSelector>();
            createChild.SetTraits(this.gameObject, MergeObject);
            mergeable = 0;
        }
        else
        {
            mergeable = 0;
            fallAnimal();
        }
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            transform.Translate(mousePosition);
        }
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
    void fallAnimal()
    {
        float step = 10 * Time.deltaTime;
        destinationP = new Vector2(transform.position.x, transform.position.y - 1);
        transform.position = Vector2.MoveTowards(new Vector2(transform.position.x, transform.position.y), destinationP, step);
        checkable = true;
        moveable = true;
    }
    IEnumerator nextMovement()
    {
        float randomx = Random.Range(-8.3f, 8.3f);
        float randomy = Random.Range(-2.8f, 1.25f);
        RaycastHit hit;
        if (Physics.Raycast(new Vector3(randomx, randomy, 0), Vector3.forward, out hit, Mathf.Infinity))
        {
            Debug.DrawRay(new Vector3(randomx, randomy, 0), Vector3.forward * 1000);

            while (hit.collider.gameObject.name.Equals("Collider"))
            {
                randomx = Random.Range(-8.3f, 8.3f);
                randomy = Random.Range(-2.8f, 1.25f);

                if (!Physics.Raycast(new Vector3(randomx, randomy, 0), Vector3.forward, out hit, Mathf.Infinity))
                {
                    break;
                }
            }
        }
        yield return new WaitForSeconds(Random.Range(1f,3f));
        if(!isDragging)
        {
            moveable = true;
            destinationP = new Vector2(randomx, randomy);
        }
    }
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (isDragging)
        {
            if (collider.transform.parent.tag == "thingy"&&mergeable==0)
            {
                Debug.Log("newcol");
                mergeable++;
                MergeObject = collider.transform.parent.gameObject;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (isDragging)
        {
            if (collider.transform.parent.tag == "thingy"&&mergeable>0)
            {
                mergeable--;
            }
        }
    }
}
