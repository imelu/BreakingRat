using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private Image hpbar;
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
    }
}
