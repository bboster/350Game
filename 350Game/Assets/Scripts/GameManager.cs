using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool triedDoorOne;

    [SerializeField]
    private GameObject doorOne;

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

        doorOne = GameObject.FindGameObjectWithTag("DoorOne");
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(triedDoorOne == true)
        {
            doorOne.SetActive(false);
        }
    }
}
