using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Transform target;
    private void Update()
    {
        CameraFollow();
    }

    private void CameraFollow()
    {
        Vector3 temp = transform.position;
        temp.x = target.position.x;
        temp.y = -3.5f;
        transform.position = temp;
    }
}
