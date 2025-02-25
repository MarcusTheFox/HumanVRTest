using System;
using UnityEngine;
using Valve.VR;

public class GoToNextLevel : MonoBehaviour
{
    public GameObject canvas;
    public TMPro.TMP_Text canvasText;
    public SteamVR_Action_Boolean nextLevelAction;
    public float holdingTime = 8f;
    public float holdingTimeWithMessage = 5f;
    private float _holdingTimer;

    private void Start()
    {
        _holdingTimer = holdingTime;
    }

    void Update()
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
