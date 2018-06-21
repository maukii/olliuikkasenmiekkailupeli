using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform P1, P2;
    public float zoomIn, zoomOut, distanceBetweenPlayers;
    public static CameraScript cam;

    private const float DISTANCE_MARGIN = 1.0f;
    private Vector3 middlePoint, origCameraPos;
    private float distanceFromMiddlePoint, cameraDistance, aspectRatio, fov, tanFov;

    void Start()
    {
        cam = this;

        aspectRatio = Screen.width / Screen.height;
        tanFov = Mathf.Tan(Mathf.Deg2Rad * Camera.main.fieldOfView / 2.0f);

        origCameraPos = Camera.main.transform.position;

        zoomIn = Camera.main.transform.position.z + 0.25f;
        zoomOut = Camera.main.transform.position.z - 3;
    }

    void Update()
    {
        // Position the camera in the center.
        Vector3 newCameraPos = Camera.main.transform.position;
        newCameraPos.x = middlePoint.x;
        Camera.main.transform.position = newCameraPos;

        // Find the middle point between players.
        Vector3 vectorBetweenPlayers = P2.position - P1.position;
        middlePoint = P1.position + 0.5f * vectorBetweenPlayers;

        // Calculate the new distance.
        distanceBetweenPlayers = vectorBetweenPlayers.magnitude;
        cameraDistance = (distanceBetweenPlayers / 2.0f / aspectRatio) / tanFov;

        // Set camera to new position.
        Vector3 dir = (Camera.main.transform.position - middlePoint).normalized;
        Camera.main.transform.position = middlePoint + dir * (cameraDistance + DISTANCE_MARGIN);
        
        Vector3 newCameraZ = new Vector3(newCameraPos.x, origCameraPos.y, Camera.main.transform.position.z);
        newCameraZ.z = Mathf.Clamp(newCameraZ.z, zoomOut, zoomIn);

        if (newCameraZ.z > zoomIn)
        {
            newCameraZ.z = zoomIn;
        }

        if (newCameraZ.z < zoomOut)
        {
            newCameraZ.z = zoomOut;
        }

        Camera.main.transform.position = newCameraZ;
    }
}
