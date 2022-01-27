using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class showStats : MonoBehaviour
{


    public Transform headLoc;
    public Transform headLoc2;
    GameObject Head = null;
    GameObject Head2 = null;
    GameObject Head3 = null;
    GameObject Selected = null;
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
                    Clear();
                }
                Selected = hit.collider.transform.parent.gameObject;
                GlobalGameManager.Instance.SelectedThingy = hit.collider.transform.parent.gameObject;
                Head = Instantiate(hit.collider.transform.GetChild(4).gameObject, headLoc.position, Quaternion.identity);
                Head2 = Instantiate(hit.collider.transform.GetChild(4).gameObject, headLoc2.position, Quaternion.identity);
                Head3 = Instantiate(hit.collider.transform.GetChild(4).gameObject, headLoc3.position, Quaternion.identity);

                TManager = hit.collider.transform.parent.GetComponent<ThingyManager>();
                atk = TManager.stats.ATK;
                def = TManager.stats.DEF;
                spd = TManager.stats.SPD;
                exp = TManager.stats.EXPCurrent;
                lvl = TManager.stats.LVL;

                hpbar.fillAmount = 1/ TManager.stats.HPMAX * TManager.stats.HP;
                stats[0].text = ((int)atk).ToString();
                stats[1].text = ((int)def).ToString();
                stats[2].text = ((int)spd).ToString();
                stats[3].text = ((int)exp).ToString();
                stats[4].text = ((int)lvl).ToString();
            }
            else if(hit.collider != null && hit.collider.transform.tag == "Kill")
            {
                
                Kill();
                Clear();
            }
            else if(hit.collider != null && hit.collider.transform.tag == "Fight")
            {
                if(Selected!=null)
                {
                    GlobalGameManager.Instance.Player = Selected;
                    GlobalGameManager.Instance.StartFightClub();
                    Clear();

                }
                
            }
            else
            {
                if (Head!= null)
                {
                    Clear();
                }
            }
        }
        if(TManager!=null&&Head!=null)
        {
            hpbar.fillAmount = 1 / TManager.stats.HPMAX * TManager.stats.HP;
        }
        

    }
    void Clear()
    {
        Destroy(Head);
        Destroy(Head2);
        Selected = null;
        stats[0].text = "";
        stats[1].text = "";
        stats[2].text = "";
        stats[3].text = "";
        stats[4].text = "";

    }
    public void Kill()
    {
        
        if (Selected != null)
        {
            CurrentThingies.Instance.RemoveThingy(Selected.GetComponent<ThingyManager>().data);
            Destroy(Selected);
            GlobalGameManager.Instance.SaveData();
        }
        
    }


}
