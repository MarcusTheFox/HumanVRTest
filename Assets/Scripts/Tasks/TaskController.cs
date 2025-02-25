using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskController : MonoBehaviour
{
    private ITask _currentTask;
    private int _taskIndex;
    private DateTime levelStartTime;
    
    public TimeSpan LevelTime => DateTime.Now - levelStartTime;
    public List<TaskBasic> tasks = new List<TaskBasic>();

    private void Start()
    {
        levelStartTime = DateTime.Now;
        ChangeTask(tasks[0]);
    }

    private void Update()
    {
        _currentTask.UpdateTask(this);
    }

    private void ChangeTask(ITask newTask)
    {
        if (_currentTask != null)
        {
            _currentTask.OnExit(this);
            TaskLogger.LogSeparator();
        }
        _currentTask = newTask;
        _currentTask?.OnEnter(this);
    }

    public void NextTask()
    {
        if (_taskIndex + 1 >= tasks.Count)
        {
            _currentTask.OnExit(this);
            TaskLogger.LogSeparator("=== Level End ===");
            TaskLogger.LogEvent("LevelEnd", "Level completed", LevelTime);
            enabled = false;
            return;
        }
        ChangeTask(tasks[++_taskIndex]);
    }
}
