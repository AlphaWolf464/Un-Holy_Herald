using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class FallenAngelScript : MonoBehaviour
{
    private PlayerUIScript playerUI;                        //script that helps manage world UI functions

    GameObject target;                                      //gameobject that will be the destination of the fallen angel
    Vector3 destination;                                    //vector3 that is the fallen angel's destination position 
    NavMeshAgent agent;                                     //navMesh that is on the fallen angel

    private Image healthbar;                                //image that will be used to display the health of the fallen angel
    public float maxHealth = 10f;                           //float that determines the maximun health of the fallen angel
    private float currentHealth;                            //float that tracks the current health of the fallen angel

    private Collider attackZone;                            //collider that will be used in attacking
    private Text attackEffect;                              //text that will visually indicate attacks
    private bool attacksActive;

    public float meleeCooldown = 1f;                        //float that notes how long between melee attacks
    private float meleeCooldownOff;                         //float that notes at what time will another melee attack become possible
    private bool meleeAttackReady;                          //bool that notes if a melee attack is currently possible
    private bool meleeAttackActive;                         //bool that notes if a melee attack is currently active

    public float chargeCooldown = 10f;                      //float that notes how long between charge attacks
    private float chargeCooldownOff;                        //float that notes at what time will another charge attack become possible
    private bool chargeAttackReady;                         //bool that notes if a charge attack is currently possible
    private bool chargeAttackActive;                        //bool than notes if a charge attack is currently active
    private float powerUpTime = 2f;                         //float that notes how long the power up sequence will be
    private bool powerUpActive;                             //bools that notes if a power up sequence is currently active

    private float normalMoveSpeed = 3.5f;                   //float that stores the normal fallen angel navMesh movespeed
    private float normalTurnSpeed = 120f;                   //float that stores the normal fallen angel navMesh angularspeed
    private float normalAcceleration = 8f;                  //float that stores the normal fallen angel navMesh acceleration
    private float chargeMoveSpeed = 40f;                    //float that stores the fallen angel navMesh movespeed durring a charge
    private float chargeTurnSpeed = 0f;                     //float that stores the fallen angel navMesh angularspeed durring a charge
    private float chargeAcceleration = 50f;                 //float that stores the fallen angel navMesh acceleration durring a charge


    void Start()                                            //sets all above variables to their prefered starting values
    {
        playerUI = GameObject.FindWithTag("Player").GetComponent<PlayerUIScript>();

        target = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;

        agent.speed = normalMoveSpeed;
        agent.angularSpeed = normalTurnSpeed;
        agent.acceleration = normalAcceleration;
        agent.autoBraking = true;

        healthbar = transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        currentHealth = maxHealth;

        attackZone = transform.GetChild(2).transform.GetChild(0).GetComponent<Collider>();
        attackZone.enabled = false;
        attackEffect = transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        attackEffect.enabled = false;

        meleeCooldownOff = Time.time;
        meleeAttackReady = true;
        meleeAttackActive = false;

        chargeCooldownOff = Time.time;
        chargeAttackReady = true;
        chargeAttackActive = false;
        powerUpActive = false;
    }

    void Update()
    {
        if (!chargeAttackActive)
        {
            moveTowardsTargetCurrentLocation();
        }
        AreAttacksActive();

        IsMeleeAttackReady();

        IsChargeAttackReady();
        IsChargeAttackPossible();
    }

    private void moveTowardsTargetCurrentLocation() //resets the destination of the navMesh to the target's current position
    {
        if (Vector3.Distance(destination, target.transform.position) > 1.0f)
        {
            destination = target.transform.position;
            agent.destination = destination;
        }
    }

    private void OnTriggerEnter(Collider other)     //if a player attack collides with the fallen angel, take damage
    {
        if (CompareTag("Final Boss") && other.CompareTag("PlayerAttack"))
        {
            Damage(1);
        }
    }

    public void Damage(float damageAmount)          //aplies damage to the fallen angel's healthbar
    {
        currentHealth -= damageAmount;
        healthbar.fillAmount = (1 / maxHealth) * currentHealth;
      /*if (powerUpActive == true)
        {
            powerUpActive = false;
        }*/
        if (currentHealth <= 0)
        {
            playerUI.hitbox.enabled = false;
            FallenAngelDeath();
        }
    }

    private void FallenAngelDeath()                 //does all required actions to stop gameplay and send the player to the vicotry screen
    {
        playerUI.FallenAngelDead = true;
        agent.speed = 0;
        agent.angularSpeed = 0;
        GameObject.FindWithTag("Player").GetComponent<PlayerFaceMouseScript>().playerUI.freezeTurn = true;
        GameObject.FindWithTag("Player").GetComponent<PlayerAbilityScript>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<IsometricCharacterMoveScript>().enabled = false;
        StartCoroutine(GameObject.FindWithTag("Player").GetComponent<UIBlackoutScript>().FadeBlackOutSquare(true, 1));
        Invoke("SendToVictoryScreen", 3);
    }

    private void SendToVictoryScreen()              //calls the vicotry screen text function
    {
        playerUI.VictoryScreen();
    }

    private void AreAttacksActive()
    {
        if (meleeAttackActive == true || chargeAttackActive == true || powerUpActive == true)
        {
            attacksActive = true;
        }
        else
        {
            attacksActive = false;
        }
    }

    //Melee attack fucntions

    private void OnTriggerStay(Collider other)      //if the fallen angel collides with the player and a melee attack is alowed by cooldowns and exclsuion bools, take a melee attack
    {
        if (other.CompareTag("Player") && meleeAttackReady == true && !attacksActive)
        {
            meleeAttackAction();
        }
    }

    private void IsMeleeAttackReady()               //checks if enough time has passed for melee attack to be off its cooldown
    {
        if (Time.time > meleeCooldownOff)
        {
            meleeAttackReady = true;
        }
        else
        {
            meleeAttackReady = false;
        }
    }

    private void meleeAttackAction()                //starts melee attack actions
    {
        meleeAttackActive = true;
        attackEffect.text = "ATTACK";
        attackZone.transform.tag = "Boss Melee";
        attackEffect.enabled = true;
        attackZone.enabled = true;
        Invoke("meleeAttackRetreat", 0.1f);
    }

    private void meleeAttackRetreat()               //ends melee attack action
    {
        attackEffect.enabled = false;
        attackZone.enabled = false;
        meleeCooldownOff = Time.time + meleeCooldown;
        meleeAttackActive = false;
    }

    //charge attack functions

    private void IsChargeAttackPossible()           //checks to see if exclsuion bools currently allow for a charge attack
    {
        if (/*player in line of sight, and...*/ chargeAttackReady == true && !attacksActive)
        {
            chargeAttackPowerUp();
        }
    }

    private void IsChargeAttackReady()              //checks if enough time has passed for charge attack to be off its cooldown
    {
        if (Time.time > chargeCooldownOff)
        {
            chargeAttackReady = true;
        }
        else
        {
            chargeAttackReady = false;
        }
    }

    private void chargeAttackPowerUp()              //does a chargeup sequence
    {
        powerUpActive = true;
        attackEffect.text = "^          ^\n^          ^\n^          ^";
        attackEffect.enabled = true;
        agent.speed = 0.5f;
        Invoke("chargeAttackPrimer", powerUpTime);
    }
    private void chargeAttackPrimer()               //checks to see if all coditions at the end of the charge upallow for a charge attack
    {
        if (powerUpActive == true)
        {
            attackEffect.enabled = false;
            chargeAttackAction();
            powerUpActive = false;
        }
      /*else
        {
            attackEffect.enabled = false;
            chargeAttackRest();
        }*/
    }

    private void chargeAttackAction()               //starts charge attack action
    {
        chargeAttackActive = true;
        attackEffect.text = "CHARGE";
        attackZone.transform.tag = "Boss Charge";
        attackEffect.enabled = true;
        attackZone.enabled = true;
        agent.speed = chargeMoveSpeed;
        agent.angularSpeed = chargeTurnSpeed;
        agent.acceleration = chargeAcceleration;
        agent.autoBraking = false;
        Invoke("chargeAttackRest", 0.5f);
    }

    private void chargeAttackRest()                 //pauses fallen angel after a charge
    {
        agent.speed = 0;
        agent.angularSpeed = 0;
        Invoke("chargeAttackRetreat", 1f);
    }

    private void chargeAttackRetreat()              //ends charge attack action
    {
        attackEffect.enabled = false;
        attackZone.enabled = false;
        agent.speed = normalMoveSpeed;
        agent.angularSpeed = normalTurnSpeed;
        agent.acceleration = normalAcceleration;
        agent.autoBraking = true;
        chargeCooldownOff = Time.time + chargeCooldown;
        meleeCooldownOff = Time.time + (meleeCooldown * 2);
        chargeAttackActive = false;

    }
}
