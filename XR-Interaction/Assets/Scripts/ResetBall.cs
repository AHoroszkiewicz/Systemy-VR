using UnityEngine;

public class ResetBall : MonoBehaviour
{
    private Rigidbody rb;
    private Vector3 originalPos;
    [SerializeField] private bool returnToOriginalPos;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalPos = GetComponent<Transform>().position;
    }

    void Update()
    {
        if (returnToOriginalPos)
        {
            ResetPosition();
        }
    }

    public void ResetPosition()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = originalPos;
        returnToOriginalPos = false;
    }
}
