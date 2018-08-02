using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDistance : MonoBehaviour
{

    CameraScript cs;

    [SerializeField] GameObject wallL, wallR;
    [SerializeField] Transform P1, P2;

    void Start()
    {
        cs = FindObjectOfType<CameraScript>();
    }

    void Update()
    {
        GetPlayerPosition();
        DistanceToBackWall();
    }

    private void DistanceToBackWall()
    {
        
    }

    private void GetPlayerPosition()
    {
        P1 = cs.P1;
        P2 = cs.P2;
    }
}
