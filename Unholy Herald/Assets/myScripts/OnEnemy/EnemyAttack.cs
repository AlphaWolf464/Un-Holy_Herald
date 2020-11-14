using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour //When placed on an enemy, summons and desummons a collider to damage the player when in range of the player
{
    public FollowTarget attackScript;
    
    public Collider attackZone;
    private bool cooldownActive;

    void Start()
    {
        attackZone.enabled = false;
        cooldownActive = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("PlayerAura") && cooldownActive == false)
        {
            cooldownActive = true;
            attackZone.enabled = true;
            Invoke("attackZoneOff", 0.5f);
            Invoke("Cooldown", 2);

        }
    }

    private void attackZoneOff()
    {
        attackZone.enabled = false;
    }

    private void Cooldown()
    {
        cooldownActive = false;
    }
}
