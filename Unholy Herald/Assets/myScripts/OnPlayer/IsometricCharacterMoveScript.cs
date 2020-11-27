using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsometricCharacterMoveScript : MonoBehaviour //When placed on the player parent, makes sure that the player can walk normaly with WASD in an isometric enviorment
{
    //if you want to understand how this code works, go see the video link in the "Un-Holy Herald Team" discord
    //under the #assets channel, posted at 11/10/2020
    [SerializeField]
    public float moveSpeed = 5f;

    Vector3 forward, right;
    void Start()
    {
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }


    void Update()
    {
        if(WASDkeyDown())
        {
            Move();
        }
    }

    void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxis("HorizontalKey"), 0, Input.GetAxis("VerticalKey"));
        Vector3 rightMovement = right * moveSpeed * Time.deltaTime * Input.GetAxis("HorizontalKey");
        Vector3 upMovement = forward * moveSpeed * Time.deltaTime * Input.GetAxis("VerticalKey");

        Vector3 heading = Vector3.Normalize(rightMovement + upMovement);

        transform.position += rightMovement;
        transform.position += upMovement;
    }

    bool WASDkeyDown()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
        {
            return true;
        }
        else 
        {
            return false;
        }
    }
}
