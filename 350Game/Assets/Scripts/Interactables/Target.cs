using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public GameObject Target1;
    public GameObject Target2;
    public GameObject Target3;
    public GameObject Target4;
    public GameObject Target5;
    public GameObject Target6;
    public GameObject Self;

    bool spawn = false;

    private void Update()
    {
        if (!Target1 && !Target2 && !Target3 && !Target4 && !Target5 && !Target6)
        {
            spawn = true;
        }
        if (spawn)
        {
            Self.GetComponent<MeshRenderer>().enabled = true;
            Self.GetComponent<BoxCollider>().enabled = true;

        }
    }
}
