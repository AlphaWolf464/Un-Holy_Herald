using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnAtVariblePoints : MonoBehaviour
{
    private PlayerUIScript playerUI;            //is set to the scrip that manages the quest and zone UI

    public GameObject spawnObject;              //takes a GameObject that will be spawned using this script
    [Tooltip("An empty GameObject that will be auto-set as a child, preferably with a 'y' value that is 2 local units above the ground.")]
    public GameObject[] spawnLocation;          //takes an array of GameObjects, who's position the 'spawnObject' clones will be spawned at
    private bool hasSpawned;                    //bool that makes sure only one 'spawnObject' is spawned at each 'spawnLocation'

    public string zoneName = "Zone 1";          //string to determine the name of the zone

    [HideInInspector] public int deadSpawn;     //int that tracks how many spawned 'spawnObjects' have been killed

    [HideInInspector] public bool zoneCleared;

    private void Start()                        //initilized all above varables to prefered starting values
    {
        playerUI = GameObject.FindWithTag("Player").GetComponent<PlayerUIScript>();

        for (int i = 0; i < spawnLocation.Length; i++)
        {
            spawnLocation[i].transform.parent = transform;
        }

        hasSpawned = false;
        transform.GetComponent<SphereCollider>().enabled = true;
        deadSpawn = 0;

        zoneCleared = false;
    }

    void Update()
    {
        if (deadSpawn >= spawnLocation.Length && playerUI.questOngoing == true)  //as soon as all spawned 'spawnObjects' are killed, anounce the clearing of this zone 
        {
            playerUI.zoneCleared();
        }
    }

    private void OnTriggerEnter(Collider other)                         //when a "player" enteres the colider for the first time, spawn the apropriate amount of 'spawnObjects'
    {
        if (other.CompareTag("Player") && hasSpawned == false && playerUI.questOngoing == false)
        {
            playerUI.spawner = transform.GetComponent<spawnAtVariblePoints>();
            playerUI.zoneName = zoneName;
            hasSpawned = true;
            transform.GetComponent<SphereCollider>().enabled = false;
            playerUI.ZoneEntered();
            for (int i = 0; i < spawnLocation.Length; i++)
            {
                GameObject clone;
                clone = Instantiate(spawnObject, spawnLocation[i].transform.position, spawnLocation[i].transform.rotation);
                clone.transform.parent = transform;
            }
        }
    }

    private void deathOfSpawn()                     //notes that a spawned 'spawnObject' has been killed at updates UI acordingly
    {
        deadSpawn++;
        playerUI.ResetQuestText();
    }
}
