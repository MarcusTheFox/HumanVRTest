using UnityEngine;

public class TaskBasic : MonoBehaviour, ITask
{
    [SerializeField] protected GameObject taskScreen;
    [SerializeField] protected TMPro.TextMeshPro completeText;
    
    public virtual void OnComplete(TaskController taskController)
    {
        taskController.NextTask();
    }

    public virtual void OnEnter(TaskController taskController)
    {
        ShowTaskScreen();
        HideCompleteText();
    }

    public virtual void OnExit(TaskController taskController)
    {
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

    protected void ShowCompleteText()
    {
        completeText.enabled = true;
    }

    protected void HideCompleteText()
    {
        completeText.enabled = false;
    }
}
