using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour {

    public GameObject spawnObj;
    public GameObject spawnLoc;
    public int spawnableObjs;


    private void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("Player") && (spawnableObjs >=1)) 
        {
            spawnableObjs = spawnableObjs - 1;
            GameObject clone;
            clone = Instantiate(spawnObj, spawnLoc.transform.position, spawnLoc.transform.rotation);
        }
    }
}
