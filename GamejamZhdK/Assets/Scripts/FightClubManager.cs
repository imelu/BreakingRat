using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightClubManager : MonoBehaviour
{

    public GameObject PlayerThingy;


    // Start is called before the first frame update
    void Start()
    {
        PlayerThingy = GlobalGameManager.Instance.Player;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
