using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    //public float health;
    private float lerpTimer;
    //public float maxHealth = 100;
    public float chipSpeed = 2;

    // the healthbars
    public Image frontHealthBar;
    public Image backHealthBar;

    public PlayerHealthController healthController;

    // Start is called before the first frame update
    void Start()
    {
        GameObject controllerObj = GameObject.FindGameObjectWithTag("PlayerHealthController");
        if (controllerObj != null)
        {
            healthController = controllerObj.GetComponent<PlayerHealthController>();
        }
        healthController.health = healthController.maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        healthController.health = Mathf.Clamp(healthController.health, 0, healthController.maxHealth);
        UpdateHealthUI();
    }

    public void UpdateHealthUI()
    {
        //Debug.Log(health);
        float fillFront = frontHealthBar.fillAmount;
        float fillBack = backHealthBar.fillAmount;
        float hFraction = healthController.health / healthController.maxHealth;
        if(fillBack > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            backHealthBar.color = Color.red;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            backHealthBar.fillAmount = Mathf.Lerp(fillBack, hFraction, percentComplete);
        }

        if(fillFront < hFraction)
        {
            backHealthBar.color = Color.green;
            backHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float percentComplete = lerpTimer / chipSpeed;
            percentComplete = percentComplete * percentComplete;
            frontHealthBar.fillAmount = Mathf.Lerp(fillFront, hFraction, percentComplete);

        }
    }

    public void TakeDamage(float damage)
    {
        healthController.health -= damage;
        lerpTimer = 0;
    }

    public void RestoreHealth(float healAmount)
    {
        healthController.health += healAmount;
        lerpTimer = 0;
    }
}
