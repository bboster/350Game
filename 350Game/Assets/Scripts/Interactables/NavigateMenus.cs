using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigateMenus : MonoBehaviour
{
    public static NavigateMenus Instance;
    private GameManager gameManager;
    [SerializeField] private PlayerHealth PlayerHealth;
    string scene;

    [SerializeField] private GameObject HowToPlay;
    [SerializeField] private GameObject MainMenu;
    private void Awake()
    {
        if (Instance == null)
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

    /*public void GivePowerUpHealth()
    {
        PlayerHealth.maxHealth = PlayerHealth.maxHealth + 20;
        SceneManager.LoadScene("Room0");
    }*/

    public void GoToHowToPlay()
    {
        HowToPlay.SetActive(true);
        MainMenu.SetActive(false);
    }
}
