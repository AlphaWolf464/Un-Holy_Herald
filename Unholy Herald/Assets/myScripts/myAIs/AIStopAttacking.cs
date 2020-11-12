using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIStopAttacking : MonoBehaviour {

    // Use this for initialization
    public GameObject patrolObject;
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            patrolObject.GetComponent<Patrol>().attacking = false;
            Debug.Log("Exit");
            patrolObject.GetComponent<Patrol>().GotoNextPoint();
            GetComponentInParent<NavMeshAgent>().autoBraking = false;
            GetComponentInParent<NavMeshAgent>().stoppingDistance = 0;
        }
    }
}
