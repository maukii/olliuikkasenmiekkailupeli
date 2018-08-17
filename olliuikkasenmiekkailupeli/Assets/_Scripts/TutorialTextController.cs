using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialTextController : MonoBehaviour
{
    [SerializeField]
    GameObject L, R;

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
    }

    void Update()
    {
        if (texts[0].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons0[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP1 && R || InputManager.IM.isXboxControllerP1 && R)
            {
                buttons0[1].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons0[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && R || InputManager.IM.isXboxControllerP2 && R)
            {
                buttons0[1].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && L)
            {
                buttons0[2].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons0[3].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L)
            {
                buttons0[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R)
            {
                buttons0[3].SetActive(true);
            }
        }

        if (texts[1].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons1[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP1 && R || InputManager.IM.isXboxControllerP1 && R)
            {
                buttons1[1].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons1[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && R || InputManager.IM.isXboxControllerP2 && R)
            {
                buttons1[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L)
            {
                buttons1[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R)
            {
                buttons1[3].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons1[4].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons1[5].SetActive(true);
            }
        }

        if (texts[2].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons2[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP1 && R || InputManager.IM.isXboxControllerP1 && R)
            {
                buttons2[1].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons2[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && R || InputManager.IM.isXboxControllerP2 && R)
            {
                buttons2[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L)
            {
                buttons2[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R)
            {
                buttons2[3].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons2[4].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons2[5].SetActive(true);
            }
        }

        if (texts[3].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons3[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons3[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L || InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons3[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R || InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons3[3].SetActive(true);
            }
        }

        if (texts[4].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons4[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP1 && R || InputManager.IM.isXboxControllerP1 && R)
            {
                buttons4[1].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons4[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && R || InputManager.IM.isXboxControllerP2 && R)
            {
                buttons4[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L)
            {
                buttons4[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R)
            {
                buttons4[3].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons4[4].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons4[5].SetActive(true);
            }
        }

        if (texts[5].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons5[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons5[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L || InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons5[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R || InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons5[3].SetActive(true);
            }
        }

        if (texts[6].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons6[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP1 && R || InputManager.IM.isXboxControllerP1 && R)
            {
                buttons6[1].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons6[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && R || InputManager.IM.isXboxControllerP2 && R)
            {
                buttons6[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L)
            {
                buttons6[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R)
            {
                buttons6[3].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons6[4].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons6[5].SetActive(true);
            }
        }

        if (texts[7].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons7[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons7[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L || InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons7[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R || InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons7[3].SetActive(true);
            }
        }

        if (texts[8].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons8[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP1 && R || InputManager.IM.isXboxControllerP1 && R)
            {
                buttons8[1].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons8[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && R || InputManager.IM.isXboxControllerP2 && R)
            {
                buttons8[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L)
            {
                buttons8[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R)
            {
                buttons8[3].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons8[4].SetActive(true);
            }

            if (InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons8[5].SetActive(true);
            }
        }

        if (texts[9].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons9[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons9[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L || InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons9[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R || InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons9[3].SetActive(true);
            }
        }

        if (texts[10].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons10[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons10[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L || InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons10[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R || InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons10[3].SetActive(true);
            }
        }

        if (texts[11].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons11[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons11[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L || InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons11[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R || InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons11[3].SetActive(true);
            }
        }

        if (texts[12].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons12[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons12[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L || InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons12[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R || InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons12[3].SetActive(true);
            }
        }

        if (texts[13].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons13[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons13[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L || InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons13[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R || InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons13[3].SetActive(true);
            }
        }

        if (texts[14].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons14[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons14[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L || InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons14[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R || InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons14[3].SetActive(true);
            }
        }

        if (texts[15].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons15[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons15[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L || InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons15[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R || InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons15[3].SetActive(true);
            }
        }

        if (texts[16].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons16[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons16[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L || InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons16[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R || InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons16[3].SetActive(true);
            }
        }

        if (texts[17].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons17[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons17[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L || InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons17[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R || InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons17[3].SetActive(true);
            }
        }

        if (texts[18].activeSelf == true)
        {
            if (InputManager.IM.isPSControllerP1 && L || InputManager.IM.isXboxControllerP1 && L)
            {
                buttons18[0].SetActive(true);
            }

            if (InputManager.IM.isPSControllerP2 && L || InputManager.IM.isXboxControllerP2 && L)
            {
                buttons18[1].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && L || InputManager.IM.isKeyboardAndMouseP1 && L || InputManager.IM.isKeyboardAndMouseP2 && L)
            {
                buttons18[2].SetActive(true);
            }

            if (InputManager.IM.isOnlyKeyboard && R || InputManager.IM.isKeyboardAndMouseP1 && R || InputManager.IM.isKeyboardAndMouseP2 && R)
            {
                buttons18[3].SetActive(true);
            }
        }
    }
}
