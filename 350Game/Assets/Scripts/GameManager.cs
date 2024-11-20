using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool triedDoorOne;
    public bool triedDoorTwo;
    public bool triedDoorThree;

    [SerializeField]
    private GameObject doorOne;
    [SerializeField] 
    private GameObject doorTwo;
    [SerializeField]
    private GameObject doorThree;

    // Get the player health variable
    [SerializeField]
    private PlayerHealth PlayerHealth;

    // update bools
    [SerializeField]
    private bool hasHealthLevel;

    public float playerHealth = 100;

    // checks the number of rooms visited
    public int roomAmount = 0;
    public string scene;

    // BULLET STUFF
    private int maxBullets;
    [SerializeField] private ProjectileGun gun;
    private int bulletCount;
    [SerializeField] private TextMeshProUGUI bulletCountText;
    [SerializeField] private GameObject playerUI;
    public PlayerBulletController playerBulletController;

    // PAUSE MENU
    public static bool GameIsPaused = false;
    public GameObject PauseMenuUI;

    // HEALTH STUFF
    public PlayerHealthController playerHealthController;

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
        Application.targetFrameRate = 60;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.scene = scene.name;

        if(gun != null)
        {
            playerBulletController.maxBullets = PlayerPrefs.GetInt("MaxBullets", playerBulletController.maxBullets);
            playerBulletController.bulletStandard = PlayerPrefs.GetInt("BulletStandard", playerBulletController.bulletStandard);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        scene = SceneManager.GetActiveScene().name;

        if (PlayerPrefs.HasKey("MaxBullets"))
        {
            playerBulletController.maxBullets = PlayerPrefs.GetInt("MaxBullets");
        }
        if (PlayerPrefs.HasKey("BulletStandard"))
        {
            playerBulletController.bulletStandard = PlayerPrefs.GetInt("BulletStandard");
        }
        if (PlayerPrefs.HasKey("MaxHealth"))
        {
            playerHealthController.maxHealth = PlayerPrefs.GetFloat("MaxHealth");
        }
    }

    // Update is called once per frame
    void Update()
    {
        doorOne = GameObject.FindGameObjectWithTag("DoorOne");
        doorTwo = GameObject.FindGameObjectWithTag("DoorTwo");
        doorThree = GameObject.FindGameObjectWithTag("DoorThree");

       PauseMenuManager pauseMenuManager = FindObjectOfType<PauseMenuManager>(true);
        if(pauseMenuManager != null)
        {
            PauseMenuUI = pauseMenuManager.gameObject;
        }

        // BULLET STUFF
        playerUI = GameObject.Find("PlayerUI");
        if(playerUI != null)
        {
            bulletCountText = playerUI.transform.Find("BulletCountText")?.GetComponent<TextMeshProUGUI>();
        }
        gun = GameObject.Find("GunPlaceholder")?.GetComponentInParent<ProjectileGun>();

        playerBulletController = FindObjectOfType<PlayerBulletController>(true);
        playerHealthController = FindObjectOfType<PlayerHealthController>(true);

        if (triedDoorOne == true)
        {
            Destroy(doorOne);
        }
        if(triedDoorTwo == true)
        {
            Destroy(doorTwo);
        }
        if(triedDoorThree == true)
        {
            Destroy(doorThree);
        }

        if(SceneManager.GetActiveScene().name == "EndGame")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Destroy(gameObject);
        }

        if(hasHealthLevel == true)
        {
            playerHealth += 20;
        }

        // check for current scene;
        scene = SceneManager.GetActiveScene().name;

        //DisplayPickLevelUp();

        if(SceneManager.GetActiveScene().name == "LevelUpSelect")
        {
            Cursor.lockState= CursorLockMode.None;
            Cursor.visible = true;
        }


        // BULLET STUFF
        bulletCount = playerBulletController.bulletCount;
        UpdateBulletCount();

        // PAUSE MENU
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    void Resume()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        PauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    private void Pause()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        PauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    /*public void DisplayPickLevelUp()
    {
        if (roomAmount % 2 == 0 && roomAmount != 0 && scene == "Room0")
        {
            SceneManager.LoadScene("LevelUpSelect");
        }
    }*/

    private void UpdateBulletCount()
    {
        if (bulletCountText != null)
        {
            bulletCountText.text = $"Bullets Left: {playerBulletController.maxBullets}";
        }
    }

    public void NewBulletCount1()
    {
        if (roomAmount >= 1 && roomAmount != 2 && roomAmount != 3)
        {
            playerBulletController.maxBullets = playerBulletController.maxBullets + 10;
            playerBulletController.bulletStandard = playerBulletController.bulletStandard + 10;
        }
        PlayerPrefs.SetInt("MaxBullets", playerBulletController.maxBullets);
        PlayerPrefs.SetInt("BulletStandard", playerBulletController.bulletStandard);
    }

    public void NewBulletCount2()
    {
        if (roomAmount >= 2 && roomAmount != 3)
        {
            playerBulletController.maxBullets = playerBulletController.maxBullets + 10;
            playerBulletController.bulletStandard = playerBulletController.bulletStandard + 10;
        }
        PlayerPrefs.SetInt("MaxBullets", playerBulletController.maxBullets);
        PlayerPrefs.SetInt("BulletStandard", playerBulletController.bulletStandard);
    }

    public void NewBulletCount3()
    {
        if (roomAmount >= 3)
        {
            playerBulletController.maxBullets = playerBulletController.maxBullets + 10;
            playerBulletController.bulletStandard = playerBulletController.bulletStandard + 10;
        }
        PlayerPrefs.SetInt("MaxBullets", playerBulletController.maxBullets);
        PlayerPrefs.SetInt("BulletStandard", playerBulletController.bulletStandard);
    }

    public void NewHealth1()
    {
        if (roomAmount >= 1 && roomAmount != 2 && roomAmount != 3)
        {
            playerHealthController.maxHealth = playerHealthController.maxHealth + 10;
        }
        PlayerPrefs.SetFloat("MaxHealth", playerHealthController.maxHealth);
    }

    public void NewHealth2()
    {
        if (roomAmount >= 2 && roomAmount != 3)
        {
            playerHealthController.maxHealth = playerHealthController.maxHealth + 10;
        }
        PlayerPrefs.SetFloat("MaxHealth", playerHealthController.maxHealth);
    }

    public void NewHealth3()
    {
        if (roomAmount >= 3)
        {
            playerHealthController.maxHealth = playerHealthController.maxHealth + 10;
        }
        PlayerPrefs.SetFloat("MaxHealth", playerHealthController.maxHealth);
    }
}
