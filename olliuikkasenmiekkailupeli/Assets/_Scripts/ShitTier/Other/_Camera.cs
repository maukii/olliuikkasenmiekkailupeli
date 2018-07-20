using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class _Camera : MonoBehaviour
{
    public List<GameObject> targets;

    public Vector3 offset;
    private Vector3 offsetReset;
    public float smoothTime = .5f;
    public float maxSpeed = 5f;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private Vector3 velocity = Vector3.zero;
    private Camera cam;

    public float distanceX;


    void Start()
    {
        offsetReset = offset;
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        while (targets.Contains(null))
        {
            print("null target ... removing");
            targets.Remove(null);
        }

        if (targets.Count == 0)
            return;

        Move();
        Zoom();

        distanceX = GetGreatestDistanceX();
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistanceX() / zoomLimiter);
        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();
        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime, maxSpeed, Time.deltaTime);
    }

    public float GetGreatestDistanceX()
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }
        return bounds.size.x;
    }

    public Vector3 GetCenterPoint()
    {
        var bounds = new Bounds(targets[0].transform.position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            bounds.Encapsulate(targets[i].transform.position);
        }
        return bounds.center;
    }
}