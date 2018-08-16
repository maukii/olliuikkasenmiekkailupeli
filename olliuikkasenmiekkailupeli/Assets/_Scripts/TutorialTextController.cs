using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextController : MonoBehaviour
{
    public GameObject[] texts, buttons;

    public static TutorialTextController TTC;

    void Start()
    {
        TTC = this;
    }

    void Update()
    {

    }
}
