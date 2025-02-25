using UnityEngine;
using System.IO;
using System;

public class TaskBasic : MonoBehaviour, ITask
{
    [SerializeField] protected GameObject taskScreen;
    [SerializeField] protected TMPro.TextMeshPro completeText;

    protected DateTime startTime;
    protected bool isCompleted;

    public virtual void OnComplete(TaskController taskController)
    {
        if (isCompleted) return;
        isCompleted = true;
        
        TaskLogger.LogEvent(GetType().Name, TaskLogger.EventType.Completed, "Task completed", startTime);
        taskController.NextTask();
    }

    public virtual void OnPartialComplete(TaskController taskController, string logMessage = "") 
    {
        TaskLogger.LogEvent(GetType().Name, TaskLogger.EventType.PartiallyCompleted, logMessage, startTime);
    }

    public virtual void OnEnter(TaskController taskController)
    {
        startTime = DateTime.Now;
        TaskLogger.LogEvent(GetType().Name, TaskLogger.EventType.Entered, "Task started", startTime);
        ShowTaskScreen();
        HideCompleteText();
    }

    public virtual void OnExit(TaskController taskController)
    {
        TaskLogger.LogEvent(GetType().Name, TaskLogger.EventType.Exited, "Task finished", startTime);
    }

    public virtual void UpdateTask(TaskController taskController)
    {
    }

    protected void ShowTaskScreen()
    {
        taskScreen.SetActive(true);
    }

    protected void HideTaskScreen()
    {
        taskScreen.SetActive(false);
    }

    protected void ChangeTaskScreenText(string newText)
    {
        taskScreen.transform.GetChild(1).GetComponent<TMPro.TextMeshPro>().SetText(newText);
    }

    protected void ShowCompleteText()
    {
        completeText.enabled = true;
    }

    protected void HideCompleteText()
    {
        completeText.enabled = false;
    }
}
