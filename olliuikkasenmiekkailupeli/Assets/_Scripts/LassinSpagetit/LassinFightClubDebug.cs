using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LassinFightClubDebug : MonoBehaviour {
    AlternativeMovement5 am5;
    HandAnimationControl hac;
	// Use this for initialization
	void Start () {
        am5 = gameObject.GetComponent<AlternativeMovement5>();
        hac = gameObject.GetComponentInChildren<HandAnimationControl>();
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.name == "P1" && Input.GetKeyDown(KeyCode.Keypad1))
        {
            am5.enabled = !am5.enabled;
        }
        else if (gameObject.name == "P1.2" && Input.GetKeyDown(KeyCode.Keypad2))
        {
            am5.enabled = !am5.enabled;
        }
        if (gameObject.name == "P1" && Input.GetKeyDown(KeyCode.Keypad4))
        {
            hac.DEBUG_NoInput = !hac.DEBUG_NoInput;
        }
        else if (gameObject.name == "P1.2" && Input.GetKeyDown(KeyCode.Keypad5))
        {
            hac.DEBUG_NoInput = !hac.DEBUG_NoInput;
        }
    }
}
