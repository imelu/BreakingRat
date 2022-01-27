using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class showStats : MonoBehaviour
{


    public Transform headLoc;
    GameObject Head = null;

    float atk;
    float def;
    float spd;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
            if (hit.collider != null && hit.collider.transform.parent.tag == "thingy")
            {
                if (Head != null)
                {
                    Destroy(Head);
                }
                GlobalGameManager.Instance.SelectedThingy = hit.collider.transform.parent.gameObject;
                Head = Instantiate(hit.collider.transform.GetChild(4).gameObject, headLoc.position, Quaternion.identity);

                ThingyManager TManager = hit.collider.transform.parent.GetComponent<ThingyManager>();
                atk = TManager.stats.ATK;
                def = TManager.stats.DEF;
                spd = TManager.stats.SPD;
                transform.GetChild(1).GetComponent<TMP_Text>().text = atk.ToString();
                transform.GetChild(2).GetComponent<TMP_Text>().text = def.ToString();
                transform.GetChild(3).GetComponent<TMP_Text>().text = spd.ToString();

            }
            else
            {
                if (Head!= null)
                {
                    Destroy(Head);
                }
            }
        }

        
    }
  
  
}
