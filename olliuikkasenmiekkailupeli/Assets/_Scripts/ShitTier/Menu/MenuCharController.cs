using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCharController : MonoBehaviour
{

    GameObject currentNode = null;
    public GameObject sword;

    public float offset = -30f;

    Animator anim;

    float dampTime = 0.2f;

    [ExecuteInEditMode]
    private void Awake()
    {
        transform.parent.parent.transform.position = new Vector3(transform.parent.parent.transform.position.x, offset, transform.parent.transform.position.z);
        transform.parent.transform.Rotate(-90f, 0, 0);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckNode();
    }

    public void MouseOverMe(GameObject node)
    {
        currentNode = node;
    }

    void CheckNode()
    {
        if(currentNode != null)
        {
            if (currentNode.name == "StartButton")
            {
                anim.SetFloat("Blend", 1, dampTime, Time.deltaTime);
            }
            else if (currentNode.name == "OptionsButton")
            {
                anim.SetFloat("Blend", 0, dampTime, Time.deltaTime);
            }
            else if (currentNode.name == "QuitButton")
            {
                anim.SetFloat("Blend", -1, dampTime, Time.deltaTime);
            }
        }
    }
}
