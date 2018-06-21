using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScriptOld : MonoBehaviour
{
    public Transform player1, player2;
    public float playerDistance;
    public static CameraScriptOld cam;

    private const float distanceMargin = 1.0f;
    private Vector3 middle;

    void Start()
    {
        cam = this;
    }

    void Update()
    {
        Vector3 vectorBetweenPlayers = player2.position - player1.position;
        playerDistance = vectorBetweenPlayers.magnitude;
    }
}