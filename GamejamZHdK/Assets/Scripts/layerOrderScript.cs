using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class layerOrderScript : MonoBehaviour
{
    int mainOrder;
    GameObject bodyObj;
    GameObject tailObj;
    GameObject leg1Obj;
    GameObject leg2Obj;
    GameObject arm1Obj;
    GameObject arm2Obj;

    GameObject headObj;
    GameObject ear1Obj;
    GameObject ear2Obj;
    GameObject eye1Obj;
    GameObject eye2Obj;
    GameObject mouthObj;
    animalMovement moveScript;


    public bool RLeft;

    void Start()
    {
       moveScript = GetComponent<animalMovement>();

        bodyObj = transform.GetChild(0).gameObject;
        tailObj = transform.GetChild(0).GetChild(1).gameObject;
        leg1Obj = transform.GetChild(0).GetChild(2).GetChild(0).gameObject;
        leg2Obj = transform.GetChild(0).GetChild(2).GetChild(1).gameObject;
        arm1Obj = transform.GetChild(0).GetChild(3).GetChild(0).gameObject;
        arm2Obj = transform.GetChild(0).GetChild(3).GetChild(1).gameObject;

        headObj = transform.GetChild(0).GetChild(4).gameObject;
        ear1Obj = transform.GetChild(0).GetChild(4).GetChild(0).GetChild(0).gameObject;
        ear2Obj = transform.GetChild(0).GetChild(4).GetChild(0).GetChild(1).gameObject;
        eye1Obj = transform.GetChild(0).GetChild(4).GetChild(1).GetChild(0).gameObject;
        eye2Obj = transform.GetChild(0).GetChild(4).GetChild(1).GetChild(1).gameObject;
        mouthObj = transform.GetChild(0).GetChild(4).GetChild(2).gameObject;

    }

    // Update is called once per frame
    void Update()
    {
        mainOrder = (int)(-transform.position.y * 100);
        bodyObj.GetComponent<SpriteRenderer>().sortingOrder = mainOrder - 1;
        tailObj.GetComponent<SpriteRenderer>().sortingOrder = mainOrder;
        leg1Obj.GetComponent<SpriteRenderer>().sortingOrder = mainOrder + 2;
        leg2Obj.GetComponent<SpriteRenderer>().sortingOrder = mainOrder - 2;
        arm1Obj.GetComponent<SpriteRenderer>().sortingOrder = mainOrder;
        arm2Obj.GetComponent<SpriteRenderer>().sortingOrder = mainOrder - 2;

        headObj.GetComponent<SpriteRenderer>().sortingOrder = mainOrder;
        ear1Obj.GetComponent<SpriteRenderer>().sortingOrder = mainOrder - 1;
        ear2Obj.GetComponent<SpriteRenderer>().sortingOrder = mainOrder - 1;
        eye1Obj.GetComponent<SpriteRenderer>().sortingOrder = mainOrder + 1;
        eye2Obj.GetComponent<SpriteRenderer>().sortingOrder = mainOrder + 1;
        mouthObj.GetComponent<SpriteRenderer>().sortingOrder = mainOrder + 1;

        if (transform.position.x - moveScript.destinationP.x > 0)
        {
            transform.localScale = new Vector3(-1,1,1);
        }
        else if (transform.position.x - moveScript.destinationP.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        if (RLeft)
        {
            if (moveScript.moveable == false)
            {
                if(leg1Obj.transform.eulerAngles.z<=0.1 && leg1Obj.transform.eulerAngles.z >= -0.1)
                {
                    RLeft = false;
                }
            }
            leg1Obj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.PingPong(Time.time * 50, 12.0f) - 6);
            arm1Obj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -(Mathf.PingPong(Time.time * 50, 12.0f) - 6));
            leg2Obj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, -(Mathf.PingPong(Time.time * 50, 12.0f) - 6));
            arm2Obj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.PingPong(Time.time * 50, 12.0f) - 6);

            headObj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, Mathf.PingPong(Time.time * 10, 3.0f) - 1.5f);
        }
    }
    public IEnumerator animationMove()
    {
        if (moveScript.moveable==false)
        {
            yield break;
        }
        
        RLeft = true;

        
        //StartCoroutine(animationMove());

        yield return null;
    }
   
}
