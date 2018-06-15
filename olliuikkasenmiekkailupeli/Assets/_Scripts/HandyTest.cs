using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandyTest : MonoBehaviour {

    public float speed = 5f;
    public float rotSpeed = 2f;
    public float hor, ver;

    public GameObject visualizer;
    public GameObject hand, elbo, elbo2, shlolder;

    //public GameObject box, sword;

    public float divider = 5f;

    public bool elboRotMax;

    void Start()
    {
        visualizer.transform.position = hand.transform.position;
    }

    void Update ()
    {

        //sword.transform.LookAt(box.transform);

        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(hor, ver, 0).normalized;

        Vector3 targetPos = visualizer.transform.position + moveDirection / divider;

        float speed = 5f;

        //hand.transform.position = Vector3.MoveTowards(hand.transform.position, targetPos, speed * Time.deltaTime);
        hand.transform.position = Vector3.Lerp(hand.transform.position, targetPos, speed * Time.deltaTime);

        if(ver >= 1f && !elboRotMax)
        {
            elbo2.transform.Rotate(new Vector3(-1, 1, -1) * rotSpeed);
        }
        if(ver <= -1f && !elboRotMax)
        {
            elbo2.transform.Rotate(new Vector3(1, -1, 1) * rotSpeed);
        }
        if(hor >= 1f && !elboRotMax)
        {
            // rot forwards
            elbo.transform.Rotate(new Vector3(-1, -1, 1) * rotSpeed);
        }
        if(hor <= -1f && !elboRotMax)
        {
            elbo.transform.Rotate(new Vector3(1, 1, -1) * rotSpeed);
        }

    }
}
