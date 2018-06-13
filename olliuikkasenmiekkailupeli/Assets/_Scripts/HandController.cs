using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{

    //[SerializeField]
    //private GameObject wrist;

    //public float maxDistance = 1f;

    //Vector3 mousePosition;
    //Vector3 orginalPos;

    //[SerializeField]
    //Rigidbody rb;

    //private void Awake()
    //{
    //    orginalPos = wrist.transform.position;
    //}

    //void Start ()
    //{
    //    wrist = GameObject.FindWithTag("Wrist");
    //    rb = gameObject.transform.parent.parent.parent.parent.parent.parent.parent.parent.GetComponent<Rigidbody>(); // u w0t
    //}

    //void Update ()
    //{
    //    // take mouses position in 2D space
    //    //Vector3 mousePos = Camera.main.WorldToScreenPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, gameObject.transform.position.z));

    //    mousePosition = Input.mousePosition;

    //    if(Vector3.Distance(mousePosition, rb.position) > maxDistance)
    //    {
    //        Debug.Log(Vector3.Distance(mousePosition, rb.position));
    //        //wrist.transform.position = (rb.position + (mousePosition - rb.transform.position).normalized);
    //        Vector3.MoveTowards(wrist.transform.position, orginalPos, Time.deltaTime);
    //    }
    //    else
    //    {
    //        wrist.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, rb.transform.position.z * - 1));
    //    }

    //    //if(Vector3.Distance(mousePos, rb.position) > maxHandDistance)
    //    //{
    //    //    //wrist.position = rb.position + (target.transform.position - rb.transform.position).normalized * maxHandDistance;
    //    //    Vector3.MoveTowards(wrist.transform.position, Input.mousePosition, Time.deltaTime);
    //    //}

    //}

    private float velX;
    private float velY;
    private float targetVelX;
    private float targetVelY;
    public float velMultX;
    public float velMultY;
    public float accX;
    public float accY;
    private float swordX;
    private float swordY;
    private float dist;
    private Vector3 swordPos;
    private Vector3 shoulderPos;
    public GameObject shoulder;
    public GameObject sword;
    public float maxDist;
    public float minDist;
    private Vector3 collPos;
    private Vector3 vel;

    private float timer;
    public int player;
    public bool flipped;

    void Start()
    {
        velX = 0;
        velY = 0;
    }

    void FixedUpdate()
    {

        swordPos.x += shoulder.transform.position.x - shoulderPos.x;
        swordPos.y += shoulder.transform.position.y - shoulderPos.y;

        if (player == 1)
        {
            velX = Input.GetAxis("Horizontal") * velMultX;
            velY = Input.GetAxis("Vertical") * velMultY;
        }

        

        swordPos.x += velX;
        swordPos.y += velY;

        dist = Vector3.Distance(shoulder.transform.position, swordPos);
        if (dist > maxDist)
        {
            swordPos = Vector3.MoveTowards(swordPos, shoulder.transform.position, dist - maxDist);
        }
        if (swordPos.x < shoulder.transform.position.x + minDist && !flipped)
        {
            swordPos.x = shoulder.transform.position.x + minDist;
        }
        if (swordPos.x > shoulder.transform.position.x + minDist && flipped)
        {
            swordPos.x = shoulder.transform.position.x - minDist;
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeStance();
        }

        sword.transform.position = swordPos;
        shoulderPos = shoulder.transform.position;

    }

    void ChangeStance()
    {
        if(GetComponent<Rigidbody>().isKinematic)
        {
            GetComponent<Rigidbody>().isKinematic = false;
            maxDist = maxDist * 2;
        }
        else
        {
            GetComponent<Rigidbody>().isKinematic = true;
            maxDist = maxDist / 2;
        }
    }

}
