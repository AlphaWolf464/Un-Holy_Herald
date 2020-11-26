using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnAtVariblePoints : MonoBehaviour
{
    public GameObject spawnObject;
    [Tooltip("An empty GameObject, preferably with a 'y' value that is 2 local units above the ground.")]
    public GameObject[] spawnLocation;
    private bool hasSpawned;

    private void Start()
    {
        hasSpawned = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && hasSpawned == false)
        {
            hasSpawned = true;
            for (int i = 0; i < spawnLocation.Length; i++)
            {
                GameObject clone;
                clone = Instantiate(spawnObject, spawnLocation[i].transform.position, spawnLocation[i].transform.rotation);
            }
        }
    }
}
