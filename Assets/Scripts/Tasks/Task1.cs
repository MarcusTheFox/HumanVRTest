using System.Collections;
using UnityEngine;
using Valve.VR;

public class Task1 : TaskBasic
{
    public SteamVR_Action_Boolean completeButton;

    public override void OnComplete(TaskController taskController)
    {
        if (isCompleted) return;
        isCompleted = true;

        TaskLogger.LogEvent(GetType().Name, TaskLogger.EventType.Completed, "Task completed", startTime);

        ShowCompleteText();
        enabled = false;

        StartCoroutine(Complete(taskController));
    }

    private IEnumerator Complete(TaskController taskController)
    {
        yield return new WaitForSeconds(1);
        taskController.NextTask();
    }

    public override void OnExit(TaskController taskController)
    {
        base.OnExit(taskController);
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
