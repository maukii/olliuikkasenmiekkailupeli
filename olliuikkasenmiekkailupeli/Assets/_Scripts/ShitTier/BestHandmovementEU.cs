using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BestHandmovementEU : MonoBehaviour
{
    public float speed = 1f;
    public float power = 1;
    public GameObject hand, visualizer;

    Vector3 direction;
    Vector3 handStartPosition;

    [Header("--- Input values ---")]
    public float hor;
    public float ver;

    [Header("--- Maximum distances ---")]
    public float maxH;
    public float minH;
    public float maxV;
    public float minV;

    public Animator anim;

    public Rigidbody[] rbs;
    public GameObject ukko;

    void Start()
    {
        ukko = GameObject.Find("UkkoMies").gameObject;

        CheckPositions(); // TODO: if player takes step forward need to update hands start position and boundries
        CheckBoundries();

        rbs = ukko.GetComponentsInChildren<Rigidbody>();
    }

    void FixedUpdate()
    {
        TakeInput(); // TODO: disable parts of input while player is moving
        MoveHand();
        ClampTargetPosition();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            foreach (Rigidbody rb in rbs)
            {
                rb.isKinematic = true;
            }
            anim.SetTrigger("Step");
        }
    }

    void CheckPositions()
    {
        handStartPosition = hand.transform.position;
        visualizer.transform.position = handStartPosition;
    }

    void CheckBoundries()
    {
        maxH += handStartPosition.x;      // TODO: Update after player has moved,
        minH -= -handStartPosition.x;     //       create animation event or ?? 
        maxV += handStartPosition.y;      
        minV -= -handStartPosition.y;     
    }

    void TakeInput()
    {
        hor = Input.GetAxisRaw("Horizontal");
        ver = Input.GetAxisRaw("Vertical");
        direction = new Vector3(hor, ver, 0).normalized;
    }

    void ClampTargetPosition()
    {
        Vector3 clampPos = visualizer.transform.position;
        clampPos.y = Mathf.Clamp(visualizer.transform.position.y, minV, maxV);
        clampPos.x = Mathf.Clamp(visualizer.transform.position.x, minH, maxH);
        visualizer.transform.position = clampPos;
    }

    void MoveHand()
    {
        hand.GetComponent<Rigidbody>().AddForce((visualizer.transform.position - hand.transform.position) * 10000 * power);
        visualizer.transform.Translate(direction * speed * Time.deltaTime);

        if (hor == 0 && ver == 0)
        {
            visualizer.transform.position = Vector3.MoveTowards(visualizer.transform.position, handStartPosition, speed * Time.deltaTime);
        }
    }

    public void KinematicFalse()
    {
        foreach (Rigidbody rb in rbs)
        {
            rb.isKinematic = false;
            ukko.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

}