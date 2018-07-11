using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseIngameModel : MonoBehaviour
{

    GameObject[] models;
    
    public void ChooseModel(int modelIndex)
    {
        models = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            models[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in models)
        {
            go.SetActive(false);
        }

        models[modelIndex].gameObject.SetActive(true);
    }

}
