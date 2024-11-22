using System.Collections;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public bool IsClicked = false;
   
    public void OnButtonClick1()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if(gameManager != null & IsClicked == false)
        {
            IsClicked = true;
            gameManager.NewBulletCount1();
        }
    }

    public void OnButtonClick2()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null & IsClicked == false)
        {
            IsClicked = true;
            gameManager.NewBulletCount2();
        }
    }

    public void OnButtonClick3()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null & IsClicked == false)
        {
            IsClicked = true;
            gameManager.NewBulletCount3();
        }
    }

    public void NewGame()
    {
        //PlayerPrefs.DeleteAll();
        //SceneManager.LoadScene("Room0");
        PlayerHealthController playerHealth = FindObjectOfType<PlayerHealthController>();
        PlayerBulletController playerBullet = FindObjectOfType<PlayerBulletController>();
        GameManager gameManager = FindObjectOfType<GameManager>();

        playerHealth.ResetHealth();
        playerBullet.ResetBullets();
        gameManager.roomAmount = 0;
        PlayerPrefs.DeleteAll();

        SceneManager.LoadScene("Room0");
        
    }

    public void OnButtonClickHealth1()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null & IsClicked == false)
        {
            IsClicked = true;
            gameManager.NewHealth1();
        }
    }

    public void OnButtonClickHealth2()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null & IsClicked == false)
        {
            IsClicked = true;
            gameManager.NewHealth2();
        }
    }

    public void OnButtonClickHealth3()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();

        if (gameManager != null & IsClicked == false)
        {
            IsClicked = true;
            gameManager.NewHealth3();
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ReturnToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Room0");
    }

}
