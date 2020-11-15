using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BasicAttackScript : MonoBehaviour //When placed on an enemy, summons and desummons a collider to damage the player when in range of the player
{   
    public Collider attackZone;
    public Text attackEffect;
    public string foeTag; //Enemies take "PlayerAura" and player takes "Foe
    public float attackCooldown = 2;
    private bool cooldownActive;

    void Start()
    {
        attackZone.enabled = false;
        attackEffect.enabled = false;
        cooldownActive = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(foeTag) && cooldownActive == false)
        {
            attack();
            Invoke("attackZoneOff", 0.5f);
            Invoke("Cooldown", attackCooldown);

        }
    }

    private void attack()
    {
        cooldownActive = true;
        attackZone.enabled = true;
        attackEffect.enabled = true;
    }

    private void attackZoneOff()
    {
        attackZone.enabled = false;
        attackEffect.enabled = false;
    }

    private void Cooldown()
    {
        cooldownActive = false;
    }
}
