using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float hor, ver;
    public float moveSpeed;

    public GameObject pivot, target;

    Rigidbody rb;

    public Rigidbody wrist;
    public float maxHandDistance = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(hor * moveSpeed, rb.velocity.y);

        Move();
        Sword();
    }

    void Move()
    {
        if (hor > 0)
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
        }
        else if (hor < 0)
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * moveSpeed * 10, ForceMode.Impulse);
        }

    }

    // use different stances or make swordhand follow some point in object space (virtual joystick)
    void Sword()
    {
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z));
        Ray mousePos = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);
        float point = 0;

        if (plane.Raycast(mousePos, out point))
        {
            //Vector3 targetPos = new Vector3(mousePos.GetPoint(point).x, mousePos.GetPoint(point).y, gameObject.transform.position.z);
            //target.transform.position = targetPos;
        }
       
        //// hand at max range
        //if(Vector3.Distance(mousePos, rb.position) > maxHandDistance)
        //{
        //    //wrist.position = rb.position + (target.transform.position - rb.transform.position).normalized * maxHandDistance;
        //    Vector3.MoveTowards(wrist.transform.position, Input.mousePosition, Time.deltaTime);
        //}

        if (ver > 0)
        {
            // change stance to upper
        }
        else if(ver < 0)
        {
            // change stance to lower
        }
    }

}
