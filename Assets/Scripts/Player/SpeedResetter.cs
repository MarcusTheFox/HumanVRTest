using UnityEngine;
using Valve.VR;

public class SpeedResetter : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean resetSpeedAction;
    [SerializeField] private SpeedMeter speedMeter;

    private void Update()
    {
        if (resetSpeedAction.stateDown)
        {
            Debug.Log("Reset speed");
            speedMeter.ResetSpeed();
        }
    }
}
