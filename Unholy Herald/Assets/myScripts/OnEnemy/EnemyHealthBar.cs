using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour //When placed on an enemy, manages their healthbar as well as checking to see if damage has been taken
{
    public float currentHealth = 3;
    public Image healthBar;
    float totalHealth;

    private void Start()
    {
        totalHealth = currentHealth;

    }
    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.fillAmount = (1 / totalHealth) * currentHealth;

        if (currentHealth <= 0)
        {
            if (gameObject.transform.parent)
            {
                gameObject.transform.parent.gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAura"))
        {
            Damage(1);
        }
    }
}
