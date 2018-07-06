using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordScript : MonoBehaviour
{

    public float swordHitSpeed;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("we hit");

        if(collision.gameObject.GetComponent<BodypartHealth>() != null)
        {
            swordHitSpeed = collision.relativeVelocity.magnitude;
            collision.gameObject.GetComponent<BodypartHealth>().TakeDamage(swordHitSpeed);
        }
    }

}
