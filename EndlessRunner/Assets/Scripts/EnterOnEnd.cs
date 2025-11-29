using UnityEngine;

public class EnterOnEnd : MonoBehaviour
{
    [SerializeField] private ObjectSpawner spawner;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TriggerReaction>() != null && spawner != null)
        {
            spawner.OnReturnToPoolFromOutside(other.GetComponent<TriggerReaction>());
        }
    }
}