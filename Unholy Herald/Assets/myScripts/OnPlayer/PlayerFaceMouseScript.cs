using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceMouseScript : MonoBehaviour
{
    [HideInInspector] public PlayerUIScript playerUI = GameObject.FindWithTag("Player").GetComponent<PlayerUIScript>();

    Ray cameraRay;
    RaycastHit cameraRayHit;
    Vector3 mousePosition;

    void Update()
    {
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            if (cameraRayHit.transform.tag == "Geometry" || cameraRayHit.transform.root.tag == "Geometry" || cameraRayHit.transform.tag == "Foe" || cameraRayHit.transform.root.tag == "Foe")
            {
                if (playerUI.freezeTurn == false)
                {
                    mousePosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
                }
            }
            transform.LookAt(mousePosition);
        }
    }
}
