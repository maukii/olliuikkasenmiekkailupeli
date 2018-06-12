using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

    public float hor;
    public float moveSpeed;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        hor = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(hor * moveSpeed, rb.velocity.y);

        Move();

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
}
