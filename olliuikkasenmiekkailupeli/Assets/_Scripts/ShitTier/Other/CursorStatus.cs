using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorStatus : MonoBehaviour
{
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
