using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this class is ABSTRACT so others can interhit from it
public abstract class Interactable : MonoBehaviour
{
    // add or remove an InteractionEvent component to this gameobject
    public bool useEvents;
    // display message to player
    public string message;

    // template method pattern
    public void BaseInteract()
    {
        if (useEvents)
        {
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        }
        Interact();
    }
    protected virtual void Interact()
    {
        // template function to be overwritten
    }
}
