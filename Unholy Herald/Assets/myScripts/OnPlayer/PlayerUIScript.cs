using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerUIScript : MonoBehaviour //When placed on the player, manages the player's UI (including death)
{
    public UIHealthbarScript healthBar;     //takes the script that provides functions for managing the player's heathbar
    public UIBlackoutScript blackout;       //takes the script that provides functions for blacking out the screen
    public PlayerAbilityScript ability;     //takes the script that provides functions and variables for managing abilites

    public int maxHealth = 100;             //takes an int that will be the player's max health
    private int currentHealth;              //int that tracks player's current health
    private bool playerAlive;               //bool that tracks if the player is alive or dead

    public Text deathscreen;                //takes the text that is used in the deathscreen

    public Image auraIconImage;             //takes the image of the aura icon
    public Text auraIconName;               //takes the name text of the aura icon
    public Text auraIconKey;                //takes the key text of the aura icon
    public Text auraIconCooldown;           //takes the cooldown text of the aura icon

    public Image beamIconImage;             //takes the image of the beam icon
    public Text beamIconName;               //takes the name text of the beam icon
    public Text beamIconKey;                //takes the key text of the beam icon
    public Text beamIconCooldown;           //takes the cooldown text of the beam icon

    public Image shieldIconImage;           //takes the image of the shield icon
    public Text shieldIconName;             //takes the name text of the shield icon
    public Text shieldIconKey;              //takes the key text of the shield icon
    public Text shieldIconCooldown;         //takes the cooldown text of the shield icon

    void Start()                            //sets all above vairables to their prefered start settings
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
        if (currentHealth <= 0 && playerAlive == true)      //runs proper functions when player dies
        {
            playerAlive = false;
            avatarDeath();
        }

        CheckAuraIcon(ability, auraIconImage);              //changes aura icon image to the current proper color

        CheckBeamIcon(ability, beamIconImage);              //changes beam icon image to the current proper color

        CheckShieldIcon(ability, shieldIconImage);          //changes shield icon image to the current proper color
    }

    public void takeDamage(int damage)                      //deducts proper amount damage from player healthbar UI
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);
    }

    private void avatarDeath()                              //runs proper fuctions to simulate player death
    {
        StartCoroutine(blackout.FadeBlackOutSquare());
        Invoke("deathScreen", 1);
        Invoke("backToMenu", 3);
    }

    private void deathScreen ()                             //enables deathscreen text
    {
        deathscreen.enabled = true;
    }

    private void backToMenu()                               //returns to the main menu scene
    {
        SceneManager.LoadScene(0);
    }

    private void SetNormalAbilityIconColor(Image target)    //changes 'target' image to normal icon color
    {
        target.color = new Color32(100, 217, 0, 255);
    }

    private void SetCooldownAbilityIconColor(Image target)  //changes 'target' image to cooldown icon color
    {
        target.color = new Color32(0, 100, 0, 255);
    }
    private void SetActiveAbilityIconColor(Image target)    //changes 'target' image to active icon color
    {
        target.color = new Color32(255, 217, 0, 255);
    }

    private void SetNameText(Text target, string name)      //sets 'target' text to proper icon format of 'name'
    {
        target.text = name;
    }

    private void SetKeyText(Text target, string key)        //sets 'target' text to proper icon fromat of 'key'
    {
        target.text = "Key: [" + key + "]";
    }

    private void SetCooldownText(Text target, float cooldown)//sets 'target' text to proper icon format of 'cooldown'
    {
        target.text = "Cooldown: " + cooldown + "s";
    }

    private void CheckAuraIcon(PlayerAbilityScript ability, Image auraImage)        //changes the auraImage to the correct color based on the aura attack bools
    {
        if (ability.isAuraOn == true)
        { SetActiveAbilityIconColor(auraImage); }                                   //changes auraImage to active color at proper times
        else if (ability.auraCooldownActive == true)
        { SetCooldownAbilityIconColor(auraImage); }                                 //changes auraImage to cooldown color at proper times
        else
        { SetNormalAbilityIconColor(auraImage); }                                   //changes auraImage to normal color at proper times
    }

    private void CheckBeamIcon(PlayerAbilityScript ability, Image beamImage)        //changes the beamImage to the correct color based on the beam attack bools
    {
        if (ability.isBeamOn == true)
        { SetActiveAbilityIconColor(beamImage); }                                   //changes beamImage to active color at proper times
        else if (ability.beamCooldownActive == true)
        { SetCooldownAbilityIconColor(beamImage); }                                 //changes beamImage to cooldown color at proper times
        else
        { SetNormalAbilityIconColor(beamImage); }                                   //changes beamImage to normal color at proper times
    }

    private void CheckShieldIcon(PlayerAbilityScript ability, Image shieldImage)    //changes the shieldImage to the correct color based on the shield bools
    {
        if (ability.isShieldOn == true)
        { SetActiveAbilityIconColor(shieldImage); }                                 //changes shieldImage to active color at proper times
        else if (ability.shieldCooldownActive == true)
        { SetCooldownAbilityIconColor(shieldImage); }                               //changes shieldImage to cooldown color at proper times
        else
        { SetNormalAbilityIconColor(shieldImage); }                                 //changes shieldImage to normal color at proper times
    }
}
