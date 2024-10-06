using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SendHomeInteractable : Interactable
{
    [SerializeField]
    private string transitionScene;

    private GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    protected override void Interact()
    {
        SceneManager.LoadScene(transitionScene);
    }
}