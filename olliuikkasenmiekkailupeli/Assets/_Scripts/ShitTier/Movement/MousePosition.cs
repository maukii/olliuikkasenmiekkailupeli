using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{

    [SerializeField]
    float mousePosY;

    void Update ()
    {
        mousePosY = Input.GetAxis("MouseY");
        
	}
}
