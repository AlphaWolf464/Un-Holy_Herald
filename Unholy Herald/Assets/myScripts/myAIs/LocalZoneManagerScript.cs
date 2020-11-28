using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalZoneManagerScript : MonoBehaviour
{
    private PlayerUIScript playerUI;

    private GameObject[] zones;
    [HideInInspector] public bool levelCleared;
    [HideInInspector] public int zonesRemaining;

    void Start()
    {

        playerUI = GameObject.FindWithTag("Player").GetComponent<PlayerUIScript>();

        zones = GameObject.FindGameObjectsWithTag("Zone");

        levelCleared = false;

        zonesRemaining = zones.Length;
    }
    void Update()
    {
        if (levelCleared == false)
        {
            if (isLevelCleared() == true)
            {
                levelCleared = true;
                playerUI.EndOfLevelFade();
            }
        }
    }

    private bool isLevelCleared()
    {
        for (int i = 0; i < zones.Length; i++)
        {
            if (zones[i].GetComponent<spawnAtVariblePoints>().zoneCleared == false)
            {
                return false;
            }
        }
        return true;
    }

    private void zoneConquered()
    {
        zonesRemaining--;
        playerUI.ResetUnclearedZoneText();
    }
}
