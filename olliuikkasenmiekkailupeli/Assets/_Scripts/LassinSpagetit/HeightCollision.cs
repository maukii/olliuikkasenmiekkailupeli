using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightCollision : MonoBehaviour {
    GameObject[] SwordCollisionPartsP1;
    GameObject[] SwordCollisionPartsP2;
    RectTransform[] HeightMeterP1;
    RectTransform[] HeightMeterP2;
    float[] Height = new float[2];
    Vector3[] TipH = new Vector3[2];
    Vector3[] MiddleH = new Vector3[2];
    Vector3[] BaseH = new Vector3[2];
    Vector3[] HandleH = new Vector3[2];
    float heightOffset;


    public bool ShowHeightMeters = false;
    GameObject[] HeightMeters = new GameObject[2];
    CollisionHandler ch;


    void Start () {
        ch = gameObject.GetComponent<CollisionHandler>();

        GameObject[] hm = GameObject.FindGameObjectsWithTag("HeightMeterP1");
        if (ShowHeightMeters)
        {
            HeightMeterP1 = new RectTransform[hm.Length];
            for (int i = 0; hm.Length > i; i++)
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
        }

        heightOffset = GameObject.FindGameObjectWithTag("HeightRefPoint").transform.position.y;
    }
	
	
	void Update () {
        if(SwordCollisionPartsP1 == null)
        {
            SwordCollisionPartsP1 = GameObject.FindGameObjectsWithTag("SwordPointsP1");
            SwordCollisionPartsP2 = GameObject.FindGameObjectsWithTag("SwordPointsP2");
        }
        UpdateSwordHeight();
        Height[0] = ch.GetHeight(1);
        Height[1] = ch.GetHeight(2);

        if (ShowHeightMeters)
        {
            EnableHeightMeter();
            UpdateHeightMeter();
        }
        else
        {
            //DissableHeightMeter();
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
                        TipH[playerNumber] = SwordCollisionPartsP1[i].transform.position;

                    }
                    else if (SwordCollisionPartsP1[i].name == "Middle")
                    {
                        MiddleH[playerNumber] = SwordCollisionPartsP1[i].transform.position;
                    }
                    else if (SwordCollisionPartsP1[i].name == "Base")
                    {
                        BaseH[playerNumber] = SwordCollisionPartsP1[i].transform.position;
                    }
                    else if (SwordCollisionPartsP1[i].name == "Handle")
                    {
                        HandleH[playerNumber] = SwordCollisionPartsP1[i].transform.position;
                    }
                }
            }
            if (playerNumber == 1)
            {
                for (int i = 0; i < SwordCollisionPartsP2.Length; i++)
                {
                    if (SwordCollisionPartsP2[i].name == "Tip")
                    {
                        TipH[playerNumber] = SwordCollisionPartsP2[i].transform.position;
                    }
                    else if (SwordCollisionPartsP2[i].name == "Middle")
                    {
                        MiddleH[playerNumber] = SwordCollisionPartsP2[i].transform.position;
                    }
                    else if (SwordCollisionPartsP2[i].name == "Base")
                    {
                        BaseH[playerNumber] = SwordCollisionPartsP2[i].transform.position;
                    }
                    else if (SwordCollisionPartsP2[i].name == "Handle")
                    {
                        HandleH[playerNumber] = SwordCollisionPartsP2[i].transform.position;
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
                        HeightMeterP1[i].position = new Vector3(HeightMeterP1[i].position.x, Height[playerNumber], HeightMeterP1[i].position.z);
                        HeightMeterP1[i].localPosition = new Vector3(-0.5f, HeightMeterP1[i].localPosition.y, 0);
                    }
                    else if (HeightMeterP1[i].name == "Middle")
                    {
                        
                        if(TipH[playerNumber].y - MiddleH[playerNumber].y < 0)
                        {
                            HeightMeterP1[i].sizeDelta = new Vector2(1, MiddleH[playerNumber].y - TipH[playerNumber].y);
                            HeightMeterP1[i].localScale = new Vector3(1, -1, 1);
                        }
                        else
                        {
                            HeightMeterP1[i].sizeDelta = new Vector2(1, TipH[playerNumber].y - MiddleH[playerNumber].y);
                            HeightMeterP1[i].localScale = new Vector3(1, 1, 1);
                        }
                        HeightMeterP1[i].position = new Vector3(HeightMeterP1[i].position.x, MiddleH[playerNumber].y, HeightMeterP1[i].position.z);
                        HeightMeterP1[i].localPosition = new Vector3(-0.5f, HeightMeterP1[i].localPosition.y, 0);
                    }
                    else if (HeightMeterP1[i].name == "Base")
                    {
                        if (MiddleH[playerNumber].y - BaseH[playerNumber].y < 0)
                        {
                            HeightMeterP1[i].sizeDelta = new Vector2(1, BaseH[playerNumber].y - MiddleH[playerNumber].y);
                            HeightMeterP1[i].localScale = new Vector3(1, -1, 1);
                        }
                        else
                        {
                            HeightMeterP1[i].sizeDelta = new Vector2(1, MiddleH[playerNumber].y - BaseH[playerNumber].y);
                            HeightMeterP1[i].localScale = new Vector3(1, 1, 1);
                        }
                        
                        HeightMeterP1[i].position = new Vector3(HeightMeterP1[i].position.x, BaseH[playerNumber].y, HeightMeterP1[i].position.z);
                        HeightMeterP1[i].localPosition = new Vector3(-0.5f, HeightMeterP1[i].localPosition.y, 0);

                    }
                    else if (HeightMeterP1[i].name == "Handle")
                    {
                        if (BaseH[playerNumber].y - HandleH[playerNumber].y < 0)
                        {
                            HeightMeterP1[i].sizeDelta = new Vector2(1, HandleH[playerNumber].y - BaseH[playerNumber].y);
                            HeightMeterP1[i].localScale = new Vector3(1, -1, 1);
                        }
                        else
                        {
                            HeightMeterP1[i].sizeDelta = new Vector2(1, BaseH[playerNumber].y - HandleH[playerNumber].y);
                            HeightMeterP1[i].localScale = new Vector3(1, 1, 1);
                        }
                        
                        HeightMeterP1[i].position = new Vector3(HeightMeterP1[i].position.x, HandleH[playerNumber].y, HeightMeterP1[i].position.z);
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
                        HeightMeterP2[i].position = new Vector3(HeightMeterP2[i].position.x, Height[playerNumber], HeightMeterP2[i].position.z);
                        HeightMeterP2[i].localPosition = new Vector3(-0.5f, HeightMeterP2[i].localPosition.y, 0);
                    }
                    else if (HeightMeterP2[i].name == "Middle")
                    {

                        if (TipH[playerNumber].y - MiddleH[playerNumber].y < 0)
                        {
                            HeightMeterP2[i].sizeDelta = new Vector2(1, MiddleH[playerNumber].y - TipH[playerNumber].y);
                            HeightMeterP2[i].localScale = new Vector3(1, -1, 1);
                        }
                        else
                        {
                            HeightMeterP2[i].sizeDelta = new Vector2(1, TipH[playerNumber].y - MiddleH[playerNumber].y);
                            HeightMeterP2[i].localScale = new Vector3(1, 1, 1);
                        }
                        HeightMeterP2[i].position = new Vector3(HeightMeterP2[i].position.x, MiddleH[playerNumber].y, HeightMeterP2[i].position.z);
                        HeightMeterP2[i].localPosition = new Vector3(-0.5f, HeightMeterP2[i].localPosition.y, 0);
                    }
                    else if (HeightMeterP2[i].name == "Base")
                    {
                        if (MiddleH[playerNumber].y - BaseH[playerNumber].y < 0)
                        {
                            HeightMeterP2[i].sizeDelta = new Vector2(1, BaseH[playerNumber].y - MiddleH[playerNumber].y);
                            HeightMeterP2[i].localScale = new Vector3(1, -1, 1);
                        }
                        else
                        {
                            HeightMeterP2[i].sizeDelta = new Vector2(1, MiddleH[playerNumber].y - BaseH[playerNumber].y);
                            HeightMeterP2[i].localScale = new Vector3(1, 1, 1);
                        }

                        HeightMeterP2[i].position = new Vector3(HeightMeterP2[i].position.x, BaseH[playerNumber].y, HeightMeterP2[i].position.z);
                        HeightMeterP2[i].localPosition = new Vector3(-0.5f, HeightMeterP2[i].localPosition.y, 0);

                    }
                    else if (HeightMeterP2[i].name == "Handle")
                    {
                        if (BaseH[playerNumber].y - HandleH[playerNumber].y < 0)
                        {
                            HeightMeterP2[i].sizeDelta = new Vector2(1, HandleH[playerNumber].y - BaseH[playerNumber].y);
                            HeightMeterP2[i].localScale = new Vector3(1, -1, 1);
                        }
                        else
                        {
                            HeightMeterP2[i].sizeDelta = new Vector2(1, BaseH[playerNumber].y - HandleH[playerNumber].y);
                            HeightMeterP2[i].localScale = new Vector3(1, 1, 1);
                        }

                        HeightMeterP2[i].position = new Vector3(HeightMeterP2[i].position.x, HandleH[playerNumber].y, HeightMeterP2[i].position.z);
                        HeightMeterP2[i].localPosition = new Vector3(-0.5f, HeightMeterP2[i].localPosition.y, 0);
                    }
                }
            }
        }
    }
    public float GetTipY(int player)
    {
        return TipH[player - 1].y;
    }
    public float GetMiddleY(int player)
    {
        return MiddleH[player - 1].y;
    }
    public float GetBaseY(int player)
    {
        return BaseH[player - 1].y;
    }
    public float GetHandleY(int player)
    {
        return HandleH[player - 1].y;
    }
    public float GetHeightOffset()
    {
        return heightOffset;
    }
    public Vector3 GetTip(int player)
    {
        return TipH[player - 1];
    }
    public Vector3 GetMiddle(int player)
    {
        return MiddleH[player - 1];
    }
    public Vector3 GetBase(int player)
    {
        return BaseH[player - 1];
    }
    public Vector3 GetHandle(int player)
    {
        return HandleH[player - 1];
    }
}
