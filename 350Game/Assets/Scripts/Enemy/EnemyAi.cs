using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private Transform playerPosition;
    [SerializeField]
    private float speed;

    [SerializeField]
    private float minDistance;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        trackPlayer();
    }

    // we want to give the enemy a push in the players direction 
    private void trackPlayer()
    {
        // turn enemy towards the player
        transform.LookAt(playerPosition);

        if (Vector3.Distance(transform.position , playerPosition.position) >= minDistance)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

    }
}
