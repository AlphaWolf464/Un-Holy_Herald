using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class uiFaceCam : MonoBehaviour //When placed on an enemy's `healthbar` canvas, makes sure that the enemy's healthbar is pointed at the main camera

{
    Camera cameraToLookAt;

    private void Start()
    {
        cameraToLookAt = Camera.main;
    }
    void Update()
    {
        transform.LookAt(transform.position + cameraToLookAt.transform.rotation * Vector3.back,
                        cameraToLookAt.transform.rotation * Vector3.up);
        transform.Rotate(0, 180, 0);
    }
}
