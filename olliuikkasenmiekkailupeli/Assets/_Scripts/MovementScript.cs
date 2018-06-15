using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour {

    Animator anim;
    public GameObject wrist,blö,blä;

    public bool moving;
    public float timer = 1f;

    void Start ()
    {
        anim = GetComponent<Animator>();
	}

    void Update ()
    {
        //if(moving == true)
        //{
        //    wrist.GetComponent<HandController>().enabled = false;
        //    timer -= Time.deltaTime;
        //    if (timer <= 0f)
        //    {
        //        wrist.GetComponent<HandController>().enabled = true;
        //        moving = false;
        //        timer = 1f;
        //    }
        //}

        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            moving = true;
            anim.SetTrigger("Step");

            // lerp 1 meter right
            Vector2.Lerp(gameObject.transform.position, new Vector2(gameObject.transform.position.x + 1f, gameObject.transform.position.y), 1f);
            Debug.Log("Step front");
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Debug.Log("Step back");
            anim.SetTrigger("StepBack");

            // lerp 1 meter left
            Vector2.Lerp(gameObject.transform.position, new Vector2(gameObject.transform.position.x - 1f, gameObject.transform.position.y), 1f);
        }
    }
}
