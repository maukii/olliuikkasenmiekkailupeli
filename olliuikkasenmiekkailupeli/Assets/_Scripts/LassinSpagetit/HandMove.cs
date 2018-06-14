using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandMove : MonoBehaviour {
    Quaternion defaultRotation;
    Vector3 defaultPosition;
    Quaternion insideRotation;
    public Vector3 insidePosition;
    Transform hanging;
    Transform thrust;
    Transform down;
    GameObject Inside;
    public Transform Outside;
    public float progress = 0;
    public float Speed = 1;
    public bool isInside;
    public bool swap;
    Quaternion hangingSwapRot;
    Quaternion downSwapRot;
    Vector3 hangingSwapPos;
    Vector3 downSwapPos;
    // Use this for initialization
    void Start () {
        isInside = true;
        thrust = GameObject.Find("thrust").transform;
        Inside = GameObject.Find("inside");
        insidePosition = Inside.transform.localPosition;
        insideRotation = Inside.transform.localRotation;
        Outside = GameObject.Find("outside").transform;
        defaultRotation = transform.localRotation;
        defaultPosition = transform.localPosition;
        hanging = GameObject.Find("hanging").transform;
        down = GameObject.Find("down").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetAxis("Vertical") >= 0)
        {
            transform.localRotation = Quaternion.Slerp(defaultRotation, hanging.localRotation, Input.GetAxis("Vertical"));
            transform.localPosition = Vector3.Lerp(defaultPosition, hanging.localPosition, Input.GetAxis("Vertical"));
        }
        else if (Input.GetAxis("Vertical") <= 0)
        {
            transform.localRotation = Quaternion.Slerp(defaultRotation, down.localRotation, -Input.GetAxis("Vertical"));
            transform.localPosition = Vector3.Lerp(defaultPosition, down.localPosition, -Input.GetAxis("Vertical"));
        }
        //if(Input.GetAxis("Horizontal") >= 0)
        //{
        //    transform.localRotation = Quaternion.Slerp(defaultRotation, thrust.localRotation, Input.GetAxis("Horizontal"));
        //    transform.localPosition = Vector3.Lerp(defaultPosition, thrust.localPosition, Input.GetAxis("Horizontal"));
        //}
        if (Input.GetButton("Fire1") && progress == 0)
        {
            Swap();
            hangingSwapRot = hanging.localRotation;
            downSwapRot = down.localRotation;
            hangingSwapPos = hanging.localPosition;
            downSwapPos = down.localPosition;
        }
        if(swap == true)
        {
            progress = progress + Time.deltaTime * Speed;
            if (!isInside)
            {
                Inside.transform.localRotation = Quaternion.Slerp(insideRotation, Outside.localRotation, progress);
                Inside.transform.localPosition = Vector3.Lerp(insidePosition, Outside.localPosition, progress);
                hanging.localRotation = Quaternion.Slerp(hangingSwapRot, Quaternion.Euler(-hangingSwapRot.eulerAngles.x, hangingSwapRot.eulerAngles.y, hangingSwapRot.eulerAngles.z), progress);
                down.localRotation = Quaternion.Slerp(downSwapRot, Quaternion.Euler(-downSwapRot.eulerAngles.x, downSwapRot.eulerAngles.y, downSwapRot.eulerAngles.z), progress);
                hanging.localPosition = Vector3.Lerp(hangingSwapPos, new Vector3(hangingSwapPos.x, -hangingSwapPos.y, -hangingSwapPos.z), progress);
                down.localPosition = Vector3.Lerp(downSwapPos, new Vector3(downSwapPos.x, -downSwapPos.y, -downSwapPos.z), progress);

            }
            else
            {
                Inside.transform.localRotation = Quaternion.Slerp(Outside.localRotation, insideRotation, progress);
                Inside.transform.localPosition = Vector3.Lerp(Outside.localPosition, insidePosition, progress);
                hanging.localRotation = Quaternion.Slerp(hangingSwapRot, Quaternion.Euler(-hangingSwapRot.eulerAngles.x, hangingSwapRot.eulerAngles.y, hangingSwapRot.eulerAngles.z), progress);
                down.localRotation = Quaternion.Slerp(downSwapRot, Quaternion.Euler(-downSwapRot.eulerAngles.x, downSwapRot.eulerAngles.y, downSwapRot.eulerAngles.z), progress);
                hanging.localPosition = Vector3.Lerp(hangingSwapPos, new Vector3(hangingSwapPos.x, -hangingSwapPos.y, -hangingSwapPos.z), progress);
                down.localPosition = Vector3.Lerp(downSwapPos, new Vector3(downSwapPos.x, -downSwapPos.y, -downSwapPos.z), progress);
            }
            Debug.Log(progress);
            if (progress >= 1)
            {
                progress = 0;
                swap = false;
            }
            
        }


    }
    void Swap()
    {
        if(isInside == true)
        {
            isInside = false;
            swap = true;
        }
        else
        {
            isInside = true;
            swap = true;
        }
    }
}
