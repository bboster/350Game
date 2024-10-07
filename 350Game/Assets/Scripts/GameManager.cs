using System.Collections;
using System.Collections.Generic;
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

        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        doorOne = GameObject.FindGameObjectWithTag("DoorOne");
        doorTwo = GameObject.FindGameObjectWithTag("DoorTwo");
        doorThree = GameObject.FindGameObjectWithTag("DoorThree");
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

        if(triedDoorOne == true && triedDoorTwo == true && triedDoorThree == true && SceneManager.GetActiveScene().name == "Room0")
        {
            SceneManager.LoadScene("EndGame");
        }

        if(SceneManager.GetActiveScene().name == "EndGame")
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Room0");
    }

    public void QuitGame()
    {
        Debug.Log("Quit game");
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }


}
