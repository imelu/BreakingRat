using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    [SerializeField] private List<int> RewardStages = new List<int>();
    private Vector3 rewardSpawnPoint = new Vector3(-15,-3,0);

    public GameObject Player;
    public GameObject SelectedThingy;
    public GameObject FightClubPrefab;
    public Camera CameraFightClub;
    public Camera CameraMainWindow;

    private GenericThingySpawner genThiSpawner;

    public bool continueGame = false;

    public bool frogSpawned = false;
    public bool fatfrogSpawned = false;
    public bool weaselSpawned = false;

    public int maxStage = 0;

    public GameObject FightClub;
    private Transform OldPlayerPos;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.Play("MenuMusic");
        genThiSpawner = GetComponent<GenericThingySpawner>();
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartFightClub();
        }*/
        /*
        if (Input.GetKeyDown(KeyCode.S))
        {
            SaveSystem.SaveData(CurrentThingies.Instance.thingies);
        }

        if (Input.GetKeyDown(KeyCode.L))
        {
            CurrentThingies.Instance.thingies = SaveSystem.LoadData();
            ReloadSaveState(CurrentThingies.Instance.thingies);
        }*/
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            genThiSpawner.GenerateRat(new Vector3(Random.Range(-8f, 1f), Random.Range(-2.9f, 0), 0));
        }*/

        if(CameraMainWindow == null || CameraFightClub == null)
        {
            CameraMainWindow = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
            CameraFightClub = GameObject.FindGameObjectWithTag("FightCamera").GetComponent<Camera>();
            if(CameraFightClub != null)
            {
                CameraFightClub.gameObject.SetActive(false);
            }   
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
        SpawnRewardAnimal();
        Player.transform.position = rewardSpawnPoint;
        CanvasSingleton.Instance.GetComponentInChildren<showStats>().EndFC();
    }

    public void SetFightClubCamera()
    {
        CameraMainWindow.gameObject.SetActive(false);
        CameraFightClub.gameObject.SetActive(true);
        AudioManager.instance.Stop("BreedMusic");
        AudioManager.instance.Play("FightMusic");
    }

    public void SetMainWindowCamera()
    {
        CameraMainWindow.gameObject.SetActive(true);
        CameraFightClub.gameObject.SetActive(false);
        AudioManager.instance.Play("BreedMusic");
        AudioManager.instance.Stop("FightMusic");
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
            _thingySpawned.GetComponent<ThingyManager>().InitializeThingyData();
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

    private void SpawnRewardAnimal()
    {
        if (maxStage > RewardStages[0] && !frogSpawned)
        {
            genThiSpawner.GenerateFrog(rewardSpawnPoint);
            frogSpawned = true;
        }
        else if (maxStage > RewardStages[1] && !weaselSpawned)
        {
            genThiSpawner.GenerateWeasel(rewardSpawnPoint);
            weaselSpawned = true;
        }
        else if (maxStage > RewardStages[2] && !fatfrogSpawned)
        {
            genThiSpawner.GenerateFatFrog(rewardSpawnPoint);
            fatfrogSpawned = true;
        }
    }

    public void QuitGame()
    {
        SaveData();
        Application.Quit();
    }
}
