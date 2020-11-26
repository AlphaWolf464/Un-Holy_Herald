using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class spawnAtVariblePoints : MonoBehaviour
{
    public GameObject spawnObject;              //takes a GameObject that will be spawned using this script
    [Tooltip("An empty GameObject, preferably with a 'y' value that is 2 local units above the ground.")]
    public GameObject[] spawnLocation;          //takes an array of GameObjects, who's position the 'spawnObject' clones will be spawned at
    private bool hasSpawned;                    //bool that makes sure only one 'spawnObject' is spawned at each 'spawnLocation'

    private Text questLog;                      //text that will display the current quest
    private bool questOngoing;                  //bool to manage if a quest is currently ongoing

    private Text zoneMessage;                   //text to anounce zone messages
    public string zoneName = "Zone 1";          //string to determine the name of the zone
    private string zoneText;                    //string to determine quest output of the zone

    private int deadSpawn;                      //int that tracks how many spawned 'spawnObjects' have been killed

    private void Start()                        //initilized all above varables to prefered starting values
    {
        hasSpawned = false;

        questLog = GameObject.Find("Quest Log").GetComponent<Text>();
        questOngoing = false;

        zoneMessage = GameObject.Find("Zone Message").GetComponent<Text>();
        zoneMessage.enabled = false;
        zoneText = "";

        deadSpawn = 0;
    }

    void Update()
    {
        if (deadSpawn >= spawnLocation.Length && questOngoing == true)  //as soon as all spawned 'spawnObjects' are killed, anounce the clearing of this zone 
        {
            zoneCleared();
        }
        SetQuestText();                                                 //sets the quest log to current proper text settings
    }

    private void OnTriggerEnter(Collider other)                         //when a "player" enteres the colider for the first time, spawn the apropriate amount of 'spawnObjects'
    {
        if (other.CompareTag("Player") && hasSpawned == false)
        {
            hasSpawned = true;
            ZoneEntered();
            for (int i = 0; i < spawnLocation.Length; i++)
            {
                GameObject clone;
                clone = Instantiate(spawnObject, spawnLocation[i].transform.position, spawnLocation[i].transform.rotation);
                clone.transform.parent = transform;
            }
        }
    }

    private void deathOfSpawn()                     //notes that a spawned 'spawnObject' has been killed
    {
        deadSpawn++;
    }

    private void zoneCleared()                      //displays the zone clear text
    {
        questOngoing = false;
        zoneMessage.text = zoneName + " cleared!";
        zoneMessage.enabled = true;
        Invoke("ZoneMessageOff", 1f);
    }

    private void ZoneEntered()                      //displays the zone enter text
    {
        questOngoing = true;
        zoneMessage.text = zoneName + " entered.\nCombat Begins.";
        zoneMessage.enabled = true;
        Invoke("ZoneMessageOff", 2.5f);
    }

    private void ZoneMessageOff()                   //turns off 'zoneMessage'
    {
        zoneMessage.enabled = false;
    }

    private void SetQuestText()                     //sets the 'questLog' text to the correct text
    {
        if (questOngoing)
        {
            zoneText = "\n" + (spawnLocation.Length - deadSpawn) + " of " + spawnLocation.Length + " enemies left in " + zoneName;
        }
        else
        {
            zoneText = "\n[No quests]";
        }
        questLog.text = "Quest Log:\n" + zoneText;
    }
}
