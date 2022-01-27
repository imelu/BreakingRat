using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Sprite[] icons = new Sprite[8];
    [SerializeField] private Image[] iconSlots = new Image[3];
    [SerializeField] private Image hpbar;
    [SerializeField] private TMP_Text[] stats = new TMP_Text[4];
    private Stats Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GlobalGameManager.Instance.Player.GetComponent<ThingyManager>().stats;
    }

    // Update is called once per frame
    void Update()
    {
        hpbar.fillAmount = Player.HP / Player.HPMAX;
        stats[0].text = ((int)Player.ATK).ToString();
        stats[1].text = ((int)Player.DEF).ToString();
        stats[2].text = ((int)Player.SPD).ToString();
        stats[3].text = ((int)Player.LVL).ToString();
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
}
