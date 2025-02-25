using System;
using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Task3 : TaskBasic
{
    [SerializeField] private TMPro.TextMeshPro countText;
    [SerializeField] private int maxBoxes = 3;
    [SerializeField] private BoxTarget boxTarget;
    [Space] 
    [SerializeField] private int countWithSmoothTurn = 1;
    [SerializeField] private int countWithNormalTeleportTurn = 1;
    [Space] 
    [SerializeField] private SnapTurn snapTurn;
    [SerializeField] private SmoothTurn smoothTurn;
    private TaskController controller;
    private GameObject player;
    
    private int counter = 0;
    private int maxCounter = 0;

    public override void OnEnter(TaskController taskController)
    {
        player = snapTurn.transform.parent.gameObject;
        base.OnEnter(taskController);
        boxTarget.gameObject.SetActive(true);
        
        controller = taskController;
        
        UpdateText(counter);
        
        boxTarget.OnBoxEnter.AddListener(IncreaseCounter);
        boxTarget.OnBoxExit.AddListener(DecreaseCounter);

        snapTurn.fadeScreen = true;
        snapTurn.showTurnAnimation = true;
        snapTurn.enabled = false;
        smoothTurn.enabled = true;
    }

    public override void OnComplete(TaskController taskController)
    {
        base.OnComplete(taskController);
        ShowCompleteText();
        boxTarget.gameObject.SetActive(false);
        StartCoroutine(LoadNextLevel());
    }

    public override void OnPartialComplete(TaskController taskController, string logMessage = "")
    {
        int tempMaxCounter = Math.Max(maxCounter, counter);
        if (tempMaxCounter > maxCounter)
        {
            maxCounter = tempMaxCounter;
            base.OnPartialComplete(taskController, $"Delivered {maxCounter} box{(counter > 1 ? "es" : "")}");
            
            Debug.LogWarning($"{maxCounter} - {tempMaxCounter} - {counter}");
            
            Debug.LogWarning($"{countWithSmoothTurn} - {countWithNormalTeleportTurn} - maxCounter >= countWithSmoothTurn {maxCounter >= countWithSmoothTurn} - ");
        } 

        if (maxCounter - countWithSmoothTurn >= countWithNormalTeleportTurn)
        {
            snapTurn.fadeScreen = false;
            snapTurn.showTurnAnimation = false;
        }
        else if (maxCounter >= countWithSmoothTurn)
        {
            smoothTurn.enabled = false;
            snapTurn.enabled = true;
        }
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(5);
        Destroy(player);
        SceneLoader.LoadNextScene();
    }

    private void UpdateText(int count)
    {
        countText.SetText($"({count}/{maxBoxes})");
    }

    private void IncreaseCounter()
    {
        UpdateText(++counter);
        if (counter >= maxBoxes)
        {
            OnComplete(controller);
        }
        else 
        {
            OnPartialComplete(controller);
        }
    }
    
    private void DecreaseCounter()
    {
        UpdateText(--counter);
    }
}
