using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;
    public Text deathscreen;
    public Blackout blackout;

    void Start()
    {
        deathscreen.enabled = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentHealth <= 0)
        {
            avatarDeath();
        }
    }

    public void takeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void avatarDeath()
    {
        StartCoroutine(blackout.FadeBlackOutSquare());
        Invoke("deathScreen", 1);
    }

    private void deathScreen ()
    {
        deathscreen.enabled = true;
    }
}
