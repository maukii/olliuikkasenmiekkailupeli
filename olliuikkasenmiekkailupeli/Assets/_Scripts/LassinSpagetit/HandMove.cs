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
    public Transform windUp1;
    Transform slash1;
    GameObject Inside;
    public Transform Outside;
    public float swapProgress = 0;
    public float swingProgress = 0;
    public float Speed = 1;
    public bool isInside;
    public bool swap;
    Quaternion hangingSwapRot;
    Quaternion downSwapRot;
    Vector3 hangingSwapPos;
    Vector3 downSwapPos;
    enum Stance {Hanging, Default, Down}
    Stance stance;
    bool swing;
    public bool altInput;
    public bool otherAltInput;
    float altInputvalue = 0;
    float altInputSpeed;
    // Use this for initialization
    void Start () {
        swing = false;
        stance = Stance.Default;
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
        windUp1 = GameObject.Find("windup1").transform;
        slash1 = GameObject.Find("slash1").transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (swing)
        {
            SwordSwing();
        }
        else
        {

            if (altInput)
            {
                AltUpdateStance();
            }
            else if (otherAltInput)
            {
                OtherAltUpdateStance();
            }
            else
            {
                UpdateStance();
            }
            

            if (Input.GetButton("Fire2"))
            {
                swing = true;
                Swap();
                swap = false;
                hangingSwapRot = hanging.localRotation;
                downSwapRot = down.localRotation;
                hangingSwapPos = hanging.localPosition;
                downSwapPos = down.localPosition;
            }


            if (Input.GetButton("Fire1") && swapProgress == 0)
            {
                Swap();
                hangingSwapRot = hanging.localRotation;
                downSwapRot = down.localRotation;
                hangingSwapPos = hanging.localPosition;
                downSwapPos = down.localPosition;
            }
            if (swap == true)
            {
                swapProgress = swapProgress + Time.deltaTime * Speed;
                if (!isInside)
                {
                    if(stance == Stance.Default)
                    {

                    }
                    else if(stance == Stance.Hanging)
                    {

                    }
                    else if (stance == Stance.Down)
                    {

                    }
                    Inside.transform.localRotation = Quaternion.Slerp(insideRotation, Outside.localRotation, swapProgress);
                    Inside.transform.localPosition = Vector3.Lerp(insidePosition, Outside.localPosition, swapProgress);
                    hanging.localRotation = Quaternion.Slerp(hangingSwapRot, Quaternion.Euler(-hangingSwapRot.eulerAngles.x, hangingSwapRot.eulerAngles.y, hangingSwapRot.eulerAngles.z), swapProgress);
                    down.localRotation = Quaternion.Slerp(downSwapRot, Quaternion.Euler(-downSwapRot.eulerAngles.x, downSwapRot.eulerAngles.y, downSwapRot.eulerAngles.z), swapProgress);
                    hanging.localPosition = Vector3.Lerp(hangingSwapPos, new Vector3(hangingSwapPos.x, -hangingSwapPos.y, -hangingSwapPos.z), swapProgress);
                    down.localPosition = Vector3.Lerp(downSwapPos, new Vector3(downSwapPos.x, -downSwapPos.y, -downSwapPos.z), swapProgress);

                }
                else
                {
                    Inside.transform.localRotation = Quaternion.Slerp(Outside.localRotation, insideRotation, swapProgress);
                    Inside.transform.localPosition = Vector3.Lerp(Outside.localPosition, insidePosition, swapProgress);
                    hanging.localRotation = Quaternion.Slerp(hangingSwapRot, Quaternion.Euler(-hangingSwapRot.eulerAngles.x, hangingSwapRot.eulerAngles.y, hangingSwapRot.eulerAngles.z), swapProgress);
                    down.localRotation = Quaternion.Slerp(downSwapRot, Quaternion.Euler(-downSwapRot.eulerAngles.x, downSwapRot.eulerAngles.y, downSwapRot.eulerAngles.z), swapProgress);
                    hanging.localPosition = Vector3.Lerp(hangingSwapPos, new Vector3(hangingSwapPos.x, -hangingSwapPos.y, -hangingSwapPos.z), swapProgress);
                    down.localPosition = Vector3.Lerp(downSwapPos, new Vector3(downSwapPos.x, -downSwapPos.y, -downSwapPos.z), swapProgress);
                }
                if (swapProgress >= 1)
                {
                    swapProgress = 0;
                    swap = false;
                }

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
    void SwordSwing()
    {
        swingProgress = swingProgress + Time.deltaTime * Speed;
        if (!isInside)
        {

            //transform.localPosition = Vector3.Lerp(hanging.localPosition, down.localPosition, progress-1);
            //transform.localRotation = Quaternion.Slerp(hanging.localRotation, down.localRotation, progress-1);
            
            transform.localRotation = Quaternion.Slerp(transform.localRotation, windUp1.localRotation, swingProgress);
            transform.localPosition = Vector3.Lerp(transform.localPosition, windUp1.localPosition, swingProgress);
            if (swingProgress > 1)
            {
                Swap();
                
                
            }
            //hanging.localRotation = Quaternion.Slerp(hangingSwapRot, Quaternion.Euler(-hangingSwapRot.eulerAngles.x, hangingSwapRot.eulerAngles.y, hangingSwapRot.eulerAngles.z), progress-1);
            //down.localRotation = Quaternion.Slerp(downSwapRot, Quaternion.Euler(-downSwapRot.eulerAngles.x, downSwapRot.eulerAngles.y, downSwapRot.eulerAngles.z), progress-1);
            //hanging.localPosition = Vector3.Lerp(hangingSwapPos, new Vector3(hangingSwapPos.x, -hangingSwapPos.y, -hangingSwapPos.z), progress-1);
            //down.localPosition = Vector3.Lerp(downSwapPos, new Vector3(downSwapPos.x, -downSwapPos.y, -downSwapPos.z), progress-1);

        }
        else
        {
            transform.localRotation = Quaternion.Slerp(windUp1.localRotation, slash1.localRotation, swingProgress - 1);
            transform.localPosition = Vector3.Lerp(windUp1.localPosition, slash1.localPosition, swingProgress - 1);
        }
        if (swingProgress >= 2)
        {
            swingProgress = 0;
            swing = false;
            if (otherAltInput)
            {
                altInputvalue = -1;
            }
        }
    }
    void SwordRotation()
    {

    }
    void UpdateStance()
    {
        Quaternion verRot = Quaternion.identity, horRot = Quaternion.identity;
        Vector3 verPos = Vector3.zero, horPos = Vector3.zero;
        if (Input.GetAxis("Vertical") >= 0)
        {
            stance = Stance.Hanging;
            verRot = Quaternion.Slerp(defaultRotation, hanging.localRotation, Input.GetAxis("Vertical"));
            verPos = Vector3.Lerp(defaultPosition, hanging.localPosition, Input.GetAxis("Vertical"));
        }
        else if (Input.GetAxis("Vertical") <= 0)
        {
            stance = Stance.Down;
            verRot = Quaternion.Slerp(defaultRotation, down.localRotation, -Input.GetAxis("Vertical"));
            verPos = Vector3.Lerp(defaultPosition, down.localPosition, -Input.GetAxis("Vertical"));
        }
        if (Input.GetAxis("Horizontal") >= 0)
        {
            horRot = Quaternion.Slerp(defaultRotation, thrust.localRotation, Input.GetAxis("Horizontal"));
            horPos = Vector3.Lerp(defaultPosition, thrust.localPosition, Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            stance = Stance.Default;
        }
        //transform.localRotation = Quaternion.Slerp(verRot, horRot, 0.5f);
        //transform.localPosition = Vector3.Lerp(verPos*2, horPos*2, 0.5f);
        transform.localRotation = verRot;
        transform.localPosition = verPos;
    }
    void AltUpdateStance()
    {
        Quaternion verRot = Quaternion.identity, horRot = Quaternion.identity;
        Vector3 verPos = Vector3.zero, horPos = Vector3.zero;
        if(Input.GetAxis("Vertical") != 0)
        {
            altInputvalue += altInputSpeed * Input.GetAxis("Vertical");
        }
        if(altInputvalue > 1)
        {
            altInputvalue = 1;
        }
        else if(altInputvalue < -1)
        {
            altInputvalue = -1;
        }
        if (altInputvalue > 0)
        {
            stance = Stance.Hanging;
            verRot = Quaternion.Slerp(defaultRotation, hanging.localRotation, altInputvalue);
            verPos = Vector3.Lerp(defaultPosition, hanging.localPosition, altInputvalue);
        }
        else if (altInputvalue < 0)
        {
            stance = Stance.Down;
            verRot = Quaternion.Slerp(defaultRotation, down.localRotation, -altInputvalue);
            verPos = Vector3.Lerp(defaultPosition, down.localPosition, -altInputvalue);
        }
        if (Input.GetAxis("Horizontal") >= 0)
        {
            horRot = Quaternion.Slerp(defaultRotation, thrust.localRotation, Input.GetAxis("Horizontal"));
            horPos = Vector3.Lerp(defaultPosition, thrust.localPosition, Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            stance = Stance.Default;
        }
        
        //transform.localRotation = Quaternion.Slerp(verRot, horRot, 0.5f);
        //transform.localPosition = Vector3.Lerp(verPos*2, horPos*2, 0.5f);
        transform.localRotation = verRot;
        transform.localPosition = verPos;
    }
    void OtherAltUpdateStance()
    {
        Quaternion verRot = Quaternion.identity, horRot = Quaternion.identity;
        Vector3 verPos = Vector3.zero, horPos = Vector3.zero;
        float smooth;
        if (Input.GetButton("Fire3"))
        {
            altInputvalue += 1;
        }
        if (Input.GetButton("Jump"))
        {
            altInputvalue += -1;
        }
        if (altInputvalue > 1)
        {
            altInputvalue = 1;
        }
        else if (altInputvalue < -1)
        {
            altInputvalue = -1;
        }
        if (altInputvalue > 0)
        {
            stance = Stance.Hanging;
            verRot = Quaternion.Slerp(defaultRotation, hanging.localRotation, altInputvalue);
            verPos = Vector3.Lerp(defaultPosition, hanging.localPosition, altInputvalue);
        }
        else if (altInputvalue < 0)
        {
            stance = Stance.Down;
            verRot = Quaternion.Slerp(defaultRotation, down.localRotation, -altInputvalue);
            verPos = Vector3.Lerp(defaultPosition, down.localPosition, -altInputvalue);
        }
        if (Input.GetAxis("Horizontal") >= 0)
        {
            horRot = Quaternion.Slerp(defaultRotation, thrust.localRotation, Input.GetAxis("Horizontal"));
            horPos = Vector3.Lerp(defaultPosition, thrust.localPosition, Input.GetAxis("Horizontal"));
        }
        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            stance = Stance.Default;
        }

        //transform.localRotation = Quaternion.Slerp(verRot, horRot, 0.5f);
        //transform.localPosition = Vector3.Lerp(verPos*2, horPos*2, 0.5f);
        transform.localRotation = verRot;
        transform.localPosition = verPos;
    }
}
