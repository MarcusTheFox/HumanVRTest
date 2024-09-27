using UnityEngine;
using Valve.VR;

public class Task2 : TaskBasic
{
    public SteamVR_Action_Boolean completeButton;

    public override void OnComplete(TaskController taskController)
    {
        ShowCompleteText();
        enabled = false;

        base.OnComplete(taskController);
    }

    public override void OnExit(TaskController taskController)
    {
    }

    public override void UpdateTask(TaskController taskController)
    {
        if (completeButton.GetState(SteamVR_Input_Sources.Any) || Input.GetKey(KeyCode.Z))
        {
            OnComplete(taskController);
        }
    }
}
