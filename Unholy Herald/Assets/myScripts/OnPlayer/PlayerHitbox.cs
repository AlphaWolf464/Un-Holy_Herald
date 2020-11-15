using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitbox : MonoBehaviour //When placed on the player, makes the player take damage whenever an "Enemy" enters a 'trigger' collider on or under the player gameobject
{
    public PlayerScript player;
    public PlayerAttackScript abilities;

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Player") && other.CompareTag("Enemy") && abilities.isShieldOn == false)
        {
            player.takeDamage(5);
        }
    }
}
