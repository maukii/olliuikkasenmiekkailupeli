using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMainMenuTitleBar : MonoBehaviour
{
    //Tällä hetkellä Box Collider objektissa 'kämmen.L'
    public Image bar;
    public bool isTouching;

    void Update()
    {
        if (isTouching)
        {
            bar.enabled = true;
        }

        else
        {
            bar.enabled = false;
        }
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Untagged")) //Laita joku toinen tagi tai sitten ei...
        {
            isTouching = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.CompareTag("Untagged"))
        {
            isTouching = false;
        }
    }
}
