using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFocusCameraController : MonoBehaviour
{
    [SerializeField] protected GameObject Target;
    // The duration between when the target stops moving and when the camera begins to return to the target.
    [SerializeField] float idleDuration;
    // The speed with which the camera returns to the target.
    [SerializeField] float returnSpeed;
    // The multiplier applied to the target's speed that the camera moves toward the direction of the input.
    [SerializeField] float leadSpeedMultiplier;
    // The maximum distance between the camera and the target in the x and y plane.Do not include z plane values in this distance calculation.
    [SerializeField] float leadMaxDistance;


    private Camera managedCamera;
    private LineRenderer cameraLineRenderer;

    // This float will be used to track the time that the player has remained idle so that the camera
    // can start returning. 
    [SerializeField] float idleTimer;
    // This float tracks the distance ahead of the player that the camera must move. 
    float leadDistance;

    private void Awake()
    {
        managedCamera = gameObject.GetComponent<Camera>();
    }

    //Use the LateUpdate message to avoid setting the camera's position before
    //GameObject locations are finalized.
    void LateUpdate()
    {
        /*This stage requires you to create a variant of the position-lock 
        focus-smoothing controller.The variation is that the center of the 
        camera leads the target in the direction of the target's movement. 
        Te position of the camera should move ahead of the target. This 
        controller should update when movement input is given. When the 
        target is not moving, the camera should not move until IdleDuration 
        seconds have elapsed and should move toward the target with returnSpeed. 
        The camera should not exceed leadMaxDistance from the target.*/
        var playerPosition = this.Target.transform.position;
        var cameraPosition = managedCamera.transform.position;
        // because the player and camera have different z values, I make a new vector with a zeroed-out
        // z value
        var subtracted = playerPosition - cameraPosition;
        var distance = new Vector3(subtracted.x, subtracted.y, 0);

        var playerController = this.Target.GetComponent<PlayerController>();
        var playerVelocity2D = playerController.GetVelocity();
        var playerVelocity = new Vector3(playerVelocity2D.x, playerVelocity2D.y, 0);
        var playerDirection = playerVelocity.normalized;
        var playerSpeed = playerVelocity.magnitude;
        // var playerVelocity = playerController.GetCurrentSpeed() * playerController.GetMovementDirection();

        // idleTimer = playerDirection.magnitude;

        if (playerDirection.magnitude == 0)
        {
            if (idleTimer > idleDuration)
            {
                var toMove = distance.normalized * returnSpeed * Time.deltaTime;
                if (toMove.magnitude > distance.magnitude)
                {
                    leadDistance = 0;
                    managedCamera.transform.position = new Vector3(playerPosition.x, playerPosition.y, cameraPosition.z);
                }
                else
                {
                    leadDistance -= toMove.magnitude;
                    // We use += here instead of -= because toMove is pointing towards the player.
                    // To move the camera towards the player, we have to add the vectors, not subtract them.
                    managedCamera.transform.position += toMove;
                }
                    
                //start moving back towards the player
            }
            idleTimer += Time.deltaTime;
        }
        else
        {
            // This is the simplest way to make sure the idle timer is zeroed whenever the player stops.
            idleTimer = 0;

            // In order to get the desired behavior, we don't want leadDistance to decrease until the player's
            // speed reaches 0, so we use the min and max functions to restrict leadDistance to be no less
            // than the previous leadDistance and no greater than the leadMaxDistance.
            leadDistance = Mathf.Max(leadDistance, leadSpeedMultiplier * playerDirection.magnitude * playerSpeed);
            leadDistance = Mathf.Min(leadDistance, leadMaxDistance);
            var leadVector = playerPosition + leadDistance * playerDirection.normalized;
            managedCamera.transform.position = new Vector3(leadVector.x, leadVector.y, cameraPosition.z);
        }
    }
}
