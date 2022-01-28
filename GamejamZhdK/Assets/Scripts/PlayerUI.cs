using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private List<Sprite> icons = new List<Sprite>();
    [SerializeField] private List<Image> iconSlots = new List<Image>();
    [SerializeField] private Image hpbar;
    [SerializeField] private TMP_Text[] stats = new TMP_Text[5];
    private List<string> Infotexts = new List<string>();
    [SerializeField] GameObject Infobox;
    [SerializeField] TMP_Text Infotext;
    private Stats Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GlobalGameManager.Instance.Player.GetComponent<ThingyManager>().stats;
        Infotexts.Add("Has 30% reduced ATK");
        Infotexts.Add("Has 30% reduced DEF");
        Infotexts.Add("Has 30% reduced SPD");
        Infotexts.Add("Heals for " + (int)(100*Player.lifestealValue) + "% of damage dealt");
        Infotexts.Add("Deals " +(int)(Player.poisonValue*100)+"% of ATK per tick when applied to enemies");
        Infotexts.Add("Throws " + (int)(Player.reflectValue*100) + "% of blocked damage back to the attacker");
        Infotexts.Add("Gains 25% more exp from battles");
        Infotexts.Add("Increases chance for inherited, mutated traits and stats by 50%");
        Infotexts.Add("Deals " + (int)Player.ATK + " damage on every Attack");
        Infotexts.Add("Blocks " + (int)Player.DEF + " damage when attacked");
        Infotexts.Add("Determines who strikes first in battle");
    }

    // Update is called once per frame
    void Update()
    {
        hpbar.fillAmount = Player.HP / Player.HPMAX;
        stats[0].text = StatDisplay((int)Player.ATK);
        stats[1].text = StatDisplay((int)Player.DEF);
        stats[2].text = StatDisplay((int)Player.SPD);
        stats[3].text = (StatDisplay((int)Player.LVL)) + " / " + (StatDisplay((int)Player.MAX));
        if(Player.LVL == Player.MAX)
        {
            stats[4].text = "MAX";
        }
        else
        {
            stats[4].text = (StatDisplay((int)Player.EXPCurrent)) + " / " + ((StatDisplay((int)Player.EXPReq)));
        }
        stats[5].text = (StatDisplay((int)Player.HP)) + " / " + (StatDisplay((int)Player.HPMAX));

        CheckForTraits();
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
        foreach(Sprite icon in iconsUsed)
        {
            iconSlots[i].sprite = icon;
            iconSlots[i].gameObject.SetActive(true);
            i++;
        }
    }

    public void ShowInfoText(int _index)
    {
        Infotext.text = Infotexts[_index];
        Infobox.SetActive(true);
        Debug.Log("Entered");
    }

    public void ShowInfoTextIcon(Image _iconslot)
    {
        Infotext.text = Infotexts[icons.IndexOf(iconSlots[iconSlots.IndexOf(_iconslot)].sprite)];
        Infobox.SetActive(true);
    }

    public void HideInfoText()
    {
        Infobox.SetActive(false);
        Debug.Log("Exit");
    }

    private string StatDisplay(int _stat)
    {
        if(_stat > 1000000)
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
