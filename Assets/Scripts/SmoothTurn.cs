using System;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SmoothTurn : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Vector2 turnAction;
    [SerializeField] private float turnSpeed = 45.0f;
    private Player player;

    private void Start()
    {
        player = Player.instance;
    }

    void Update()
    {
        Vector2 turnAxis = turnAction.axis;

        if (Mathf.Abs(turnAxis.x) > 0.1f)
        {
            float angle = turnAxis.x * turnSpeed * Time.deltaTime;
            Vector3 playerFeetOffset = player.trackingOriginTransform.position - player.feetPositionGuess;
            player.trackingOriginTransform.position -= playerFeetOffset;
            player.transform.Rotate(Vector3.up, angle);
            playerFeetOffset = Quaternion.Euler(0.0f, angle, 0.0f) * playerFeetOffset;
            player.trackingOriginTransform.position += playerFeetOffset;
        }
    }
}