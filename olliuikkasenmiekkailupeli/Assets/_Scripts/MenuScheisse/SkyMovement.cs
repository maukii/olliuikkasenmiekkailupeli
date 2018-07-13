using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyMovement : MonoBehaviour
{
    public GameObject sky;

    private Vector3 skyVec, skyNew;
    private float startTime, length, distance, fraction;
    private float speed = 1.5f;

	void Start ()
    {
        skyVec = sky.transform.position;
        skyNew = new Vector3(-74.5f, skyVec.y, skyVec.z);

        startTime = Time.time;
        length = Vector3.Distance(skyVec, skyNew);
    }

	void Update ()
    {
        distance = (Time.time - startTime) * speed;
        fraction = distance / length;
        sky.transform.position = Vector3.Lerp(skyVec, skyNew, fraction);
    }
}
