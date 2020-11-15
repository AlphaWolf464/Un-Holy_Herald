using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour //When placed on the main chamera, makes the main camera move to follow the player's possition
{
    public Transform PlayerTransform; //takes the player avatar

    private Vector3 _cameraOffset;

    [Range(0.1f, 1.0f)]
    public float SmoothFactor = 0.5f;
    void Start()
    {
        _cameraOffset = transform.position - PlayerTransform.position;
        
    }

    void LateUpdate()
    {
        Vector3 newPos = PlayerTransform.position + _cameraOffset;

        transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
    }
}

