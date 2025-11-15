using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private InputActionReference actionReference;
    InputAction action;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float currentMoveXValue;
    [SerializeField] private Vector2 xRange;

    void Start()
    {
        action = actionReference.action;
        action.performed += ActionPerformed;
        action.canceled += ActionCanceled;
    }

    private void ActionCanceled(InputAction.CallbackContext context)
    {
        currentMoveXValue = 0f;
    }

    private void ActionPerformed(InputAction.CallbackContext context)
    {
        Vector2 val = context.ReadValue<Vector2>();
        currentMoveXValue = val.x;
    }

    void Update()
    {
        if (currentMoveXValue != 0f)
        {
            Vector3 newPos = transform.position;
            newPos += new Vector3(currentMoveXValue * speed * Time.deltaTime, 0f, 0f);
            newPos.x = Mathf.Clamp(newPos.x, xRange.x, xRange.y);
            transform.position = newPos;
        }
    }
}
