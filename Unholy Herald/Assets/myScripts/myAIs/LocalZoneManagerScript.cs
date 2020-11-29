using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LocalZoneManagerScript : MonoBehaviour
{
    private PlayerUIScript playerUI;

    private GameObject[] zones;
    [HideInInspector] public bool levelCleared;
    [HideInInspector] public int zonesRemaining;

    public GameObject FinalBossPrefab;
    private GameObject FallenAngel;
    private GameObject bossSpawnpoint;

    void Start()
    {
        playerUI = GameObject.FindWithTag("Player").GetComponent<PlayerUIScript>();

        zones = GameObject.FindGameObjectsWithTag("Zone");
        levelCleared = false;
        zonesRemaining = zones.Length;

        bossSpawnpoint = GameObject.FindWithTag("Boss Spawn");
    }
    void Update()
    {
        if (levelCleared == false)
        {
            if (isLevelCleared() == true)
            {
                levelCleared = true;
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

    public void FinalBossSpawn()
    {
        Instantiate(FinalBossPrefab, bossSpawnpoint.transform.position, bossSpawnpoint.transform.rotation);
        FallenAngel = GameObject.FindWithTag("Final Boss");
        FallenAngel.SetActive(false);
        Invoke("FinalBossAwaken", 3);
    }

    private void FinalBossAwaken()
    {
        FallenAngel.SetActive(true);
    }
}
