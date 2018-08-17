using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMoveList : MonoBehaviour
{

    [SerializeField] GameObject P1, P2;

    [SerializeField] GameObject[] P1_SpritesPositions, P2_SpritesPositions;

    [SerializeField] Sprite[] P1_Sprites_KeyboardOnly, P1_Sprites_KeyboardAndMouse, P1_Sprites_Controller;
    [SerializeField] Sprite[] P2_Sprites_KeyboardOnly, P2_Sprites_KeyboardAndMouse, P2_Sprites_Controller;

    bool P1_left;

    private void Start()
    {
        P1 = GameObject.FindGameObjectWithTag("P1");
        P2 = GameObject.FindGameObjectWithTag("P2");

        P1_left = P1.GetComponent<AlternativeMovement5>().GetFacingRight(1) ? true : false;
    }


    public void ShowButtons()
    {

        for (int i = 0; i < P1_SpritesPositions.Length; i++)
        {
            for (int j = 0; j < P2_SpritesPositions.Length; j++)
            {
                P1_SpritesPositions[i].gameObject.SetActive(false);
                P2_SpritesPositions[i].gameObject.SetActive(false);
            }
        }

        if(P1_left)
        {
            if (InputManager.IM.isOnlyKeyboard)
            {
                for (int i = 0; i < P1_SpritesPositions.Length; i++)
                {
                    P1_SpritesPositions[i].GetComponent<SpriteRenderer>().sprite = P1_Sprites_KeyboardOnly[i];
                    P1_SpritesPositions[i].gameObject.SetActive(true);
                }
            }
            else if (InputManager.IM.isKeyboardAndMouseP1 || InputManager.IM.isKeyboardAndMouseP2)
            {
                for (int i = 0; i < P1_SpritesPositions.Length; i++)
                {
                    P1_SpritesPositions[i].GetComponent<SpriteRenderer>().sprite = P1_Sprites_KeyboardAndMouse[i];
                    P1_SpritesPositions[i].gameObject.SetActive(true);
                }
            }
            else if(!InputManager.IM.isOnlyKeyboard || !InputManager.IM.isKeyboardAndMouseP1)
            {
                for (int i = 0; i < P1_SpritesPositions.Length; i++)
                {
                    P1_SpritesPositions[i].GetComponent<SpriteRenderer>().sprite = P1_Sprites_Controller[i];
                    P1_SpritesPositions[i].gameObject.SetActive(true);
                }
            }
        }
        else
        {
            if (InputManager.IM.isOnlyKeyboard)
            {
                for (int i = 0; i < P2_SpritesPositions.Length; i++)
                {
                    P2_SpritesPositions[i].GetComponent<SpriteRenderer>().sprite = P2_Sprites_KeyboardOnly[i];
                    P2_SpritesPositions[i].gameObject.SetActive(true);
                }
            }
            else if (InputManager.IM.isKeyboardAndMouseP2 || InputManager.IM.isKeyboardAndMouseP2)
            {
                for (int i = 0; i < P2_SpritesPositions.Length; i++)
                {
                    P2_SpritesPositions[i].GetComponent<SpriteRenderer>().sprite = P2_Sprites_KeyboardOnly[i];
                    P2_SpritesPositions[i].gameObject.SetActive(true);
                }
            }
            else if (!InputManager.IM.isOnlyKeyboard || !InputManager.IM.isKeyboardAndMouseP2)
            {
                for (int i = 0; i < P2_SpritesPositions.Length; i++)
                {
                    P2_SpritesPositions[i].GetComponent<SpriteRenderer>().sprite = P2_Sprites_KeyboardOnly[i];
                    P2_SpritesPositions[i].gameObject.SetActive(true);
                }
            }
        }

    }

    public void CloseMoveList()
    {
        for (int i = 0; i < P1_SpritesPositions.Length; i++)
        {
            for (int j = 0; j < P2_SpritesPositions.Length; j++)
            {
                P1_SpritesPositions[i].gameObject.SetActive(false);
                P2_SpritesPositions[i].gameObject.SetActive(false);
            }
        }
    }

}
