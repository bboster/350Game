using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHead : MonoBehaviour
{

    [SerializeField]
    private Transform player;
    [SerializeField]
    private float speed;

    [SerializeField]
    private float maxTurn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        trackPlayer();
    }
    private void trackPlayer()
    {
        // turn enemy towards the player
        transform.LookAt(player);

        if (Vector3.Distance( transform.position, player.position) <= maxTurn && Vector3.Distance(transform.position, player.position) >= maxTurn)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        }

    }
}
