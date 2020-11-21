using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFaceMouseScript : MonoBehaviour
{
    Ray cameraRay;
    RaycastHit cameraRayHit;

    void Update()
    {
        cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            Vector3 mousePosition = new Vector3(cameraRayHit.point.x, transform.position.y, cameraRayHit.point.z);
            transform.LookAt(mousePosition);
        }
    }
}
