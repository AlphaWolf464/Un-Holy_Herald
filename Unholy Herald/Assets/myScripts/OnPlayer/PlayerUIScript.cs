using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUIScript : MonoBehaviour //When placed on the player, manages the player's healthbar and death
{
    public UIHealthbarScript healthBar;
    public UIBlackoutScript blackout;
    public PlayerAbilityScript ability;

    public int maxHealth = 100;
    private int currentHealth;
    private bool playerAlive;

    public Text deathscreen;

    public Image auraIconImage;
    public Text auraIconName;
    public Text auraIconKey;
    public Text auraIconCooldown;

    public Image beamIconImage;
    public Text beamIconName;
    public Text beamIconKey;
    public Text beamIconCooldown;

    public Image shieldIconImage;
    public Text shieldIconName;
    public Text shieldIconKey;
    public Text shieldIconCooldown;

    void Start()
    {
        deathscreen.enabled = false;
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        playerAlive = true;

        SetNormalAbilityIconColor(auraIconImage);
        SetNameText(auraIconName, "Melee");
        SetKeyText(auraIconKey, "Space");
        SetCooldownText(auraIconCooldown, ability.auraAttackCooldown);

        SetNormalAbilityIconColor(beamIconImage);
        SetNameText(beamIconName, "Beam Attack");
        SetKeyText(beamIconKey, "1");
        SetCooldownText(beamIconCooldown, ability.beamAttackCooldown);

        SetNormalAbilityIconColor(shieldIconImage);
        SetNameText(shieldIconName, "Holy Shield");
        SetKeyText(shieldIconKey, "2");
        SetCooldownText(shieldIconCooldown, ability.shieldCooldown);
    }

    void Update()
    {
        if (currentHealth <= 0 && playerAlive == true)
        {
            playerAlive = false;
            avatarDeath();
        }

        if (ability.auraCooldownActive == true)
        { SetCooldownAbilityIconColor(auraIconImage); }
        else
        { SetNormalAbilityIconColor(auraIconImage); }

        if (ability.beamCooldownActive == true)
        { SetCooldownAbilityIconColor(beamIconImage); }
        else
        { SetNormalAbilityIconColor(beamIconImage); }

        if(ability.isShieldOn == true)
        { SetActiveAbilityIconColor(shieldIconImage); }
        else if (ability.shieldCooldownActive == true)
        { SetCooldownAbilityIconColor(shieldIconImage); }
        else
        { SetNormalAbilityIconColor(shieldIconImage);  }
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

    private void SetNormalAbilityIconColor(Image target)
    {
        target.color = new Color32(100, 217, 0, 255);
    }

    private void SetCooldownAbilityIconColor(Image target)
    {
        target.color = new Color32(0, 100, 0, 255);
    }
    private void SetActiveAbilityIconColor(Image target)
    {
        target.color = new Color32(255, 217, 0, 255);
    }

    private void SetNameText(Text target, string name)
    {
        target.text = name;
    }

    private void SetKeyText(Text target, string key)
    {
        target.text = "Key: [" + key + "]";
    }

    private void SetCooldownText(Text target, float time)
    {
        target.text = "Cooldown: " + time + "s";
    }
}
