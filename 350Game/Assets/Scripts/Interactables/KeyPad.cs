using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyPad : Interactable
{
    [SerializeField]
    private GameObject door;
    private bool doorOpen;

    [SerializeField]
    private string transitionScene;

    [SerializeField]
    private int sceneNumber;

    [SerializeField]
    private GameManager gameManager;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if(sceneNumber == 1)
        {
            gameManager.triedDoorOne = true;
            gameManager.roomAmount++;
        }
        else if(sceneNumber == 2)
        {
            gameManager.triedDoorTwo = true;
            gameManager.roomAmount++;
        }
        else if(sceneNumber == 3)
        {
            gameManager.triedDoorThree = true;
            gameManager.roomAmount++;
        }
        else if(sceneNumber == 4)
        {
            gameManager.triedDoorFour = true;
            gameManager.roomAmount++;
        }
        else if (sceneNumber == 5)
        {
            gameManager.triedDoorFive = true;
            gameManager.roomAmount++;
        }
        else if (sceneNumber == 6)
        {
            gameManager.triedDoorSix = true;
            gameManager.roomAmount++;
        }
    }

    protected override void Interact()
    {
        SceneManager.LoadScene(transitionScene);
    }
}
