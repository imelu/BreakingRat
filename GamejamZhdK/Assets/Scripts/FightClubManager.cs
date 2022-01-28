using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BigNums;

public class FightClubManager : MonoBehaviour
{

    public GameObject PlayerThingy;
    private CombatManager CManager;
    [SerializeField] private GameObject RetreatPanel;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform PlayerSpawn;
    public ScienceNum expGained;

    // Start is called before the first frame update
    void Start()
    {
        PlayerThingy = GlobalGameManager.Instance.Player;
        canvas.worldCamera = GlobalGameManager.Instance.CameraFightClub;
        GlobalGameManager.Instance.MovePlayer(PlayerSpawn);
        CManager = GetComponent<CombatManager>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerThingy.transform.localScale = Vector3.one;
    }

    public void ButtonGoBack()
    {
        // switch camera to main screen
        GlobalGameManager.Instance.SetMainWindowCamera();
        //Debug.Log("GoBack");
        AudioManager.instance.Play("Click");
    }

    public void ButtonRetreat()
    {
        // Show are you sure panel
        RetreatPanel.SetActive(true);
        AudioManager.instance.Play("Click");
    }

    public void ButtonCancelRetreat()
    {
        // hide are you sure panel
        RetreatPanel.SetActive(false);
        AudioManager.instance.Play("Click");
    }

    public void Retreat()
    {
        // switch camera to main screen
        // yeet FightClub
        CManager.YeetEnemies();
        GlobalGameManager.Instance.EndFightCub();
        //Debug.Log("Retreat");
        AudioManager.instance.Play("Click");
    }
}
