using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightCollision : MonoBehaviour {
    GameObject[] SwordCollisionPartsP1;
    GameObject[] SwordCollisionPartsP2;
    RectTransform[] HeightMeterP1;
    RectTransform[] HeightMeterP2;
    float Height;
    float[] TipH = new float[2];
    float[] MiddleH = new float[2];
    float[] BaseH = new float[2];
    float[] HandleH = new float[2];

    public bool ShowHeightMeters = false;
    GameObject[] HeightMeters = new GameObject[2];


    
    void Start () {
        GameObject[] hm = GameObject.FindGameObjectsWithTag("HeightMeterP1");
        HeightMeterP1 = new RectTransform[hm.Length]; 
        for(int i = 0; hm.Length > i; i++)
        {
            HeightMeterP1[i] = hm[i].GetComponent<RectTransform>();
        }
        HeightMeters[0] = hm[0].transform.parent.gameObject;

        hm = GameObject.FindGameObjectsWithTag("HeightMeterP2");
        HeightMeterP2 = new RectTransform[hm.Length];
        for (int i = 0; hm.Length > i; i++)
        {
            HeightMeterP2[i] = hm[i].GetComponent<RectTransform>();
        }
        HeightMeters[1] = hm[0].transform.parent.gameObject;

        SwordCollisionPartsP1 = GameObject.FindGameObjectsWithTag("SwordPointsP1");
        SwordCollisionPartsP2 = GameObject.FindGameObjectsWithTag("SwordPointsP2");
    }
	
	
	void Update () {
        UpdateSwordHeight();

        if (ShowHeightMeters)
        {
            EnableHeightMeter();
            UpdateHeightMeter();
        }
        else
        {
            DissableHeightMeter();
        }
        
	}
    void EnableHeightMeter()
    {
        for (int playernumber = 0; playernumber < 2; playernumber++)
        {
            HeightMeters[playernumber].SetActive(true);
        }
    }
    void DissableHeightMeter()
    {
        for (int playernumber = 0; playernumber < 2; playernumber++)
        {
            HeightMeters[playernumber].SetActive(false);
        }
    }
    void UpdateSwordHeight()
    {
        for(int playerNumber = 0; playerNumber < 2; playerNumber++)
        {
            if(playerNumber == 0)
            {
                for (int i = 0; i < SwordCollisionPartsP1.Length; i++)
                {
                    if (SwordCollisionPartsP1[i].name == "Tip")
                    {
                        TipH[playerNumber] = SwordCollisionPartsP1[i].transform.position.y;
                    }
                    else if (SwordCollisionPartsP1[i].name == "Middle")
                    {
                        MiddleH[playerNumber] = SwordCollisionPartsP1[i].transform.position.y;
                    }
                    else if (SwordCollisionPartsP1[i].name == "Base")
                    {
                        BaseH[playerNumber] = SwordCollisionPartsP1[i].transform.position.y;
                    }
                    else if (SwordCollisionPartsP1[i].name == "Handle")
                    {
                        HandleH[playerNumber] = SwordCollisionPartsP1[i].transform.position.y;
                    }
                }
            }
            if (playerNumber == 1)
            {
                for (int i = 0; i < SwordCollisionPartsP2.Length; i++)
                {
                    if (SwordCollisionPartsP2[i].name == "Tip")
                    {
                        TipH[playerNumber] = SwordCollisionPartsP2[i].transform.position.y;
                    }
                    else if (SwordCollisionPartsP2[i].name == "Middle")
                    {
                        MiddleH[playerNumber] = SwordCollisionPartsP2[i].transform.position.y;
                    }
                    else if (SwordCollisionPartsP2[i].name == "Base")
                    {
                        BaseH[playerNumber] = SwordCollisionPartsP2[i].transform.position.y;
                    }
                    else if (SwordCollisionPartsP2[i].name == "Handle")
                    {
                        HandleH[playerNumber] = SwordCollisionPartsP2[i].transform.position.y;
                    }
                }
            }

        }
    }
    void UpdateHeightMeter()
    {
        for (int playerNumber = 0; playerNumber < 2; playerNumber++)
        {
            if (playerNumber == 0)
            {
                for (int i = 0; i < HeightMeterP1.Length; i++)
                {
                    if (HeightMeterP1[i].name == "Height")
                    {
                        
                    }
                    else if (HeightMeterP1[i].name == "Middle")
                    {
                        
                        if(TipH[playerNumber] - MiddleH[playerNumber]< 0)
                        {
                            HeightMeterP1[i].sizeDelta = new Vector2(1, MiddleH[playerNumber] - TipH[playerNumber]);
                            HeightMeterP1[i].localScale = new Vector3(1, -1, 1);
                        }
                        else
                        {
                            HeightMeterP1[i].sizeDelta = new Vector2(1, TipH[playerNumber] - MiddleH[playerNumber]);
                            HeightMeterP1[i].localScale = new Vector3(1, 1, 1);
                        }
                        HeightMeterP1[i].position = new Vector3(HeightMeterP1[i].position.x, MiddleH[playerNumber], HeightMeterP1[i].position.z);
                        HeightMeterP1[i].localPosition = new Vector3(-0.5f, HeightMeterP1[i].localPosition.y, 0);
                    }
                    else if (HeightMeterP1[i].name == "Base")
                    {
                        if (MiddleH[playerNumber] - BaseH[playerNumber] < 0)
                        {
                            HeightMeterP1[i].sizeDelta = new Vector2(1, BaseH[playerNumber] - MiddleH[playerNumber]);
                            HeightMeterP1[i].localScale = new Vector3(1, -1, 1);
                        }
                        else
                        {
                            HeightMeterP1[i].sizeDelta = new Vector2(1, MiddleH[playerNumber] - BaseH[playerNumber]);
                            HeightMeterP1[i].localScale = new Vector3(1, 1, 1);
                        }
                        
                        HeightMeterP1[i].position = new Vector3(HeightMeterP1[i].position.x, BaseH[playerNumber], HeightMeterP1[i].position.z);
                        HeightMeterP1[i].localPosition = new Vector3(-0.5f, HeightMeterP1[i].localPosition.y, 0);

                    }
                    else if (HeightMeterP1[i].name == "Handle")
                    {
                        if (BaseH[playerNumber] - HandleH[playerNumber] < 0)
                        {
                            HeightMeterP1[i].sizeDelta = new Vector2(1, HandleH[playerNumber] - BaseH[playerNumber]);
                            HeightMeterP1[i].localScale = new Vector3(1, -1, 1);
                        }
                        else
                        {
                            HeightMeterP1[i].sizeDelta = new Vector2(1, BaseH[playerNumber] - HandleH[playerNumber]);
                            HeightMeterP1[i].localScale = new Vector3(1, 1, 1);
                        }
                        
                        HeightMeterP1[i].position = new Vector3(HeightMeterP1[i].position.x, HandleH[playerNumber], HeightMeterP1[i].position.z);
                        HeightMeterP1[i].localPosition = new Vector3(-0.5f, HeightMeterP1[i].localPosition.y, 0);
                    }
                }
            }
            if (playerNumber == 1)
            {
                for (int i = 0; i < HeightMeterP2.Length; i++)
                {
                    if (HeightMeterP2[i].name == "Height")
                    {

                    }
                    else if (HeightMeterP2[i].name == "Middle")
                    {

                        if (TipH[playerNumber] - MiddleH[playerNumber] < 0)
                        {
                            HeightMeterP2[i].sizeDelta = new Vector2(1, MiddleH[playerNumber] - TipH[playerNumber]);
                            HeightMeterP2[i].localScale = new Vector3(1, -1, 1);
                        }
                        else
                        {
                            HeightMeterP2[i].sizeDelta = new Vector2(1, TipH[playerNumber] - MiddleH[playerNumber]);
                            HeightMeterP2[i].localScale = new Vector3(1, 1, 1);
                        }
                        HeightMeterP2[i].position = new Vector3(HeightMeterP2[i].position.x, MiddleH[playerNumber], HeightMeterP2[i].position.z);
                        HeightMeterP2[i].localPosition = new Vector3(-0.5f, HeightMeterP2[i].localPosition.y, 0);
                    }
                    else if (HeightMeterP2[i].name == "Base")
                    {
                        if (MiddleH[playerNumber] - BaseH[playerNumber] < 0)
                        {
                            HeightMeterP2[i].sizeDelta = new Vector2(1, BaseH[playerNumber] - MiddleH[playerNumber]);
                            HeightMeterP2[i].localScale = new Vector3(1, -1, 1);
                        }
                        else
                        {
                            HeightMeterP2[i].sizeDelta = new Vector2(1, MiddleH[playerNumber] - BaseH[playerNumber]);
                            HeightMeterP2[i].localScale = new Vector3(1, 1, 1);
                        }

                        HeightMeterP2[i].position = new Vector3(HeightMeterP2[i].position.x, BaseH[playerNumber], HeightMeterP2[i].position.z);
                        HeightMeterP2[i].localPosition = new Vector3(-0.5f, HeightMeterP2[i].localPosition.y, 0);

                    }
                    else if (HeightMeterP2[i].name == "Handle")
                    {
                        if (BaseH[playerNumber] - HandleH[playerNumber] < 0)
                        {
                            HeightMeterP2[i].sizeDelta = new Vector2(1, HandleH[playerNumber] - BaseH[playerNumber]);
                            HeightMeterP2[i].localScale = new Vector3(1, -1, 1);
                        }
                        else
                        {
                            HeightMeterP2[i].sizeDelta = new Vector2(1, BaseH[playerNumber] - HandleH[playerNumber]);
                            HeightMeterP2[i].localScale = new Vector3(1, 1, 1);
                        }

                        HeightMeterP2[i].position = new Vector3(HeightMeterP2[i].position.x, HandleH[playerNumber], HeightMeterP2[i].position.z);
                        HeightMeterP2[i].localPosition = new Vector3(-0.5f, HeightMeterP2[i].localPosition.y, 0);
                    }
                }
            }
        }
    }
}
