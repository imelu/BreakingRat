using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject ContinueButton;
    [SerializeField] private GameObject NewGameButton;
    [SerializeField] private GameObject StartGameButton;


    // Start is called before the first frame update
    void Start()
    {
        string path = Application.persistentDataPath + "/data2.rat";
        if(ContinueButton != null)
        {
            if (File.Exists(path))
            {
                Debug.Log(path);
                ContinueButton.SetActive(true);
                NewGameButton.SetActive(true);
                StartGameButton.SetActive(false);
            }
            else
            {
                ContinueButton.SetActive(false);
                NewGameButton.SetActive(false);
                StartGameButton.SetActive(true);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        GlobalGameManager.Instance.continueGame = false;
    }

    public void Continue()
    {
        SceneManager.LoadScene(1);
        GlobalGameManager.Instance.continueGame = true;
        // Load Game
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Credits()
    {
        SceneManager.LoadScene(2);
    }

    public void NewGame()
    {
        SceneManager.LoadScene(1);
        GlobalGameManager.Instance.continueGame = false;
    }

    public void TitleScreen()
    {
        SceneManager.LoadScene(0);
    }
}
