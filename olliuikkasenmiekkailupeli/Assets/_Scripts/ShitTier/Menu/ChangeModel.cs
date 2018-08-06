using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModel : MonoBehaviour
{
    PlayerInfo info;

    public GameObject[] characterList;
    private int index;
    private bool swapped;


    private void Start()
    {
        info = GetComponent<PlayerInfo>();
        CreateCharacterList();
    }

    private void CreateCharacterList()
    {
        characterList = new GameObject[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            characterList[i] = transform.GetChild(i).gameObject;
        }

        foreach (GameObject go in characterList)
        {
            go.SetActive(false);
        }

        if (characterList[0])
            characterList[0].SetActive(true);
    }

    private void Update()
    {
        ChangeModels();
    }
    
    void ChangeModels()
    {
        if(!GetComponent<PlayerInfo>().ready)
        {
            if (info.playerIndex != 0)
            {
                if (Input.GetAxisRaw(info.horizontalName) < -0.1 && !swapped) // toggle left
                {
                    characterList[index].SetActive(false);

                    index--;
                    if (index < 0)
                        index = characterList.Length - 1;

                    characterList[index].SetActive(true);
                    swapped = true;
                }
                else if (Input.GetAxisRaw(info.horizontalName) > 0.1 && !swapped) // toggle right
                {
                    characterList[index].SetActive(false);

                    index++;
                    if (index == characterList.Length)
                        index = 0;

                    characterList[index].SetActive(true);
                    swapped = true;
                }
                if (Input.GetAxisRaw(info.horizontalName) == 0)
                {
                    swapped = false;
                }
            }
        }
        
        info.modelIndex = index;

    }

}
