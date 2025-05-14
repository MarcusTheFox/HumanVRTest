using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class PlayerController : MonoBehaviour
{
    private const float DefaultSpeed = 2.0f;
    public static float speed = DefaultSpeed;
    
    public SteamVR_Action_Vector2 movement;

    public Transform cameraTransform;
    private CapsuleCollider capsuleCollider;

    private void Start()
    {
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    private void FixedUpdate()
    {
        Vector3 movementDir = Player.instance.hmdTransform.TransformDirection(new Vector3(movement.axis.x, 0, movement.axis.y));
        transform.position += Vector3.ProjectOnPlane(Time.deltaTime * movementDir * speed, Vector3.up);

        float distanceFromFloor = Vector3.Dot(cameraTransform.localPosition, Vector3.up);
        capsuleCollider.height = Mathf.Max(capsuleCollider.height, distanceFromFloor);

        capsuleCollider.center = cameraTransform.localPosition - 0.5f * distanceFromFloor * Vector3.up;
    }

    public static void ResetSpeed()
    {
        speed = DefaultSpeed;
    }
}
