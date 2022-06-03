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

    private void OnMouseExit()
    {
        CameraManager.Cam.SetCursorHoveringState(false);
    }
}
