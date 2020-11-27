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
    public spawnAtVariblePoints spawner;    //takes the script that provides variables to determin information about spawned entities

    public int maxHealth = 100;             //takes an int that will be the player's max health
    private float currentPlayerHealth;      //int that tracks player's current health
    [HideInInspector] public float currentShieldHealth;//float that tracks player shield's current health
    private bool playerAlive;               //bool that tracks if the player is alive or dead
    
    public Text deathscreen;                //takes the text that is used in the deathscreen
    private bool isWriting;                 //bool that notes if deathscreen writing is actve
    private string deathscreenMessage;      //string that contains the message printed on the deathscreen
    private int characterIndex;             //int that helps manage deathscreen printing
    private float timer;                    //float that helps manage deathscreen printing speed
    private float writingSpeed;             //float that determies deathscreen printing speed

    public Image auraIconImage;             //takes the image of the aura icon
    private Text auraIconName;              //set to the name text of the aura icon
    private Text auraIconKey;               //set to the key text of the aura icon
    private Text auraIconCooldown;          //set to the cooldown text of the aura icon

    public Image beamIconImage;             //takes the image of the beam icon
    private Text beamIconName;              //set to the name text of the beam icon
    private Text beamIconKey;               //set to the key text of the beam icon
    private Text beamIconCooldown;          //set to the cooldown text of the beam icon

    public Image shieldIconImage;           //takes the image of the shield icon
    private Text shieldIconName;            //set to the name text of the shield icon
    private Text shieldIconKey;             //set to the key text of the shield icon
    private Text shieldIconCooldown;        //set to the cooldown text of the shield icon

    private Text questLog;                  //text that will display the current quest
    [HideInInspector] public bool questOngoing;//bool to manage if a quest is currently ongoing

    private Text zoneMessage;               //text to anounce zone messages
    public string zoneName = "Zone 1";      //string to determine the name of the zone
    private string zoneText;                //string to determine quest output of the zone

    void Start()                            //sets all above vairables to their prefered start settings
    {
        Time.timeScale = 1f;

        deathscreen.enabled = false;
        deathscreen.text = "";
        isWriting = false;
        deathscreenMessage = "Your avatar has died...\n You have failed the Lord";
        writingSpeed = 0.1f;

        currentPlayerHealth = maxHealth;
        healthBar.SetMaxPlayerHealth(maxHealth);
        healthBar.SetPlayerHealthToMax();
        healthBar.shieldHealth.transform.gameObject.SetActive(false);
        playerAlive = true;

        auraIconName = auraIconImage.transform.GetChild(0).GetComponent<Text>();
        auraIconKey = auraIconImage.transform.GetChild(1).GetComponent<Text>();
        auraIconCooldown = auraIconImage.transform.GetChild(2).GetComponent<Text>();

        beamIconName = beamIconImage.transform.GetChild(0).GetComponent<Text>();
        beamIconKey = beamIconImage.transform.GetChild(1).GetComponent<Text>();
        beamIconCooldown = beamIconImage.transform.GetChild(2).GetComponent<Text>();

        shieldIconName = shieldIconImage.transform.GetChild(0).GetComponent<Text>();
        shieldIconKey = shieldIconImage.transform.GetChild(1).GetComponent<Text>();
        shieldIconCooldown = shieldIconImage.transform.GetChild(2).GetComponent<Text>();

        SetNormalAbilityIconColor(auraIconImage);
        SetNameText(auraIconName, "Melee");
        SetKeyText(auraIconKey, ability.MeleeAbilityKey);
        SetCooldownText(auraIconCooldown, ability.auraAttackCooldown);

        SetNormalAbilityIconColor(beamIconImage);
        SetNameText(beamIconName, "Beam Attack");
        SetKeyText(beamIconKey, ability.BeamAbilityKey);
        SetCooldownText(beamIconCooldown, ability.beamAttackCooldown);

        SetNormalAbilityIconColor(shieldIconImage);
        SetNameText(shieldIconName, "Holy Shield");
        SetKeyText(shieldIconKey, ability.ShieldAbilityKey);
        SetCooldownText(shieldIconCooldown, ability.shieldCooldown);

        questLog = GameObject.Find("Quest Log").GetComponent<Text>();
        questOngoing = false;
        ResetQuestText();

        zoneMessage = GameObject.Find("Zone Message").GetComponent<Text>();
        zoneMessage.enabled = false;
        zoneText = "";
    }
    
    void Update()
    {
        if (currentPlayerHealth <= 0 && playerAlive == true)      //runs proper functions when player dies
        {
            playerAlive = false;
            avatarDeath();
        }
        if (currentShieldHealth <=0 && ability.isShieldOn == true)//runs proper functions when shield is overwhelmed
        {
            ability.shieldDown();
        }

        CheckAuraIcon(ability, auraIconImage);              //changes aura icon image to the current proper color
        CheckBeamIcon(ability, beamIconImage);              //changes beam icon image to the current proper color
        CheckShieldIcon(ability, shieldIconImage);          //changes shield icon image to the current proper color

        CheckTextWriter();                                  //types out a deathscreen message when 'isWriting` is true
    }

    public void takeDamage(float damage)                      //deducts proper amount damage from player healthbar UIs
    {
        if (ability.isShieldOn)
        {
            currentShieldHealth -= damage;
            healthBar.SetShieldHealth(currentShieldHealth);
        }
        else
        {
            currentPlayerHealth -= damage;
            healthBar.SetPlayerHealth(currentPlayerHealth);
        }
    }

    //This section of functions manages what happens on player death

    private void avatarDeath()                              //runs proper fuctions to simulate player death
    {
        StartCoroutine(blackout.FadeBlackOutSquare());
        Invoke("deathScreen", 1);
    }

    private void deathScreen ()                             //enables deathscreen text
    {
        deathscreen.enabled = true;
        SetTextWriter(deathscreen, deathscreenMessage, writingSpeed);
    }

    private void SetTextWriter(Text iuText, string text, float timePerCharacter)//sets varriables to proper setting for CheckTextWriter to work properly 
    {
        isWriting = true;
        deathscreenMessage = text + " ";
        characterIndex = 0;
        timer = 0f;
        writingSpeed = timePerCharacter;
    }

    private void CheckTextWriter()                          //runs proper functions to print out a deathscreen message when `isWriting` is true
    {
        if(isWriting)
        {
            timer -= Time.deltaTime;
            if (timer <= 0f)
            {
                if (deathscreenMessage[characterIndex] == '.' || deathscreenMessage[characterIndex + 1] == '.')
                {
                    timer += 0.7f;
                }
                else
                {
                    timer += writingSpeed;
                }

                characterIndex++;
                deathscreen.text = deathscreenMessage.Substring(0, characterIndex);

                if (characterIndex >= deathscreenMessage.Length - 1)
                {
                    isWriting = false;
                    Invoke("backToMenu", 2);
                    return;
                }
            }
        }
    }

    //This section of functions manages the action bar UI

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

    private void SetKeyText(Text target, KeyCode key)        //sets 'target' text to proper icon fromat of 'key'
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

    //This section of functions manages the quest log UI and zone clearing data

    public void ZoneEntered()                      //updates UI to acount for entering a combat zone
    {
        questOngoing = true;
        zoneMessage.text = zoneName + " entered.\nCombat Begins.";
        zoneMessage.enabled = true;
        ResetQuestText();
        Invoke("ZoneMessageOff", 3f);
    }

    public void zoneCleared()                       //updates UI to acount for clearing a combat zone
    {
        questOngoing = false;
        zoneMessage.text = zoneName + " cleared!";
        zoneMessage.enabled = true;
        Invoke("ZoneMessageOff", 1f);
        Invoke("ResetQuestText", 1f);
    }

    private void ZoneMessageOff()                   //turns off 'zoneMessage'
    {
        zoneMessage.enabled = false;
    }

    public void ResetQuestText()                    //sets the 'questLog' text to the correct text
    {
        if (questOngoing)
        {
            zoneText = "\n" + (spawner.spawnLocation.Length - spawner.deadSpawn) + " of " + spawner.spawnLocation.Length + " enemies left in " + zoneName;
        }
        else
        {
            zoneText = "\n[No quests]";
        }
        questLog.text = "Quest Log:\n" + zoneText;
    }
}
