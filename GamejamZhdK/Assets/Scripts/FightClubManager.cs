using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightClubManager : MonoBehaviour
{

    public GameObject PlayerThingy;
    [SerializeField] private GameObject RetreatPanel;
    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform PlayerSpawn;

    // Start is called before the first frame update
    void Start()
    {
        PlayerThingy = GlobalGameManager.Instance.Player;
        canvas.worldCamera = GlobalGameManager.Instance.CameraFightClub;
        GlobalGameManager.Instance.MovePlayer(PlayerSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonGoBack()
    {
        // switch camera to main screen
        Debug.Log("GoBack");
    }

    public void ButtonRetreat()
    {
        // Show are you sure panel
        RetreatPanel.SetActive(true);
    }

    public void ButtonCancelRetreat()
    {
        // hide are you sure panel
        RetreatPanel.SetActive(false);
    }

    public void Retreat()
    {
        // switch camera to main screen
        // yeet FightClub
        Debug.Log("Retreat");
    }
}
