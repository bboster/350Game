using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class is ABSTRACT so others can interhit from it
public abstract class Interactable : MonoBehaviour
{
    // display message to player
    public string message;

    // template method pattern
    public void BaseInteract()
    {
        Interact();
    }
    protected virtual void Interact()
    {
        // template function to be overwritten
    }
}