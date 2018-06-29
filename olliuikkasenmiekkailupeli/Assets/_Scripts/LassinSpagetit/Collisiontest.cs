﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collisiontest : MonoBehaviour {
    public Transform SparkPrefab;
    

    Animator anim;
    HandAnimationControl hac;
    public float animSpeed;
    public bool collide;
	// Use this for initialization
	void Start () {
        hac = gameObject.GetComponent<HandAnimationControl>();
        anim = gameObject.GetComponent<Animator>();
        collide = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            
            SwordCollision();
        }
        if (collide)
        {
            
            if(animSpeed > 0)
            {
                animSpeed = 0;
            }
            //anim.speed = animSpeed;
            if(animSpeed == 0)
            {
                anim.SetBool("interrupt", false);
                collide = false;
                //anim.speed = hac.AnimSpeed;
            }
            animSpeed += Time.deltaTime;
        }

	}
    void SwordCollision()
    {
        animSpeed = -1;
        anim.SetBool("interrupt", true);
        collide = true;
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("mo");
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(SparkPrefab, pos, rot);
    }
}
