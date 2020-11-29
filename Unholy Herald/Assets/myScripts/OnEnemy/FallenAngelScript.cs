using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class FallenAngelScript : MonoBehaviour
{
    private PlayerUIScript playerUI;

    GameObject target;
    Vector3 destination;
    NavMeshAgent agent;

    private Image healthbar;
    public float maxHealth = 20f;
    private float currentHealth;
    [HideInInspector] public bool FallenAngelDead;

    private bool meleeAttackReady;
    public float meleeCooldown = 1f;
    private float meleeCooldownOff;
    private Collider meleeAttackZone;
    private Text meleeAttackEffect;

    void Start()
    {
        playerUI = GameObject.FindWithTag("Player").GetComponent<PlayerUIScript>();

        transform.GetComponent<NavMeshAgent>().speed = 3.5f;
        transform.GetComponent<NavMeshAgent>().angularSpeed = 120;

        target = GameObject.FindWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        destination = agent.destination;

        healthbar = transform.GetChild(3).transform.GetChild(0).transform.GetChild(0).GetComponent<Image>();
        currentHealth = maxHealth;
        FallenAngelDead = false;

        meleeCooldownOff = Time.time;
        meleeAttackZone = transform.GetChild(2).transform.GetChild(0).GetComponent<Collider>();
        meleeAttackZone.enabled = false;
        meleeAttackZone.transform.tag = "Boss Melee";
        meleeAttackEffect = transform.GetChild(1).transform.GetChild(0).GetComponent<Text>();
        meleeAttackEffect.enabled = false;
    }

    void Update()
    {
        IsMeleeAttackReady();
        moveTowardsTargetCurrentLocation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerAttack"))
        {
            Damage(1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && meleeAttackReady == true)
        {
            meleeAttackAction();
        }
    }

    private void moveTowardsTargetCurrentLocation()
    {
        if (Vector3.Distance(destination, target.transform.position) > 1.0f)
        {
            destination = target.transform.position;
            agent.destination = destination;
        }
    }

    public void Damage(float damageAmount)
    {
        currentHealth -= damageAmount;
        healthbar.fillAmount = (1 / maxHealth) * currentHealth;

        if (currentHealth <= 0)
        {
            FallenAngelDeath();
        }
    }

    private void FallenAngelDeath()
    {
        FallenAngelDead = true;
        transform.GetComponent<NavMeshAgent>().speed = 0;
        transform.GetComponent<NavMeshAgent>().angularSpeed = 0;
        GameObject.FindWithTag("Player").GetComponent<PlayerFaceMouseScript>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<PlayerAbilityScript>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<IsometricCharacterMoveScript>().enabled = false;
        StartCoroutine(GameObject.FindWithTag("Player").GetComponent<UIBlackoutScript>().FadeBlackOutSquare(true, 1));
        Invoke("SendToVictoryScreen", 3);
    }

    private void SendToVictoryScreen()
    {
        playerUI.VictoryScreen();
    }

    //Melee attack fucntions

    private void IsMeleeAttackReady()
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

    private void meleeAttackAction()
    {
        meleeCooldownOff = Time.time + meleeCooldown;
        meleeAttackEffect.enabled = true;
        meleeAttackZone.enabled = true;
        Invoke("meleeAttackRetreat", 0.1f);
    }

    private void meleeAttackRetreat()
    {
        meleeAttackEffect.enabled = false;
        meleeAttackZone.enabled = false;
    }
}
