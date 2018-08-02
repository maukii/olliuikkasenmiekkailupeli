using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDistance : MonoBehaviour
{
    CameraScript cs;
    public float distance;
    float maxDistance = 10f;

    void Start()
    {
        cs = FindObjectOfType<CameraScript>();
    }

    void Update()
    {
        CanBackup();
    }

    public bool CanBackup()
    {
        distance = Vector3.Distance(cs.P1.position, cs.P2.position);

        if(distance >= maxDistance)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
