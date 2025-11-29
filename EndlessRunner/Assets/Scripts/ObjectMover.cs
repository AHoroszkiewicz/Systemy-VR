using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector2 zRange;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 newPos = transform.position;
        newPos += new Vector3(0, 0, speed * Time.deltaTime);
        if (newPos.z < zRange.x)
        {
            newPos.z = zRange.y;
        }
        transform.position = newPos;
    }
}
