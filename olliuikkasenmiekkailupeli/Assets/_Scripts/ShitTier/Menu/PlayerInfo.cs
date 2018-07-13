using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfo : MonoBehaviour
{

    public GameObject activeModel;

    [Header("-- Axis --")]
    public string horizontalName;
    public string verticalName;

    [Header("-- Player Info --")]
    public int playerIndex;
    public int modelIndex;
    public bool ready;

    public void SetInputs(string hor, string ver)
    {
        horizontalName = hor;
        verticalName = ver;
        // buttons laters
    }

    public void ChooseCharacter()
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).gameObject.activeSelf == true)
            {
                modelIndex = i;
                activeModel = gameObject.transform.GetChild(i).gameObject;
            }
        }

        if(activeModel.GetComponent<Animator>() != null) // do simple animation without rootmotion ty
        {
            activeModel.GetComponent<Animator>().SetBool("Ready", ready);
            activeModel.GetComponent<Animator>().SetTrigger("Choose");
        }

    }

}
