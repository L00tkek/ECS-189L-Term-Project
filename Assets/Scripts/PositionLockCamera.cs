using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionLockCamera : MonoBehaviour
{
    [SerializeField] protected GameObject Target;
    private Camera managedCamera;
    private LineRenderer cameraLineRenderer;

    private void Awake()
    {
        managedCamera = gameObject.GetComponent<Camera>();
        cameraLineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    //Use the LateUpdate message to avoid setting the camera's position before
    //GameObject locations are finalized.
    void LateUpdate()
    {
        var targetPosition = this.Target.transform.position;
        var cameraPosition = managedCamera.transform.position;

        cameraPosition.x = targetPosition.x;
        cameraPosition.y = targetPosition.y;

        managedCamera.transform.position = cameraPosition;
    }
}
