using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseIngameModel : MonoBehaviour
{

    GameObject[] models;
    Camera cam;

    private void Awake()
    {
        cam = FindObjectOfType<Camera>();
    }

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

        if (gameObject.name == "P1")
            cam.GetComponent<CameraScript>().P1 = models[modelIndex].transform;
        else if(gameObject.name == "P2")
            cam.GetComponent<CameraScript>().P2 = models[modelIndex].transform;

    }

}
