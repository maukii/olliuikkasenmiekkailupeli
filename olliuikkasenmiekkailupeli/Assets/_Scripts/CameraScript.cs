using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform P1, P2;
    public float zoomIn, zoomOut;
    public float distanceBetweenPlayers;
    public static CameraScript cam;

    private const float DISTANCE_MARGIN = 1.0f;

    private Vector3 middlePoint, origCameraPos;
    private float distanceFromMiddlePoint;
    private float cameraDistance;
    private float aspectRatio;
    private float fov;
    private float tanFov;

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










    /*public Transform f1, f2;
    
    float margin = 1.5f;
    private float xL, xR, width, wScene;

    Vector3 origCamPos;

    void Start ()
    {
        origCamPos = Camera.main.transform.position;

        CalcScreen();
        wScene = xR - xL;
    }
	
	void Update ()
    {
        Vector3 newCamPos = new Vector3(transform.position.x, transform.position.y, -17.5f);
        
        CalcScreen();
        width = xR - xL;

        if (width > wScene)
        {
            transform.position = newCamPos;
        }

        if (width < wScene)
        {
            transform.position = origCamPos;
        }
    }

    void CalcScreen()
    {
        if (f1.position.x < f2.position.x)
        {
            xL = f1.position.x - margin;
            xR = f2.position.x + margin;
        }

        else
        {
            xL = f2.position.x - margin;
            xR = f1.position.x + margin;
        }
    }*/





    /*public Transform p1, p2;

    float margin = 1.5f; // space between screen border and nearest fighter

    private float z0 = 0; // coord z of the fighters plane
    private float zCam; // camera distance to the fighters plane
    private float wScene; // scene width
    private float xL; // left screen X coordinate
    private float xR; // right screen X coordinate

    void Start()
    {
        // initializes scene size and camera distance
        CalcScreen();
        wScene = xR - xL;
        zCam = transform.position.z - z0;
    }

    void Update()
    {
        Vector3 cam = transform.position;

        CalcScreen();
        float width = xR - xL;

        if (width > wScene)
        {
            // if fighters too far adjust camera distance
            cam.z = zCam * width / wScene + z0;
        }
        // centers the camera
        cam.x = (xR + xL) / 2;
    }

    void CalcScreen()
    {
        // Calculates the xL and xR screen coordinates 
        if (p1.position.x < p2.position.x)
        {
            xL = p1.position.x - margin;
            xR = p2.position.x + margin;
        }
        else
        {
            xL = p2.position.x - margin;
            xR = p1.position.x + margin;
        }
    }*/
}
