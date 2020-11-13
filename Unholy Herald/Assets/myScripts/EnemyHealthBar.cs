using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
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
        if (other.CompareTag("Player"))
        {
            Damage(1);
        }
    }
}
