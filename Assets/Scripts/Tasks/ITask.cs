using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITask
{
    public void OnEnter(TaskController taskController);

    public void UpdateTask(TaskController taskController);

    public void OnComplete(TaskController taskController);

    public void OnExit(TaskController taskController);
}
