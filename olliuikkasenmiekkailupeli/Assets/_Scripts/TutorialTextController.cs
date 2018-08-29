using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextController : MonoBehaviour
{
    [SerializeField]
    GameObject L, R;
    private bool P1Left;

    public GameObject[] texts,
                        buttons0,
                        buttons1,
                        buttons2,
                        buttons3,
                        buttons4,
                        buttons5,
                        buttons6,
                        buttons7,
                        buttons8,
                        buttons9,
                        buttons10,
                        buttons11,
                        buttons12,
                        buttons13,
                        buttons14,
                        buttons15,
                        buttons16,
                        buttons17,
                        buttons18;

    public static TutorialTextController TTC;

    void Start()
    {
        TTC = this;
        P1Left = L.GetComponent<AlternativeMovement5>().GetFacingRight(1) ? true : false;
    }

    void Update()
    {
        if (texts[0].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left)
            {
                buttons0[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons0[1].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons0[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left)
            {
                buttons0[1].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons0[2].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons0[3].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard)
            {
                buttons0[2].SetActive(true);
                buttons0[3].SetActive(true);
            }
        }

        if (texts[1].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left)
            {
                buttons1[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons1[1].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons1[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left)
            {
                buttons1[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard)
            {
                buttons1[2].SetActive(true);
                buttons1[3].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons1[4].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons1[5].SetActive(true);
            }
        }

        if (texts[2].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left)
            {
                buttons2[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons2[1].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons2[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left)
            {
                buttons2[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard)
            {
                buttons2[2].SetActive(true);
                buttons2[3].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons2[4].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons2[5].SetActive(true);
            }
        }

        if (texts[3].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left|| InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons3[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left || InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons3[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard|| InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons3[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard || InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons3[3].SetActive(true);
            }
        }

        if (texts[4].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left)
            {
                buttons4[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons4[1].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons4[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left)
            {
                buttons4[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard)
            {
                buttons4[2].SetActive(true);
                buttons4[3].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons4[4].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons4[5].SetActive(true);
            }
        }

        if (texts[5].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left || InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons5[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left || InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons5[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard|| InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons5[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard|| InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons5[3].SetActive(true);
            }
        }

        if (texts[6].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left)
            {
                buttons6[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons6[1].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons6[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left)
            {
                buttons6[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard)
            {
                buttons6[2].SetActive(true);
                buttons6[3].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons6[4].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons6[5].SetActive(true);
            }
        }

        if (texts[7].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left || InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons7[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left || InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons7[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard || InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons7[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard || InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons7[3].SetActive(true);
            }
        }

        if (texts[8].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left)
            {
                buttons8[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons8[1].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons8[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left)
            {
                buttons8[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard)
            {
                buttons8[2].SetActive(true);
                buttons8[3].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons8[4].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons8[5].SetActive(true);
            }
        }

        if (texts[9].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left || InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons9[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left || InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons9[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard || InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons9[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard|| InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons9[3].SetActive(true);
            }
        }

        if (texts[10].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left || InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons10[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left || InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons10[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard || InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons10[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard|| InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons10[3].SetActive(true);
            }
        }

        if (texts[11].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left || InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons11[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left || InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons11[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard || InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons11[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard || InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons11[3].SetActive(true);
            }
        }

        if (texts[12].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left || InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons12[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left || InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons12[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard|| InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons12[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard || InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons12[3].SetActive(true);
            }
        }

        if (texts[13].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left || InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons13[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left || InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons13[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard|| InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons13[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard)
            {
                buttons13[3].SetActive(true);
            }
            if ( InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons13[4].SetActive(true);
            }
        }

        if (texts[14].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left || InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons14[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left || InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons14[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard  || InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons14[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard)
            {
                buttons14[3].SetActive(true);
            }
            if (InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons14[4].SetActive(true);
            }
        }

        if (texts[15].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left || InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons15[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left || InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons15[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard || InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons15[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard)
            {
                buttons15[3].SetActive(true);
            }
            if (InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons15[4].SetActive(true);
            }
        }

        if (texts[16].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left || InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons16[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left || InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons16[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard || InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons16[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard)
            {
                buttons16[3].SetActive(true);
            }
            if (InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons16[4].SetActive(true);
            }
        }

        if (texts[17].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left || InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons17[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left || InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons17[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard || InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons17[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard || InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons17[3].SetActive(true);
            }
        }

        if (texts[18].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && P1Left || InputManager.IM.isXboxControllerP1 && P1Left || InputManager.IM.isPSControllerP2 && !P1Left || InputManager.IM.isXboxControllerP2 && !P1Left)
            {
                buttons18[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && P1Left || InputManager.IM.isXboxControllerP2 && P1Left || InputManager.IM.isPSControllerP1 && !P1Left || InputManager.IM.isXboxControllerP1 && !P1Left)
            {
                buttons18[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard || InputManager.IM.isKeyboardAndMouseP1 && P1Left || InputManager.IM.isKeyboardAndMouseP2 && !P1Left)
            {
                buttons18[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard || InputManager.IM.isKeyboardAndMouseP1 && !P1Left || InputManager.IM.isKeyboardAndMouseP2 && P1Left)
            {
                buttons18[3].SetActive(true);
            }
        }
    }
}
