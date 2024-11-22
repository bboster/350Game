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
    public bool triedDoorFour;
    public bool triedDoorFive;
    public bool triedDoorSix;

    [SerializeField]
    private GameObject doorOne;
    [SerializeField] 
    private GameObject doorTwo;
    [SerializeField]
    private GameObject doorThree;
    [SerializeField]
    private GameObject doorFour;
    [SerializeField]
    private GameObject doorFive;
    [SerializeField]
    private GameObject doorSix;

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

    [SerializeField] private TextMeshProUGUI skillPointText;

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
        //if (PlayerPrefs.HasKey("MaxHealth"))
        //{
        //    playerHealthController.maxHealth = PlayerPrefs.GetFloat("MaxHealth");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        doorOne = GameObject.FindGameObjectWithTag("DoorOne");
        doorTwo = GameObject.FindGameObjectWithTag("DoorTwo");
        doorThree = GameObject.FindGameObjectWithTag("DoorThree");
        doorFour = GameObject.FindGameObjectWithTag("DoorFour");
        doorFive = GameObject.FindGameObjectWithTag("DoorFive");
        doorSix = GameObject.FindGameObjectWithTag("DoorSix");

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
            skillPointText = playerUI.transform.Find("SkillPointText")?.GetComponent<TextMeshProUGUI>();
        }
        gun = GameObject.Find("rifle_1")?.GetComponentInParent<ProjectileGun>();

        playerBulletController = FindObjectOfType<PlayerBulletController>(true);
        playerHealthController = FindObjectOfType<PlayerHealthController>(true);

        if (SceneManager.GetActiveScene().name == "Room0")
        {
            if (triedDoorOne == true && !triedDoorTwo)
            {
                Destroy(doorOne);
                Vector3 PositionTwo = new Vector3(-98.91f, -31.84001f, 138f);
                doorTwo.transform.position = PositionTwo;
            }
            if (triedDoorTwo == true && !triedDoorThree)
            {
                Destroy(doorOne);
                Destroy(doorTwo);
                Vector3 PositionThree = new Vector3(-206.71f, -31.84001f, 145.05f);
                doorThree.transform.position = PositionThree;
            }
            if (triedDoorThree == true && !triedDoorFour)
            {
                Destroy(doorOne);
                Destroy(doorTwo);
                Destroy(doorThree);
                Vector3 PositionFour = new Vector3(-206.71f, -31.84001f, 161.84f);
                doorFour.transform.position = PositionFour;
            }
            if (triedDoorFour == true && !triedDoorFive)
            {
                Destroy(doorOne);
                Destroy(doorTwo);
                Destroy(doorThree);
                Destroy(doorFour);
                Vector3 PositionFive = new Vector3(-187.11f, -31.84001f, 75.93001f);
                doorFive.transform.position = PositionFive;
            }
            if (triedDoorFive == true && !triedDoorSix)
            {
                Destroy(doorOne);
                Destroy(doorTwo);
                Destroy(doorThree);
                Destroy(doorFour);
                Destroy(doorFive);
                Vector3 PositionSix = new Vector3(-187.11f, -31.84001f, 75.93001f);
                doorSix.transform.position = PositionSix;
            }
            if (triedDoorSix == true)
            {
                Destroy(doorOne);
                Destroy(doorTwo);
                Destroy(doorThree);
                Destroy(doorFour);
                Destroy(doorFive);
                Destroy(doorSix);
            }
        }
        if(SceneManager.GetActiveScene().name == "YouDiedMenu")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        if(hasHealthLevel == true)
        {
            playerHealth += 20;
        }

        if(playerHealthController.health <= 0)
        {
            SceneManager.LoadScene("YouDiedMenu");
            playerHealthController.health = 20;
        }

        // check for current scene;
        scene = SceneManager.GetActiveScene().name;

        //DisplayPickLevelUp();


        // BULLET STUFF
        bulletCount = playerBulletController.bulletCount;
        UpdateBulletCount();
        UpdateSkillPoints();


        if(bulletCount <= 1)
        {
            bulletCount = 1;
        }

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

    private void UpdateSkillPoints()
    {
        if(skillPointText != null)
        {
            skillPointText.text = $"Skill points: {roomAmount}";
        }
    }

    public void NewBulletCount1()
    {
        if (roomAmount == 2)
        {
            playerBulletController.maxBullets = playerBulletController.maxBullets + 10;
            playerBulletController.bulletStandard = playerBulletController.bulletStandard + 10;
            roomAmount = 0;
        }
        PlayerPrefs.SetInt("MaxBullets", playerBulletController.maxBullets);
        PlayerPrefs.SetInt("BulletStandard", playerBulletController.bulletStandard);
    }

    public void NewBulletCount2()
    {
        if (roomAmount == 2)
        {
            playerBulletController.maxBullets = playerBulletController.maxBullets + 10;
            playerBulletController.bulletStandard = playerBulletController.bulletStandard + 10;
            roomAmount = 0;
        }
        PlayerPrefs.SetInt("MaxBullets", playerBulletController.maxBullets);
        PlayerPrefs.SetInt("BulletStandard", playerBulletController.bulletStandard);
    }

    public void NewBulletCount3()
    {
        if (roomAmount == 2)
        {
            playerBulletController.maxBullets = playerBulletController.maxBullets + 10;
            playerBulletController.bulletStandard = playerBulletController.bulletStandard + 10;
            roomAmount = 0;
        }
        PlayerPrefs.SetInt("MaxBullets", playerBulletController.maxBullets);
        PlayerPrefs.SetInt("BulletStandard", playerBulletController.bulletStandard);
    }

    public void NewHealth1()
    {
        if (roomAmount == 2)
        {
            playerHealthController.maxHealth = playerHealthController.maxHealth + 10;
            roomAmount = 0;
        }
        PlayerPrefs.SetFloat("MaxHealth", playerHealthController.maxHealth);
    }

    public void NewHealth2()
    {
        if (roomAmount == 2)
        {
            playerHealthController.maxHealth = playerHealthController.maxHealth + 10;
            roomAmount = 0;
        }
        PlayerPrefs.SetFloat("MaxHealth", playerHealthController.maxHealth);
    }

    public void NewHealth3()
    {
        if (roomAmount == 2)
        {
            playerHealthController.maxHealth = playerHealthController.maxHealth + 10;
            roomAmount = 0;
        }
        PlayerPrefs.SetFloat("MaxHealth", playerHealthController.maxHealth);
    }
}
