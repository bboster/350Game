using UnityEditor;

[CustomEditor(typeof(Interactable), true)]
public class InteractableEditor : Editor
{
    public override void OnInspectorGUI()
    {
        Interactable interactable = (Interactable)target;
        base.OnInspectorGUI();
        if (target.GetType() == typeof(EventOnlyInteractable))
        {
            interactable.message = EditorGUILayout.TextField("Prompt Message", interactable.message);
            EditorGUILayout.HelpBox("EventOnlyInteract can ONLY use UnityEvents", MessageType.Info);
            if(interactable.GetComponent<InteractionEvent>() == null)
            {
                interactable.useEvents = true;
                interactable.gameObject.AddComponent<InteractionEvent>();
            }
        }
        else
        {
            if (interactable.useEvents)
            {
                if (interactable.GetComponent<InteractionEvent>() == null)
                {
                    interactable.gameObject.AddComponent<InteractionEvent>();
                }

            }
            else
            {
                if (interactable.GetComponent<InteractionEvent>() != null)
                {
                    DestroyImmediate(interactable.GetComponent<InteractionEvent>());
                }
            }
        }
    }
}
