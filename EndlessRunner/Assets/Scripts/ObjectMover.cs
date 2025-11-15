using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    [SerializeField] private List<Transform> transforms = new List<Transform>();
    [SerializeField] private float speed = 2f;
    [SerializeField] private Vector2 zRange;

    void Start()
    {
        
    }

    void Update()
    {
        foreach (var t in transforms)
        {
            Vector3 newPos = t.position;
            newPos += new Vector3(0, 0, speed * Time.deltaTime);
            if (newPos.z < zRange.x)
            {
                newPos.z = zRange.y;
            }
            t.position = newPos;
        }
    }
}
