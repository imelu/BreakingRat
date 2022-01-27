using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{

    private CombatManager combatManager;
    [SerializeField] private Image[] images;
    private List<Stats> stats = new List<Stats>();

    // Start is called before the first frame update
    void Start()
    {
        combatManager = GetComponent<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach(Stats stat in stats)
        {
            images[stats.IndexOf(stat)].fillAmount = stat.HP / stat.HPMAX;
        }
    }

    public void FetchHP()
    {
        stats.Clear();
        foreach(Image image in images)
        {
            image.gameObject.transform.parent.gameObject.SetActive(false);
        }
        foreach(Stats stat in combatManager.Enemies)
        {
            stats.Add(stat);
        }
        for(int i = 0; i < stats.Count; i++)
        {
            images[i].gameObject.transform.parent.gameObject.SetActive(true);
        }
    }
}
