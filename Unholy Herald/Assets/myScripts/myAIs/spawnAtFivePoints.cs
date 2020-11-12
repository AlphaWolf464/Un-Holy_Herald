using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnAtFivePoints : MonoBehaviour {

    public GameObject spawnObject;
    public int triggerTimes = 1;
    public GameObject spawnLocation1;
    public GameObject spawnLocation2;
    public GameObject spawnLocation3;
    public GameObject spawnLocation4;
    public GameObject spawnLocation5;

    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && triggerTimes >= 1) 
        {
            triggerTimes--;
            GameObject clone;
            clone = Instantiate(spawnObject, spawnLocation1.transform.position, spawnLocation1.transform.rotation);
            clone = Instantiate(spawnObject, spawnLocation2.transform.position, spawnLocation2.transform.rotation);
            clone = Instantiate(spawnObject, spawnLocation3.transform.position, spawnLocation3.transform.rotation);
            clone = Instantiate(spawnObject, spawnLocation4.transform.position, spawnLocation4.transform.rotation);
            clone = Instantiate(spawnObject, spawnLocation5.transform.position, spawnLocation5.transform.rotation);
        }
    }
}
