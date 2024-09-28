using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private float playerHealth = 100;

    private void FixedUpdate()
    {
        if(healthCheck() <= 0)
        {
            // death screen
        }
    }
    private float healthCheck()
    {
        return playerHealth;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerHealth = playerHealth - 5;
        }
    }
}
