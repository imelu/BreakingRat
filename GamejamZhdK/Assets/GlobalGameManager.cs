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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
