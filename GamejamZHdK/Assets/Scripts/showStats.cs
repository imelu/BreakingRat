using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class showStats : MonoBehaviour
{


    public Transform headLoc;
    GameObject Head = null;

    [SerializeField] private Sprite[] icons = new Sprite[8];
    [SerializeField] private Image[] iconSlots = new Image[3];
    [SerializeField] private Image hpbar;
    [SerializeField] private TMP_Text[] stats = new TMP_Text[5];
    float atk;
    float def;
    float spd;
    float exp;
    float lvl;

    ThingyManager TManager;
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

                TManager = hit.collider.transform.parent.GetComponent<ThingyManager>();
                atk = TManager.stats.ATK;
                def = TManager.stats.DEF;
                spd = TManager.stats.SPD;
                lvl = TManager.stats.LVL;

                hpbar.fillAmount = 1/ TManager.stats.HPMAX * TManager.stats.HP;
                stats[0].text = atk.ToString();
                stats[1].text = def.ToString();
                stats[2].text = spd.ToString();
                //stats[3].text = exp.ToString();
                stats[4].text = lvl.ToString();
            }
            else
            {
                if (Head!= null)
                {
                    Destroy(Head);
                }
            }
        }
        if(TManager!=null&&Head!=null)
        {
            hpbar.fillAmount = 1 / TManager.stats.HPMAX * TManager.stats.HP;
        }


    }
  
  
}
