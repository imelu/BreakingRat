using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{

    #region Singleton
    public static GlobalGameManager Instance;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public GameObject Player;
    public GameObject SelectedThingy;
    public GameObject FightClubPrefab;
    public Camera CameraFightClub;
    public Camera CameraMainWindow;


    private GameObject FightClub;
    private Transform OldPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartFightClub();
        }
    }

    public void StartFightClub()
    {
        // set player?
        OldPlayerPos = Player.transform;
        FightClub = Instantiate(FightClubPrefab);
        Player.GetComponent<animalMovement>().moveable = false;
        Player.GetComponent<animalMovement>().enabled = false;
        Player.GetComponent<layerOrderScript>().enabled = false;
        Player.transform.localScale = Vector3.one;
        SetFightClubCamera();
    }

    public void EndFightCub()
    {
        Destroy(FightClub);
        Player.GetComponent<animalMovement>().moveable = true;
        Player.GetComponent<animalMovement>().enabled = true;
        Player.GetComponent<layerOrderScript>().enabled = true;
        MovePlayer(OldPlayerPos);
        SetMainWindowCamera();
    }

    public void SetFightClubCamera()
    {
        CameraMainWindow.gameObject.SetActive(false);
        CameraFightClub.gameObject.SetActive(true);
    }

    public void SetMainWindowCamera()
    {
        CameraMainWindow.gameObject.SetActive(true);
        CameraFightClub.gameObject.SetActive(false);
    }

    public void MovePlayer(Transform _position)
    {
        Player.transform.position = _position.position;
        Player.transform.localScale = Vector3.one;
    }
}
