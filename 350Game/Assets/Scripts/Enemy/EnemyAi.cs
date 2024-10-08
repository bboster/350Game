using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemyAi : MonoBehaviour
{
    private Rigidbody rb;

    [SerializeField]
    private Transform player;
    [SerializeField]
    private GameObject p;
    [SerializeField]
    private float speed;

    [SerializeField]
    private float minDistance;

    [SerializeField]
    private bool canAttack;
    [SerializeField]
    private float attackStrength;
    [SerializeField]
    private float attackDelay;
    private bool attackResetting;

    [SerializeField]
    private float Health;

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
        anim = GetComponent<Animator>();
        p = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        trackPlayer();
        /*if (!canAttack && !attackResetting)
        {
            attackResetting = true;
            Invoke("attackReset", attackDelay);
        }*/
    }

    // We want to give the enemy a push in the players direction 
    private void trackPlayer()
    {
        // turn enemy towards the player
        var playerPosition = player.position;
        playerPosition.y = transform.position.y;
        transform.LookAt(playerPosition);

        if (Vector3.Distance(transform.position, playerPosition) >= minDistance)
        {
            transform.position += transform.forward * speed * Time.deltaTime;
        } else if (canAttack) {
            anim.SetTrigger("Attack");
            p.GetComponent<PlayerHealth>().TakeDamage(attackStrength);
            canAttack = false;
            //attackResetting = false;
        }

    }

    private void TakeDamage()
    {
        Health--;
        if (Health <= 0)
        {
            anim.SetTrigger("Dead");
        }
    }


    //This is called at the end of the attack animation to start another attack.
    private void attackReset()
    {
        canAttack = true;
    }

    //Used for if a bullet collides with the enemy.
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Bullet")
        {
            TakeDamage();
            Destroy(collision.gameObject);
        }
    }
    
    //This is called at the end of the deaath animation to delete the enemy.
    private void Death()
    {
        Destroy(gameObject);
    }
}
