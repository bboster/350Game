using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    [SerializeField]
    private float sensX;
    [SerializeField]
    private float sensY;

    public Transform orientation;

    [SerializeField] private float xRotation;
    [SerializeField] private float yRotation;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        
    }
}
