using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static void LoadNextScene()
    {
        // Загружаем следующую сцену по индексу, увеличив текущий на 1
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        // Проверка, существует ли следующая сцена
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            Debug.Log("This is the last level!");
        }
    }

}
