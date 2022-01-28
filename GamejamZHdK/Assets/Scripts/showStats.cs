using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BigNums;

public class showStats : MonoBehaviour
{
    public Transform headLoc;
    public Transform headLoc2;
    public Transform headLoc3;
    GameObject Head = null;
    GameObject Head2 = null;
    GameObject Head3 = null;
    GameObject Selected = null;
    public GameObject FCButton ;
    public GameObject FCButtonEmpty;
    [SerializeField] private List<Sprite> icons = new List<Sprite>();
    [SerializeField] private List<Image> iconSlots = new List<Image>();
    [SerializeField] private Image hpbar;
    [SerializeField] private TMP_Text[] stats = new TMP_Text[5];
  
    float atk;
    float def;
    float spd;
    float exp;
    float lvl;

    ThingyManager TManager;
    private List<string> Infotexts = new List<string>();
    [SerializeField] GameObject Infobox;
    [SerializeField] TMP_Text Infotext;
    private Stats Player;

    ScienceNum one;
    
    void Start()
    {
        one.baseValue = 1;
        one.eFactor = 0;
        
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(hpbar.fillAmount);
        if (Camera.main!=null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                AudioManager.instance.Play("Click");
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity);
                if (hit.collider != null && hit.collider.transform.parent.tag == "thingy")
                {

                    if (Head != null)
                    {
                        Clear();
                    }
                    Selected = hit.collider.transform.parent.gameObject;
                    Player = Selected.GetComponent<ThingyManager>().stats;

                    Infotexts.Add("Has 30% reduced ATK");
                    Infotexts.Add("Has 30% reduced DEF");
                    Infotexts.Add("Has 30% reduced SPD");
                    Infotexts.Add("Heals for " + (int)(100 * Player.lifestealValue.Conversion()) + "% of damage dealt");
                    Infotexts.Add("Deals " + (int)(Player.poisonValue.Conversion() * 100) + "% of ATK per tick when applied to enemies");
                    Infotexts.Add("Throws " + (int)(Player.reflectValue.Conversion() * 100) + "% of blocked damage back to the attacker");
                    Infotexts.Add("Gains 25% more exp from battles");
                    Infotexts.Add("Increases chance for inherited, mutated traits and stats by 50%");
                    Infotexts.Add("Deals " + Player.ATK.ToString() + " damage on every Attack");
                    Infotexts.Add("Blocks " + Player.DEF.ToString() + " damage when attacked");
                    Infotexts.Add("Determines who strikes first in battle");

                    GlobalGameManager.Instance.SelectedThingy = hit.collider.transform.parent.gameObject;
                    Head = Instantiate(hit.collider.transform.GetChild(4).gameObject, headLoc.position, Quaternion.identity);
                    Head2 = Instantiate(hit.collider.transform.GetChild(4).gameObject, headLoc2.position, Quaternion.identity);


                    TManager = hit.collider.transform.parent.GetComponent<ThingyManager>();
                    /*atk = TManager.stats.ATK;
                    def = TManager.stats.DEF;
                    spd = TManager.stats.SPD;
                    exp = TManager.stats.EXPCurrent;
                    lvl = TManager.stats.LVL;*/
                    /*
                    stats[0].text = ((int)atk).ToString();
                    stats[1].text = ((int)def).ToString();
                    stats[2].text = ((int)spd).ToString();
                    stats[3].text = ((int)exp).ToString();
                    stats[4].text = ((int)lvl).ToString();
                    */
                    hpbar.fillAmount = (Player.HP / Player.HPMAX).Conversion();
                    stats[0].text = Player.ATK.ToString();
                    stats[1].text = Player.DEF.ToString();
                    stats[2].text = Player.SPD.ToString();
                    stats[4].text = (Player.LVL.ToString()) + " / " + (Player.MAX.ToString());
                    if (Player.LVL >= Player.MAX)
                    {
                        stats[3].text = "MAX";
                    }
                    else
                    {
                        stats[3].text = (Player.EXPCurrent.ToString()) + " / " + (Player.EXPReq.ToString());
                    }
                    //stats[5].text = (Player.HP.ToString()) + " / " + (Player.HPMAX.ToString());
                }
                else if (hit.collider != null && hit.collider.transform.tag == "Kill")
                {
                    
                    Kill();
                    Clear();
                }
                else if (hit.collider != null && hit.collider.transform.tag == "Fight")
                {
                    if (Selected != null)
                    {
                        if(Head3!=null)
                        {
                            Destroy(Head3);
                        }
                        Head3 = Instantiate(Selected.transform.GetChild(0).GetChild(4).gameObject, headLoc3.position, Quaternion.identity);
                        GlobalGameManager.Instance.Player = Selected;
                        if (Selected.GetComponent<ThingyManager>().stats.HP>= Selected.GetComponent<ThingyManager>().stats.HPMAX)
                        {
                            GlobalGameManager.Instance.StartFightClub();
                           
                            GlobalGameManager.Instance.Player.GetComponent<ThingyManager>().fighting = true;
                            FCButton.SetActive(false);
                            FCButtonEmpty.SetActive(true);
                        }
                        
                        Clear();

                    }

                }
                else if (hit.collider != null && hit.collider.transform.tag == "FightProgress")
                {
                    if (GlobalGameManager.Instance.FightClub != null)
                    {

                        GlobalGameManager.Instance.SetFightClubCamera();
                    }
                } 
                else if (hit.collider != null && hit.collider.transform.tag == "Quit")
                {
                    GlobalGameManager.Instance.QuitGame();
                }
                else
                {
                    if (Head != null)
                    {
                        Clear();
                    }
                }
            }
            if (TManager != null && Head != null&&Selected!=null)
            {
                hpbar.fillAmount = (TManager.stats.HP / TManager.stats.HPMAX).Conversion();
            }
            if (Player != null)
            {
                CheckForTraits();
            }
        }
       
        
    }
    public void EndFC()
    {
        GlobalGameManager.Instance.Player.GetComponent<ThingyManager>().stats.HP = one;
        GlobalGameManager.Instance.Player.GetComponent<ThingyManager>().fighting = false;
        Destroy(Head3);
        FCButton.SetActive(true);
        FCButtonEmpty.SetActive(false);
    }
    void Clear()
    {
        
        Destroy(Head);
        Destroy(Head2);
        Player = null;
        Infobox.SetActive(false);
        iconSlots[0].gameObject.SetActive(false);
        iconSlots[1].gameObject.SetActive(false);
        iconSlots[2].gameObject.SetActive(false);
        Selected = null;
        stats[0].text = "";
        stats[1].text = "";
        stats[2].text = "";
        stats[3].text = "";
        stats[4].text = "";
        hpbar.fillAmount = 0f;
    }
    public void Kill()
    {
        
        if (Selected != null&& CurrentThingies.Instance.thingies.Count > 2)
        {
            CurrentThingies.Instance.RemoveThingy(Selected.GetComponent<ThingyManager>().data);
            Destroy(Selected);
            GlobalGameManager.Instance.SaveData();
        }
        
    }

    private void CheckForTraits()
    {
        List<Sprite> iconsUsed = new List<Sprite>();
        if (Player.weak) iconsUsed.Add(icons[0]);
        if (Player.frail) iconsUsed.Add(icons[1]);
        if (Player.slow) iconsUsed.Add(icons[2]);
        if (Player.lifesteal) iconsUsed.Add(icons[3]);
        if (Player.poison) iconsUsed.Add(icons[4]);
        if (Player.reflect) iconsUsed.Add(icons[5]);
        if (Player.looter) iconsUsed.Add(icons[6]);
        if (Player.demGeenes) iconsUsed.Add(icons[7]);

        int i = 0;
        foreach (Sprite icon in iconsUsed)
        {
            iconSlots[i].sprite = icon;
            iconSlots[i].gameObject.SetActive(true);
            i++;
        }
    }

    public void ShowInfoText(int _index)
    {
        if(Player!=null)
        {
            Infotext.text = Infotexts[_index];
            Infobox.SetActive(true);
        }
        
    }
    public void ShowInfoTextIcon(Image _iconslot)
    {
        if (Player != null)
        {
            Infotext.text = Infotexts[icons.IndexOf(iconSlots[iconSlots.IndexOf(_iconslot)].sprite)];
            Infobox.SetActive(true);
        }
    }

    public void HideInfoText()
    {
        if (Player != null)
        {
            Infobox.SetActive(false);
        }
    }

    private string StatDisplay(int _stat)
    {
        if (_stat > 1000000)
        {
            return (_stat / 1000000).ToString() + "M";
        }
        else if (_stat > 1000)
        {
            return (_stat / 1000).ToString() + "k";
        }
        else
        {
            return _stat.ToString();
        }
    }
}
