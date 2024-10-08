using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject Target1;
    public GameObject Self;

    bool spawn = false;

    private void Update()
    {
        if (!Target1)
        {
            spawn = true;
        }
        if (spawn)
        {
            Self.SetActive(true);
        }
    }
}
