using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
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
        }
        else if(sceneNumber == 2)
        {
            gameManager.triedDoorTwo = true;
        }
        else if(sceneNumber == 3)
        {
            gameManager.triedDoorThree = true;
        }
    }

    protected override void Interact()
    {
        SceneManager.LoadScene(transitionScene);
    }
}
