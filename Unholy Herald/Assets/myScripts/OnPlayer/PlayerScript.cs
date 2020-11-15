using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour //When placed on the player, manages the player's healthbar and death
{
    public int maxHealth = 100;
    public int currentHealth;
    private bool playerAlive;

    public HealthBar healthBar;
    public Text deathscreen;
    public Blackout blackout;

    void Start()
    {
        deathscreen.enabled = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHealth <= 0 && playerAlive == true)
        {
            playerAlive = false;
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
        Invoke("backToMenu", 3);
    }

    private void deathScreen ()
    {
        deathscreen.enabled = true;
    }

    private void backToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
