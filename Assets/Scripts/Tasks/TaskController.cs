using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    private ITask _currentTask;
    private int _taskIndex;

    public List<TaskBasic> tasks = new List<TaskBasic>();

    private void Start()
    {
        ChangeTask(tasks[0]);
    }

    private void Update()
    {
        _currentTask.UpdateTask(this);
        Debug.Log(_currentTask);
    }

    private void ChangeTask(ITask newTask)
    {
        if (_currentTask != null)
        {
            _currentTask.OnExit(this);
        }
        _currentTask = newTask;
        _currentTask?.OnEnter(this);
    }

    public void NextTask()
    {
        if (_taskIndex + 1 >= tasks.Count)
        {
            _currentTask.OnExit(this);
            enabled = false;
            return;
        }
        ChangeTask(tasks[++_taskIndex]);
    }
}
