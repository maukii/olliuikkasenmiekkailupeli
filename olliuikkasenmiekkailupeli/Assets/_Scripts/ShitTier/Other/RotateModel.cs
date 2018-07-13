using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateModel : MonoBehaviour
{
    public Vector3 offsetRot = new Vector3(90, 0, 0);
    public Vector3 offsetPos = new Vector3(0, -1, 0);

    private void Awake()
    {
        transform.Rotate(offsetRot);
        transform.position += offsetPos;
    }

}
