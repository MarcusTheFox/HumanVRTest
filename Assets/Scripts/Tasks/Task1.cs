using System.Collections;
using UnityEngine;
using Valve.VR;

public class Task1 : TaskBasic
{
    public SteamVR_Action_Boolean completeButton;

    public override void OnComplete(TaskController taskController)
    {
        ShowCompleteText();
        enabled = false;

        StartCoroutine(Complete(taskController));
    }

    private IEnumerator Complete(TaskController taskController)
    {
        yield return new WaitForSeconds(1);
        base.OnComplete(taskController);
    }

    public override void OnExit(TaskController taskController)
    {
        HideTaskScreen();
    }

    public override void UpdateTask(TaskController taskController)
    {
        if (completeButton.GetStateDown(SteamVR_Input_Sources.Any) || Input.GetKeyDown(KeyCode.Z))
        {
            OnComplete(taskController);
        }
    }
}
