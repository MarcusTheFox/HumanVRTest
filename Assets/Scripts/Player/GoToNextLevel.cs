using System;
using UnityEngine;
using Valve.VR;

public class GoToNextLevel : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    [SerializeField] private TMPro.TMP_Text canvasText;
    [SerializeField] private SteamVR_Action_Boolean nextLevelAction;
    [SerializeField] private float holdingTime = 8f;
    [SerializeField] private float holdingTimeWithMessage = 5f;
    private float _holdingTimer;
    private TaskController taskController;

    private void Start()
    {
        _holdingTimer = holdingTime;
        taskController = GetComponent<TaskController>();
    }

    private void Update()
    {
        if (nextLevelAction.state)
        {
            _holdingTimer -= Time.deltaTime;
            _holdingTimer = Math.Max(_holdingTimer, 0);
            
            if (_holdingTimer <= holdingTimeWithMessage)
            {
                canvas.SetActive(true);
                canvasText.text = "До перехода на следующий уровень:\n" + _holdingTimer.ToString("0");
                
                if (_holdingTimer == 0)
                {
                    if (taskController != null)
                    {
                        TimeSpan levelTime = taskController.LevelTime;
                        TaskLogger.LogSeparator("=== Level Skipped ===");
                        TaskLogger.LogEvent("NextLevel", "Player skipped to next level", levelTime);
                    }
                    Destroy(transform.gameObject); // destroy player
                    SceneLoader.LoadNextScene();
                }
            }
        }
        else
        {
            _holdingTimer = holdingTime;
            canvas.SetActive(false);
        }
    }
}
