using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerBulletController : MonoBehaviour
{
    public static PlayerBulletController Instance;

    public GameObject StartButton;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public int bulletCount;
    public int maxBullets = 20;
    public int bulletStandard = 20;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "MainMenu")
        {
            StartButton = GameObject.FindGameObjectWithTag("StartButton");
            Destroy(StartButton);
        }
    }

    public void ResetBullets()
    {
        bulletCount = 0;
        maxBullets = 20;
        bulletStandard = 20;
    }
}
