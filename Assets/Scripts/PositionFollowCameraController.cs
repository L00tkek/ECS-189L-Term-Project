using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollowCameraController : MonoBehaviour
{
    [SerializeField] protected GameObject Target;
    // The fraction of the target's movement speed that is set to the camera's chasing speed.
    [SerializeField] float followSpeedFactor;
    //The camera should move at the same speed as the target when they are leashDsitance apart.
    [SerializeField] float leashDistance;
    // The camera should move at catchUpSpeed toward the target when the target is not moving.
    [SerializeField] float catchUpSpeed;

    private Camera managedCamera;
    private LineRenderer cameraLineRenderer;
    [SerializeField] public int crosshairRadius;

    private void Awake()
    {
        managedCamera = gameObject.GetComponent<Camera>();
        cameraLineRenderer = gameObject.GetComponent<LineRenderer>();
    }

    //Use the LateUpdate message to avoid setting the camera's position before
    //GameObject locations are finalized.
    void LateUpdate()
    {
        var playerPosition = this.Target.transform.position;
        var cameraPosition = managedCamera.transform.position;
        // because the player and camera have different z values, I make a new vector with a zeroed-out
        // z value
        var subtracted = playerPosition - cameraPosition;
        var distance = new Vector3(subtracted.x, subtracted.y, 0);

        var playerController = this.Target.GetComponent<PlayerController>();
        var playerVelocity = playerController.GetVelocity();


        // We multiply velocities by Time.deltaTime to get the distance to travel per function call. Otherwise,
        // we'd be dealing with stuttering issues.
        if (distance.magnitude != 0)
        {
            if (playerVelocity.magnitude == 0)
            {
                // Move toward the target at speed catchUpSpeed. if the "catch up" would overshoot the player,
                // then we simply snap to the player's position. This prevents a camera-stutter issue I was having before.
                var toMove = distance.normalized * catchUpSpeed * Time.deltaTime;
                if (toMove.magnitude > distance.magnitude)
                {
                    cameraPosition = new Vector3(playerPosition.x, playerPosition.y, cameraPosition.z);
                }
                else
                {
                    cameraPosition = cameraPosition + toMove;
                }
            }
            else if (distance.magnitude >= leashDistance)
            {
                //move toward the target at the target's speed
                cameraPosition = cameraPosition + (distance.normalized * playerVelocity.magnitude * Time.deltaTime);
            }
            else
            {
                //move toward the target at speed (playerVelocity * followSpeedFactor)
                cameraPosition = cameraPosition + (distance.normalized * playerVelocity.magnitude * followSpeedFactor * Time.deltaTime);
            }

            // here is where we actually move the camera
            managedCamera.transform.position = cameraPosition;
        }
    }
}