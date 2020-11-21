﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHitboxTriggerScript : MonoBehaviour //When placed on the player, makes the player take damage whenever an "Enemy" enters a 'trigger' collider on or under the player gameobject
{
    public PlayerUIScript player;

    private void OnTriggerEnter(Collider other)
    {
        if (CompareTag("Player") && other.CompareTag("Enemy"))
        {
            player.takeDamage(5);
        }
    }
}
