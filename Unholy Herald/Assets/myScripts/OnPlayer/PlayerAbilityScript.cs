using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAbilityScript : MonoBehaviour //When placed on player, manages attacks and abilites
{
    public Collider auraAttackZone;                     //takes a collider that surrounds the player
    public Text auraAttackEffect;                       //takes a text object to be used as a visual indicator of 'auraAttackZone'
    public float auraAttackCooldown = 0.5f;             //takes a float that will determien the time between activations of 'auraAttack'
    [HideInInspector] public bool auraCooldownActive;   //bool that will help make the cooldown effective
    private float auraAttackDurration = 0.2f;           //float that determines the length of attack of 'auraAttack'

    public Collider beamAttackZone;                     //takes a collider that streches out in front of the player
    public Text beamAttackEffect;                       //takes a text object to be used as a visual indicator of 'beamAttackZone'
    public float beamAttackCooldown = 2f;               //takes a float that will determien the time between activations of 'beamAttack'
    [HideInInspector] public bool beamCooldownActive;   //bool that will help make the cooldown effective
    private float beamAttackDurration = 0.1f;           //float that determines the length of attack of 'beamAttack'

    [HideInInspector] public bool isShieldOn;           //bool that notes if the shield is active (used widely for making sure player can't take damage or attack while shield us up)
    public Text shieldEffect;                           //takes a text object  to be used as a visual indicator of 'shieldUp'
    public float shieldCooldown = 5f;                   //takes a float that will determien the time between activations of 'shieldUp'
    [HideInInspector] public bool shieldCooldownActive; //bool that will help make the cooldown effective
    public float shieldDurration = 2f;                  //takes a float that determines the length of uptime for 'shieldUp'

    void Start()                                        //initilizes all above variables to their prefered start condition
    {
        properAttackColliderTag(auraAttackZone);
        properAttackColliderTag(beamAttackZone);

        auraAttackZone.enabled = false;
        auraAttackEffect.enabled = false;
        auraCooldownActive = false;

        beamAttackZone.enabled = false;
        beamAttackEffect.enabled = false;
        beamCooldownActive = false;

        isShieldOn = false;
        shieldEffect.enabled = false;
        shieldCooldownActive = false;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && auraCooldownActive == false && isShieldOn == false)      //triggers if 'Space' is pressed while the aura attack is off cooldown and the shield is down
        {
            auraAttack();                                                                               //activates the aura attack
            Invoke("auraAttackOff", auraAttackDurration);                                               //turns off the aura attack after the perscribed attack duration
            Invoke("auraCooldown", auraAttackCooldown);                                                 //puts the aura attack off cooldown after perscribed cooldown length

        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && beamCooldownActive == false && isShieldOn == false)     //triggers if '1' is pressed while the beam attack is off cooldown and the shield is down
        {
            beamAttack();                                                                               //activates the beam attack
            Invoke("beamAttackOff", beamAttackDurration);                                               //turns off the beam attack after the perscribed attack durration
            Invoke("beamCooldown", beamAttackCooldown);                                                 //puts the beam attack off cooldown after perscribed cooldown duration
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && shieldCooldownActive == false && isShieldOn == false)   //triggers if '2' is pressed while the shield is off cooldown and is down
        {
            shieldUp();                                                                                 //actives the shield
            Invoke("shieldDown", shieldDurration);                                                      //turns off the shield after the perscribed shield durration
            Invoke("shieldRecharged", shieldDurration + shieldCooldown);                                //puts the shield off cooldown after the correct amount of time
        }
    }

    private void properAttackColliderTag(Collider attack)   //takes an attack collider and makes sure it has the proper tag to work with the 'EnemyHealthBar' script
    {
        attack.tag = "PlayerAttack";
    }

    private void auraAttack()                               //changes proper bools so aura attack starts
    {
        auraCooldownActive = true;
        auraAttackZone.enabled = true;
        auraAttackEffect.enabled = true;
    }

    private void auraAttackOff()                            //changes proper bools so aura attack ends
    {
        auraAttackZone.enabled = false;
        auraAttackEffect.enabled = false;
    }

    private void auraCooldown()                             //changes proper bools so aura attack comes off cooldown
    {
        auraCooldownActive = false;
    }

    private void beamAttack()                               //changes proper bools so beam attack starts
    {
        beamCooldownActive = true;
        beamAttackZone.enabled = true;
        beamAttackEffect.enabled = true;
    }

    private void beamAttackOff()                            //changes proper bools so beam attack ends
    {
        beamAttackZone.enabled = false;
        beamAttackEffect.enabled = false;
    }

    private void beamCooldown()                             //changes proper bools so beam attack comes off cooldown
    {
        beamCooldownActive = false;
    }

    private void shieldUp()                                 //changes proper bools so shield is activated and can block damage & ability activations
    {
        isShieldOn = true;
        shieldEffect.enabled = true;
    }

    private void shieldDown()                               //changes proper bools so shield is deactivated and no longer blocks damage & abiility activations
    {
        shieldCooldownActive = true;
        isShieldOn = false;
        shieldEffect.enabled = false;
    }

    private void shieldRecharged()                          //changes proper bools so shield comes off cooldown
    {
        shieldCooldownActive = false;
    }
}
