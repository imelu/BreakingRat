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


    public GameObject FightClub;
    private Transform OldPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("MenuMusic");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartFightClub();
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveSystem.SaveData(CurrentThingies.Instance.thingies);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            CurrentThingies.Instance.thingies = SaveSystem.LoadData();
            ReloadSaveState(CurrentThingies.Instance.thingies);
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
        SaveData();
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

    private void ReloadSaveState(List<ThingyData> _data)
    {
        foreach(ThingyData _thingy in _data)
        {
            List<string> BodypartsOut = _thingy.Bodyparts;
            GameObject _thingySpawned = SpriteManager.Instance.GenerateThingy(BodypartsOut[0], BodypartsOut[1], BodypartsOut[2], BodypartsOut[3], BodypartsOut[4], BodypartsOut[5], BodypartsOut[6], BodypartsOut[7], BodypartsOut[8], BodypartsOut[9], BodypartsOut[10], BodypartsOut[11], new Vector3(Random.Range(-8f, 1f), Random.Range(-2.9f, 0), 0), _thingy.mainAnimalType);
            _thingySpawned.GetComponent<ThingyManager>().data = _thingy;
        }
    }

    public void SaveData()
    {
        SaveSystem.SaveData(CurrentThingies.Instance.thingies);
    }

    public void LoadData()
    {
        CurrentThingies.Instance.thingies = SaveSystem.LoadData();
        ReloadSaveState(CurrentThingies.Instance.thingies);
    }
}
