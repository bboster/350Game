using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerUI : MonoBehaviour
{

    [SerializeField]
    public TextMeshProUGUI promptText;

    // Start is called before the first frame update

    public void UpdateText(string promptMessage)
    {
        promptText.text = promptMessage;
    }

    private void Start()
    {
        
    }
}
