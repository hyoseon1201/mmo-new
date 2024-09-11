using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public BaseObject Target { get; set; }

    private void Start()
    {
        Camera.main.orthographicSize = 6;
    }

    private void LateUpdate()
    {
        if (Target == null)
            return;

        transform.position = new Vector3(Target.transform.position.x, Target.transform.position.y, -10f);
    }
}

