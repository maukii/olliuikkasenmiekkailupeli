using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Distances : MonoBehaviour
{

    CameraScript cs;

    [SerializeField] GameObject wallL, wallR;
    [SerializeField] Transform P1, P2;

    [SerializeField] float P1DistanceL, P1DistanceR, P2DistanceL, P2DistanceR;
    [SerializeField] float minDistance = 3.25f, maxDistance = 10f;
    [SerializeField] float playerDistance;
    [SerializeField] float extraDistance = 0.2f;

    void Start()
    {
        cs = FindObjectOfType<CameraScript>();
    }

    void Update()
    {
        GetPlayerPosition();
    }

    public bool CanBackUp(int playerNumber)
    {
        playerDistance = Vector3.Distance(P1.position, P2.position);

        P1DistanceL = Vector3.Distance(P1.position, wallL.transform.position);
        P1DistanceR = Vector3.Distance(P1.position, wallR.transform.position);

        P2DistanceL = Vector3.Distance(P2.position, wallL.transform.position);
        P2DistanceR = Vector3.Distance(P2.position, wallR.transform.position);

        if((P1DistanceL < minDistance + extraDistance || P1DistanceR < minDistance + extraDistance) && playerNumber == 1 || playerDistance >= maxDistance)
        {
            return false;
        }
        else if((P2DistanceL < minDistance + extraDistance || P2DistanceR < minDistance + extraDistance) && playerNumber == 2 || playerDistance >= maxDistance)
        {
            return false;
        }
        else
        {
            return true;
        }

    }

    private void GetPlayerPosition()
    {
        P1 = cs.P1;
        P2 = cs.P2;
    }

    public float GetPlayerDistance()
    {
        return Vector3.Distance(P1.position, P2.position);
    }
}
