using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FPSCounter : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshPro fpsText;
    private float fps;

    private IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
                fps = 1.0f / Time.deltaTime;

            if (fpsText != null)
            {
                fpsText.text = $"FPS\n{fps:F0}";
            }

            Debug.Log($"Current FPS: {fps:F0}");
        }
    }
}
