using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthbarScript : MonoBehaviour //When placed on the `healthbar` on the canvas, provides functions to `PlayerScript` for it to manage the player's healthbar
{
    private PlayerUIScript playerUI;

    public Slider playerHealth;
    public Slider shieldHealth;

    void Start()
    {
        playerUI = GameObject.FindWithTag("Player").GetComponent<PlayerUIScript>();
    }

    public void SetMaxPlayerHealth(float health)
    {
        playerHealth.maxValue = health;
    }

    public void SetPlayerHealthToMax()
    {
        playerHealth.value = playerHealth.maxValue;
    }

    public void SetPlayerHealth(float health)
    {
        playerHealth.value = health;
    }

    public void SetMaxShieldHealth(float health)
    {
        shieldHealth.maxValue = health;
    }

    public void SetShieldHealthToMax()
    {
        shieldHealth.value = shieldHealth.maxValue;
    }

    public void SetShieldHealth(float health)
    {
        shieldHealth.value = health;
    }

    public void ShieldDamageSpillover(float health)
    {
        if (health < 0)
        {
            playerUI.takeDamage(Mathf.Abs(health));
        }
    }
}
