using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doors : Interactable
{
    [SerializeField]
    private string transitionScene;
    private bool doorOpen;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GetComponent<GameManager>();
    }

    protected override void Interact()
    {
        if(gameManager != null)
        {
            Debug.Log("interacting with door" + gameObject.name);
            gameManager.TryDoor(gameObject);
            SceneManager.LoadScene(transitionScene);
        }
    }
}
