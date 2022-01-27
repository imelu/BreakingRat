using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class showStats : MonoBehaviour
{
    #region Singleton
    public static showStats Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
    }
    #endregion

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
    void Start()
    {

        
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Camera.main!=null)
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
                    Infotexts.Add("Heals for " + (int)(100 * Player.lifestealValue) + "% of damage dealt");
                    Infotexts.Add("Deals " + (int)(Player.poisonValue * 100) + "% of ATK per tick when applied to enemies");
                    Infotexts.Add("Throws " + (int)(Player.reflectValue * 100) + "% of blocked damage back to the attacker");
                    Infotexts.Add("Gains 25% more exp from battles");
                    Infotexts.Add("Increases chance for inherited, mutated traits and stats by 50%");
                    Infotexts.Add("Deals " + (int)Player.ATK + " damage on every Attack");
                    Infotexts.Add("Blocks " + (int)Player.DEF + " damage when attacked");
                    Infotexts.Add("Determines who strikes first in battle");

                    GlobalGameManager.Instance.SelectedThingy = hit.collider.transform.parent.gameObject;
                    Head = Instantiate(hit.collider.transform.GetChild(4).gameObject, headLoc.position, Quaternion.identity);
                    Head2 = Instantiate(hit.collider.transform.GetChild(4).gameObject, headLoc2.position, Quaternion.identity);


                    TManager = hit.collider.transform.parent.GetComponent<ThingyManager>();
                    atk = TManager.stats.ATK;
                    def = TManager.stats.DEF;
                    spd = TManager.stats.SPD;
                    exp = TManager.stats.EXPCurrent;
                    lvl = TManager.stats.LVL;

                    hpbar.fillAmount = 1 / TManager.stats.HPMAX * TManager.stats.HP;
                    stats[0].text = ((int)atk).ToString();
                    stats[1].text = ((int)def).ToString();
                    stats[2].text = ((int)spd).ToString();
                    stats[3].text = ((int)exp).ToString();
                    stats[4].text = ((int)lvl).ToString();
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
                        Head3 = Instantiate(Selected.transform.GetChild(0).GetChild(4).gameObject, headLoc3.position, Quaternion.identity);
                        GlobalGameManager.Instance.Player = Selected;
                        GlobalGameManager.Instance.StartFightClub();
                        FCButton.SetActive(false);
                        FCButtonEmpty.SetActive(true);
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
                else
                {
                    if (Head != null)
                    {
                        Clear();
                    }
                }
            }
            if (TManager != null && Head != null)
            {
                hpbar.fillAmount = 1 / TManager.stats.HPMAX * TManager.stats.HP;
            }
            if (Player != null)
            {
                CheckForTraits();
            }
        }
       
        
    }
    public void EndFC()
    {
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


}
