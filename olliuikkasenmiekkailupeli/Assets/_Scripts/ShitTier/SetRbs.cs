using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetRbs : MonoBehaviour {

    Rigidbody[] rbs;
    public Rigidbody sholder;

	void Start ()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
	}
	
	public void ResetBodies()
    {
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
        }
        sholder.GetComponent<Rigidbody>().isKinematic = true;
    }
}
