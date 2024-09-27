using UnityEngine;
using UnityEngine.Events;

public class BoxTarget : MonoBehaviour
{
    public UnityEvent OnBoxEnter;
    public UnityEvent OnBoxExit;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Box>())
        {
            OnBoxEnter?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Box>())
        {
            OnBoxExit?.Invoke();
        }
    }
}
