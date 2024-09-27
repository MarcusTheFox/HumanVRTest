using UnityEngine;
using Valve.VR;

public class SmoothTurn : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Vector2 turnAction;
    [SerializeField] private float turnSpeed = 45.0f;

    void Update()
    {
        Vector2 turnAxis = turnAction.axis;

        if (Mathf.Abs(turnAxis.x) > 0.1f)
        {
            transform.parent.Rotate(Vector3.up, turnAxis.x * turnSpeed * Time.deltaTime);
        }
    }
}