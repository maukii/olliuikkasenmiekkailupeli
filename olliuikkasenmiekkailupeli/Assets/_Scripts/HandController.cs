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


    //[Header("--- OLLIN SCRIPTI ALKAA ---")]

    //private float dist;
    //private Vector3 swordPos;
    //private Vector3 shoulderPos;
    //public GameObject shoulder;
    //public GameObject sword;
    //public float maxDist;
    //public float minDist;

    //public float hor, vel;

    //private float timer;
    //public int player;
    //public bool flipped;

    //void FixedUpdate()
    //{

    //    //swordPos.x += shoulder.transform.position.x - shoulderPos.x;
    //    //swordPos.y += shoulder.transform.position.y - shoulderPos.y;

    //    if (player == 1)
    //    {
    //        hor = Input.GetAxis("Horizontal");
    //        vel = Input.GetAxis("Vertical");
    //    }

    //    swordPos.x += hor;
    //    swordPos.y += vel;

    //    dist = Vector3.Distance(shoulder.transform.position, swordPos);
    //    if (dist > maxDist)
    //    {
    //        swordPos = Vector3.MoveTowards(swordPos, shoulder.transform.position, dist - maxDist);
    //    }
    //    if (swordPos.x < shoulder.transform.position.x + minDist && !flipped)
    //    {
    //        swordPos.x = shoulder.transform.position.x + minDist;
    //    }
    //    if (swordPos.x > shoulder.transform.position.x + minDist && flipped)
    //    {
    //        swordPos.x = shoulder.transform.position.x - minDist;
    //    }

    //    sword.transform.position = swordPos;
    //    shoulderPos = shoulder.transform.position;


    //    if(Input.GetKeyDown(KeyCode.Space))
    //    {
    //        ChangeStance();
    //    }
    //}

    //void ChangeStance()
    //{
    //    if(GetComponent<Rigidbody>().isKinematic)
    //    {
    //        GetComponent<Rigidbody>().isKinematic = false;
    //        maxDist = maxDist * 2;
    //    }
    //    else
    //    {
    //        GetComponent<Rigidbody>().isKinematic = true;
    //        maxDist = maxDist / 2;
    //    }
    //}

    public GameObject sholderPivot, elboPivot;
    public float rotSpeed = 10f;

    public float elboz;
    public float sholderz;

    Animator anim;

    public float angle;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        elboz = elboPivot.transform.rotation.z;
        sholderz = sholderPivot.transform.rotation.z;



        if(Input.GetKey(KeyCode.LeftArrow))
        {
            if (elboPivot.transform.rotation.z > 0.2f)
            {
                elboPivot.transform.Rotate(Vector3.back * rotSpeed * Time.deltaTime);
            }
        }

        if(Input.GetKey(KeyCode.RightArrow))
        {
            if (elboPivot.transform.rotation.z < -0.45)
            {
                elboPivot.transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (sholderPivot.transform.rotation.z <= 0f)
            {
                sholderPivot.transform.Rotate(Vector3.left * rotSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            if (sholderPivot.transform.rotation.z >= -0.45f)
            {
                sholderPivot.transform.Rotate(Vector3.right * rotSpeed * Time.deltaTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Step());
        }
    }

    IEnumerator Step()
    {
        anim.enabled = true;
        anim.SetTrigger("Step");
        yield return new WaitForSeconds(1.175f);
        anim.enabled = false;
    }

}
