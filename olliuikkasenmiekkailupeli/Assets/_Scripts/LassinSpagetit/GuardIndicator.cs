using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardIndicator : MonoBehaviour {
    HandAnimationControl hac;
    GameObject[] guardIndicators;
    float inside = -1;
    float hanging = -1;
    bool useIndicator = false;
	
	void Start () {
        hac = GetComponentInParent<HandAnimationControl>();
        guardIndicators = new GameObject[transform.childCount];
        for(int i = 0; i < transform.childCount; i++)
        {
            guardIndicators[i] = transform.GetChild(i).gameObject;
        }
        DissableIndicators();
	}
	
	
	void Update () {
        if (useIndicator)
        {
            
            inside = hac.GetInsideForIndicators();
            hanging = hac.GetHangingForIndicators();
            if (inside == 0 && hanging == 0) EnableIndicator(0);
            else if (inside == 1 && hanging == 0) EnableIndicator(1);
            else if (inside == 0 && hanging == 1) EnableIndicator(2);
            else if (inside == 1 && hanging == 1) EnableIndicator(3);
        }
        else
        {
            DissableIndicators();
        }
    }
    void EnableIndicator(int index)
    {
        for (int i = 0; i < guardIndicators.Length; i++)
        {
            guardIndicators[i].SetActive(false);
        }
        guardIndicators[index].SetActive(true);
    }
    void DissableIndicators()
    {
        for(int i = 0; i < guardIndicators.Length; i++)
        {
            guardIndicators[i].SetActive(false);
        }
    }
    public bool UseIndicators(bool Enable)
    {
        useIndicator = Enable;
        return useIndicator;
    }
}
