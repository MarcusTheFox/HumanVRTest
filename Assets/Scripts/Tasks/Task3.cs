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
        ShowCompleteText();
        boxTarget.gameObject.SetActive(false);
        StartCoroutine(LoadNextLevel());
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
        else if (counter - countWithSmoothTurn >= countWithNormalTeleportTurn)
        {
            snapTurn.fadeScreen = false;
            snapTurn.showTurnAnimation = false;
        }
        else if (counter >= countWithSmoothTurn)
        {
            smoothTurn.enabled = false;
            snapTurn.enabled = true;
        }
    }
    
    private void DecreaseCounter()
    {
        UpdateText(--counter);
    }
}
