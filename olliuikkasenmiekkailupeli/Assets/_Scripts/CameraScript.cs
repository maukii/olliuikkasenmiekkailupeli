using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player1, player2;
    public float playerDistance;
    public static CameraScript cam;

    private const float distanceMargin = 1.0f;
    private Vector3 middle;
    private float aspectRatio, camDistance, tanFov;

    void Start()
    {
        cam = this;
        aspectRatio = Screen.width / Screen.height;
        tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);
    }

    void Update()
    {
        Vector3 newCamPos = Camera.main.transform.position;
        newCamPos.x = middle.x;                                                         //Kamera keskitetään
        Camera.main.transform.position = newCamPos;

        Vector3 vectorBetweenPlayers = player2.position - player1.position;             //Etsitään keskipiste pelaajien väliltä
        middle = player1.position + 0.5f * vectorBetweenPlayers;

        playerDistance = vectorBetweenPlayers.magnitude;                                //Lasketaan uusi distance
        camDistance = (playerDistance / 2 / aspectRatio) / tanFov;

        Vector3 dir = (Camera.main.transform.position - middle).normalized;             //Asetetaan kamera uuteen positioon
        Camera.main.transform.position = middle + dir * (camDistance + distanceMargin);
    }
}