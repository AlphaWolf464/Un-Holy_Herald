using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbarScript : MonoBehaviour //When placed on an enemy, manages their healthbar, checks tosee if damage has been taken, and reports death to the parent
{
    public float currentHealth = 3;
    private Image healthBar;
    public float totalHealth;
    private GameObject parentObject;

    private void Start()
    {
        healthBar = transform.GetChild(15).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        totalHealth = currentHealth;
        parentObject = transform.parent.transform.gameObject;
    }
    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthBar.fillAmount = (1 / totalHealth) * currentHealth;

        if (currentHealth <= 0)
        {
            parentObject.SendMessage("deathOfSpawn");
            transform.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            Damage(1);
        }
    }
}
