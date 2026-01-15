using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BallManager : MonoBehaviour
{
    [SerializeField] private bool returnToOriginalPos;
    [SerializeField] private HoopManager hoopManager;
    private Rigidbody rb;
    private Vector3 originalPos;
    private XRGrabInteractable grab;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalPos = GetComponent<Transform>().position;
        grab = GetComponent<XRGrabInteractable>();
        grab.selectExited.AddListener(OnRelease);
    }

    void Update()
    {
        if (returnToOriginalPos)
        {
            ResetPosition();
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        hoopManager.AddShot();
    }

    public void ResetPosition()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.position = originalPos;
        returnToOriginalPos = false;
    }
}
