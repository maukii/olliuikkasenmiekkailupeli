using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Handheightadjustment : MonoBehaviour {
    public float speed = 1;
    Vector3 up;
    public bool altInput;
	// Use this for initialization
	void Start () {

        up = new Vector3(0.005f, -0.0148f, -0.0042f);

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 verPos = Vector3.zero, horPos = Vector3.zero;
        if (Input.GetAxis("Vertical") >= 0)
        {
            if (altInput)
            {
                transform.Translate(Vector3.up * Input.GetAxis("Vertical") * speed * Time.deltaTime, Space.World);
            }
            else
            {
                verPos = Vector3.Lerp(new Vector3(0, 0, 0), up, Input.GetAxis("Vertical"));
            }
            
        

        }
        else if (Input.GetAxis("Vertical") <= 0)
        {
            if (altInput)
            {
                transform.Translate(Vector3.up * Input.GetAxis("Vertical") * speed * Time.deltaTime, Space.World);
            }
            else
            {
                verPos = Vector3.Lerp(new Vector3(0, 0, 0), -up, -Input.GetAxis("Vertical"));
            }
        }
        transform.localPosition = verPos;
        
    }
}
