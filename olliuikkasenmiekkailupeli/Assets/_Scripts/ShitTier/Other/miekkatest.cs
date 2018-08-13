using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miekkatest : MonoBehaviour
{

    [SerializeField]
    Animator anim;

    [SerializeField]
    GameObject miekkaDrop;

    void Start()
    {
        //anims = GetComponentsInChildren<Animator>();
        //
        //for (int i = 0; i < anims.Length; i++)
        //{
        //    if (anims[i].enabled)
        //        anim = anims[i];
        //}

        anim = miekkaDrop.GetComponent<Animator>();

    }

	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            miekkaDrop.gameObject.SetActive(true);
            anim.SetTrigger("Drop");
        }
	}
}
