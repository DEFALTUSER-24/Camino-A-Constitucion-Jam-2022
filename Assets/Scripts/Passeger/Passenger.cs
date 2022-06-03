using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Passenger : MonoBehaviour
{
    private void OnMouseEnter()
    {
        CameraManager.Cam.SetCursorHoveringState(true);
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
            CandySpawner.instance.RequestACandy(this, CandyType.Alfajor);
        else if (Input.GetMouseButtonDown(1))
            CandySpawner.instance.RequestACandy(this, CandyType.Mantekel);
    }

    private void OnMouseExit()
    {
        CameraManager.Cam.SetCursorHoveringState(false);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, FlyWeightPointer.passenger.viewRadius);
    }
}