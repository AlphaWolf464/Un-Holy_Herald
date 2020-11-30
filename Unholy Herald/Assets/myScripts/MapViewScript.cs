using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapViewScript : MonoBehaviour
{
    public GameObject[] UIToToggle;
    public Camera playerCamera;
    public GameObject PlayerArrow;
    public Camera mapCamera;
    private RawImage pauseOverlay;

    void Start()
    {
        pauseOverlay = transform.parent.GetComponent<RawImage>();
        pauseOverlay.enabled = true;
        PlayerArrow.SetActive(false);
        playerCamera.enabled = true;
        mapCamera.enabled = false;
    }

    public void ToggleUI()
    {
        for (int i = 0; i < UIToToggle.Length; i++)
        {
            UIToToggle[i].SetActive(!UIToToggle[i].activeSelf);
        }
        pauseOverlay.enabled = !pauseOverlay.enabled;
        playerCamera.enabled = !playerCamera.enabled;
        mapCamera.enabled = !mapCamera.enabled;
        PlayerArrow.SetActive(!PlayerArrow.activeSelf);
    }
}
