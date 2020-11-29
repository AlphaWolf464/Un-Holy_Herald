using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityScript : MonoBehaviour //When placed on player, manages attacks and abilites
{
    private UIHealthbarScript healthbars;               //takes a script which provides functions to manage healthbar sliders
    [HideInInspector] public PlayerUIScript playerUI;   //takes a script which provides vraibles to manage healthbar sliders

    public KeyCode MeleeAbilityKey = KeyCode.Space;     //takes a keycode that will be used to activate the melee (aka aura) ability
    public KeyCode BeamAbilityKey = KeyCode.Mouse0;     //takes a keycode that will be used to activate the beam ability
    public KeyCode ShieldAbilityKey = KeyCode.Mouse1;   //takes a keycode that will be used to activate the shield avility
    
    [HideInInspector] public bool isAuraOn;             //bool that notes if the aura is active
    private Collider auraAttackZone;                    //takes a collider that surrounds the player
    private Text auraAttackEffect;                      //takes a text object to be used as a visual indicator of 'auraAttackZone'
    public float auraAttackCooldown = 0.5f;             //takes a float that will determien the time between activations of 'auraAttack'
    [HideInInspector] public bool auraCooldownActive;   //bool that will help make the cooldown effective
    private float auraAttackDurration = 0.2f;           //float that determines the length of attack of 'auraAttack'

    [HideInInspector] public bool isBeamOn;             //bool that notes if the beam is active
    private Collider beamAttackZone;                    //takes a collider that streches out in front of the player
    private Text beamAttackEffect;                      //takes a text object to be used as a visual indicator of 'beamAttackZone'
    public float beamAttackCooldown = 2f;               //takes a float that will determien the time between activations of 'beamAttack'
    [HideInInspector] public bool beamCooldownActive;   //bool that will help make the cooldown effective
    private float beamAttackDurration = 0.1f;           //float that determines the length of attack of 'beamAttack'

    [HideInInspector] public bool isShieldOn;           //bool that notes if the shield is active (used widely for making sure player can't take damage or attack while shield us up)
    private Text shieldEffect;                          //takes a text object  to be used as a visual indicator of 'shieldUp'
    public float shieldCooldown = 5f;                   //takes a float that will determien the time between activations of 'shieldUp'
    [HideInInspector] public bool shieldCooldownActive; //bool that will help make the cooldown effective
    public float shieldDurration = 4f;                  //takes a float that determines the length of uptime for 'shieldUp'

    void Start()                                        //initilizes all above variables to their prefered start condition
    {
        healthbars = GameObject.Find("Healthbar").GetComponent<UIHealthbarScript>();
        playerUI = GameObject.FindWithTag("Player").GetComponent<PlayerUIScript>();

        auraAttackZone = GameObject.FindWithTag("Player").transform.GetChild(1).transform.GetChild(0).GetComponent<Collider>();
        auraAttackZone.enabled = false;
        auraAttackEffect = GameObject.FindWithTag("Player").transform.GetChild(2).transform.GetChild(0).GetComponent<Text>();
        auraAttackEffect.enabled = false;
        auraCooldownActive = false;

        beamAttackZone = GameObject.FindWithTag("Player").transform.GetChild(1).transform.GetChild(1).GetComponent<Collider>();
        beamAttackZone.enabled = false;
        beamAttackEffect = GameObject.FindWithTag("Player").transform.GetChild(2).transform.GetChild(1).GetComponent<Text>();
        beamAttackEffect.enabled = false;
        beamCooldownActive = false;

        isShieldOn = false;
        shieldEffect = GameObject.FindWithTag("Player").transform.GetChild(2).transform.GetChild(2).GetComponent<Text>();
        shieldEffect.enabled = false;
        shieldCooldownActive = false;

        properAttackColliderTag(auraAttackZone);
        properAttackColliderTag(beamAttackZone);

    }

    void Update()
    {
        if (Input.GetKeyDown(MeleeAbilityKey) && auraCooldownActive == false && isShieldOn == false)      //triggers if 'Space' is pressed while the aura attack is off cooldown and the shield is down
        {
            auraAttack();                                                                               //activates the aura attack
            Invoke("auraAttackOff", auraAttackDurration);                                               //turns off the aura attack after the perscribed attack duration
            Invoke("auraCooldown", auraAttackDurration + auraAttackCooldown);                           //puts the aura attack off cooldown after perscribed cooldown length

        }
        if (Input.GetKeyDown(BeamAbilityKey) && beamCooldownActive == false && isShieldOn == false)     //triggers if '1' is pressed while the beam attack is off cooldown and the shield is down
        {
            beamAttack();                                                                               //activates the beam attack
            Invoke("beamAttackOff", beamAttackDurration);                                               //turns off the beam attack after the perscribed attack durration
            Invoke("beamCooldown", beamAttackDurration + beamAttackCooldown);                           //puts the beam attack off cooldown after perscribed cooldown duration
        }
        if (Input.GetKeyDown(ShieldAbilityKey) && shieldCooldownActive == false && isShieldOn == false)   //triggers if '2' is pressed while the shield is off cooldown and is down
        {
            shieldUp();                                                                                 //actives the shield
            Invoke("shieldDown", shieldDurration);                                                      //turns off the shield after the perscribed shield durration
            Invoke("shieldRecharged", shieldDurration + shieldCooldown);                                //puts the shield off cooldown after the correct amount of time
        }
    }

    private void properAttackColliderTag(Collider attack)   //takes an attack collider and makes sure it has the proper tag to work with the 'EnemyHealthBar' script
    {
        attack.transform.tag = "PlayerAttack";
    }

    private void auraAttack()                               //changes proper bools so aura attack starts
    {
        isAuraOn = true;
        auraAttackZone.enabled = true;
        auraAttackEffect.enabled = true;
    }

    private void auraAttackOff()                            //changes proper bools so aura attack ends
    {
        auraCooldownActive = true;
        isAuraOn = false;
        auraAttackZone.enabled = false;
        auraAttackEffect.enabled = false;
    }

    private void auraCooldown()                             //changes proper bools so aura attack comes off cooldown
    {
        auraCooldownActive = false;
    }

    private void beamAttack()                               //changes proper bools so beam attack starts
    {
        isBeamOn = true;
        beamAttackZone.enabled = true;
        beamAttackEffect.enabled = true;
    }

    private void beamAttackOff()                            //changes proper bools so beam attack ends
    {
        beamCooldownActive = true;
        isBeamOn = false;
        beamAttackZone.enabled = false;
        beamAttackEffect.enabled = false;
    }

    private void beamCooldown()                             //changes proper bools so beam attack comes off cooldown
    {
        beamCooldownActive = false;
    }

    private void shieldUp()                                 //runs proper functions to set up a player shield
    {
        isShieldOn = true;
        healthbars.shieldHealth.transform.gameObject.SetActive(true);
        healthbars.SetMaxShieldHealth(playerUI.maxHealth / 2);
        healthbars.SetShieldHealthToMax();
        playerUI.currentShieldHealth = healthbars.shieldHealth.value;
        shieldEffect.enabled = true;
    }

    public void shieldDown()                               //runs proper functions so player shield is deactivated
    {
        shieldCooldownActive = true;
        isShieldOn = false;
        healthbars.shieldHealth.transform.gameObject.SetActive(false);
        healthbars.ShieldDamageSpillover(playerUI.currentShieldHealth);
        shieldEffect.enabled = false;
    }

    private void shieldRecharged()                          //changes proper bools so shield comes off cooldown
    {
        shieldCooldownActive = false;
    }
}
