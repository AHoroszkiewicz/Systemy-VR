using UnityEngine;
using UnityEngine.Events;

public class TriggerReaction : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player";
    public UnityEvent onEntered;
    public string poolID;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            Debug.Log(targetTag + " has entered the trigger zone.");
            HandleTriggerEnter(other);
        }
    }
    protected virtual void HandleTriggerEnter(Collider other)
    {
            onEntered?.Invoke();
    }
}
