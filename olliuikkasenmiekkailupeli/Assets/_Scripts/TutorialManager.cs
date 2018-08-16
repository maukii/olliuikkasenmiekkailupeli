using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public float defaultTimer, timer = 5f;
    public float defaultInputTimer, inputTimerP1 = 0.5f, inputTimerP2 = 0.5f;
    public float defaultTextTimer, textTimer;

    [Header("---Affects movement---")]
    public bool moveLock;
    public bool guardLock;
    public bool deathLock;
    public bool heightLock;
    public bool lungeLock;

    [Header("---Checks for player input---")]
    public bool P1_OK;
    public bool P2_OK;
    public bool P1Clear;
    public bool P2Clear;
    public bool canInteractText;

    [Header("---Tutorial phases---")]
    public bool tutorialNotStarted;
    public bool tutorialClear;
    
    public bool phase1, phase2, phase3, phase4, phase5, phase6, phase7, phase8, phase9, phase10, phase11, phase12, phase13, phase14, phase15;
    
    float hangingP1, hangingP2, insideP1, insideP2, heightP1, heightP2;
    
    Animator animP1, animP2;
    InputManager im;
    GameObject P1, P2;

    public static TutorialManager TM;

    void Start ()
    {
        im = FindObjectOfType<InputManager>();
        TM = this;
        tutorialNotStarted = true;

        textTimer = 1f;

        defaultTimer = timer;
        defaultTextTimer = textTimer;
        defaultInputTimer = inputTimerP1;
    }

	void Update ()
    {
        if (MainMenuController.MMC.isTutorial && GameHandler.instance.BattleStarted)
        {
            tutorialNotStarted = false;
        }

        if (!tutorialNotStarted && MainMenuController.MMC.isTutorial)
        {
            TutorialPhases();
        }

        TextInteraction();
        TutorialExit();
        TutorialClear();
    }

    void TutorialPhases ()
    {
        if (!tutorialNotStarted && MainMenuController.MMC.isTutorial)
        {
            P1 = GameObject.FindGameObjectWithTag("Player 1");
            P2 = GameObject.FindGameObjectWithTag("Player 2");

            if (moveLock)
            {
                P1.GetComponent<AlternativeMovement5>().enabled = false;
                P2.GetComponent<AlternativeMovement5>().enabled = false;
            }

            if (!moveLock)
            {
                P1.GetComponent<AlternativeMovement5>().enabled = true;
                P2.GetComponent<AlternativeMovement5>().enabled = true;
            }

            animP1 = P1.GetComponentInChildren<Animator>();
            animP2 = P2.GetComponentInChildren<Animator>();

            hangingP1 = animP1.GetFloat("Hanging");
            insideP1 = animP1.GetFloat("Inside");
            heightP1 = animP1.GetFloat("Height");

            hangingP2 = animP2.GetFloat("Hanging");
            insideP2 = animP2.GetFloat("Inside");
            heightP2 = animP2.GetFloat("Height");

            phase1 = true;

            if (heightLock)
            {
                heightP1 = 0;
                heightP2 = 0;
            }

            #region PhaseFalseStuff

            if (phase1)
            {
                moveLock = true;
                guardLock = true;
                deathLock = true;
                heightLock = true;
                lungeLock = true;
            }

            if (phase2)
            {
                phase1 = false;
            }

            if (phase3)
            {
                phase1 = false;
                phase2 = false;
                guardLock = false;
            }

            if (phase4)
            {
                phase1 = false;
                phase3 = false;
                guardLock = false;
            }

            if (phase5)
            {
                phase1 = false;
                phase4 = false;
                guardLock = false;
            }

            if (phase6)
            {
                phase1 = false;
                phase5 = false;
                guardLock = false;
                heightLock = false;
            }

            if (phase7)
            {
                phase1 = false;
                phase6 = false;
                guardLock = false;
                heightLock = false;
            }

            if (phase8)
            {
                phase1 = false;
                phase7 = false;
                guardLock = false;
                heightLock = false;
            }

            if (phase9)
            {
                phase1 = false;
                phase8 = false;
                guardLock = false;
                heightLock = false;
            }

            if (phase10)
            {
                phase1 = false;
                phase9 = false;
                guardLock = false;
                heightLock = false;
            }

            if (phase11)
            {
                phase1 = false;
                phase10 = false;
                guardLock = false;
                heightLock = false;
                moveLock = false;
            }

            if (phase12)
            {
                phase1 = false;
                phase11 = false;
                guardLock = false;
                heightLock = false;
                moveLock = false;
            }

            if (phase13)
            {
                phase1 = false;
                phase12 = false;
                guardLock = false;
                heightLock = false;
                moveLock = false;
                lungeLock = false;
            }

            if (phase14)
            {
                phase1 = false;
                phase13 = false;
                guardLock = false;
                heightLock = false;
                moveLock = false;
                lungeLock = false;
            }

            if (phase15)
            {
                phase1 = false;
                phase14 = false;
                guardLock = false;
                heightLock = false;
                moveLock = false;
                lungeLock = false;
            }

            if (tutorialClear)
            {
                deathLock = false;
            }
            #endregion
        }

        #region Tutorial
        if (phase1)
        {
            TutorialTextController.TTC.texts[0].SetActive(true);

            if (canInteractText && Input.GetKeyUp(KeyCode.Return) || canInteractText && InputManager.IM.GetA(1) || canInteractText && InputManager.IM.GetA(2))
            {
                TutorialTextController.TTC.texts[1].SetActive(true);
                canInteractText = false;
            }
            
            if (TutorialTextController.TTC.texts[1].activeSelf)
            {
                TutorialTextController.TTC.texts[0].SetActive(false);

                if (Input.GetKeyDown(KeyCode.R))
                {
                    P1_OK = true;
                }

                if (P1_OK && Input.GetKeyDown(KeyCode.F))
                {
                    P1_OK = false;
                    P1Clear = true;
                }

                if (InputManager.IM.isKeyboardAndMouseP1 && Input.GetMouseButtonDown(1))
                {
                    P1_OK = true;
                }

                if (P1_OK && InputManager.IM.isKeyboardAndMouseP1 && Input.GetKeyDown(KeyCode.F))
                {
                    P1_OK = false;
                    P1Clear = true;
                }

                if (InputManager.IM.P1_Triggers < 0)
                {
                    P1_OK = true;
                }

                if (P1_OK && InputManager.IM.P1_RB)
                {
                    P1_OK = false;
                    P1Clear = true;
                }

                if (Input.GetKeyDown(KeyCode.RightShift))
                {
                    P2_OK = true;
                }

                if (P2_OK && Input.GetKeyDown(KeyCode.RightControl))
                {
                    P2_OK = false;
                    P2Clear = true;
                }

                if (InputManager.IM.isKeyboardAndMouseP2 && Input.GetMouseButtonDown(1))
                {
                    P2_OK = true;
                }

                if (P2_OK && InputManager.IM.isKeyboardAndMouseP2 && Input.GetKeyDown(KeyCode.F))
                {
                    P2_OK = false;
                    P2Clear = true;
                }

                if (InputManager.IM.P2_Triggers < 0)
                {
                    P2_OK = true;
                }

                if (P2_OK && InputManager.IM.P2_RB)
                {
                    P2_OK = false;
                    P2Clear = true;
                }

                if (P1Clear && P2Clear)
                {
                    timer -= Time.deltaTime;

                    if (timer <= 4.75f)
                    {
                        textTimer = defaultTextTimer;
                        phase2 = true;
                    }
                }
            }
        }

        if (phase2)
        {
            P1Clear = false;
            P2Clear = false;

            TutorialTextController.TTC.texts[1].SetActive(false); 
            TutorialTextController.TTC.texts[2].SetActive(true);
            
            if (TutorialTextController.TTC.texts[2].activeSelf)
            {
                if (Input.GetKey(KeyCode.R) || Input.GetKey(KeyCode.F))
                {
                    inputTimerP1 -= Time.deltaTime;
                }

                else if (InputManager.IM.isKeyboardAndMouseP1 && Input.GetMouseButton(1) || InputManager.IM.isKeyboardAndMouseP1 && Input.GetKey(KeyCode.F))
                {
                    inputTimerP1 -= Time.deltaTime;
                }

                else if (InputManager.IM.P1_Triggers < 0 || InputManager.IM.P1_RB)
                {
                    inputTimerP1 -= Time.deltaTime;
                }

                else
                {
                    inputTimerP1 = defaultInputTimer;
                }

                if (Input.GetKey(KeyCode.RightShift) || Input.GetKey(KeyCode.RightControl))
                {
                    inputTimerP2 -= Time.deltaTime;
                }

                else if (InputManager.IM.isKeyboardAndMouseP2 && Input.GetMouseButton(1) || InputManager.IM.isKeyboardAndMouseP2 && Input.GetKey(KeyCode.F))
                {
                    inputTimerP2 -= Time.deltaTime;
                }

                else if (InputManager.IM.P2_Triggers < 0 || InputManager.IM.P2_RB)
                {
                    inputTimerP2 -= Time.deltaTime;
                }

                else
                {
                    inputTimerP2 = defaultInputTimer;
                }

                if (inputTimerP1 <= 0.2f)
                {
                    P1_OK = true;
                }

                if (inputTimerP2 <= 0.2f)
                {
                    P2_OK = true;
                }

                if (P1_OK && P2_OK)
                {
                    timer -= Time.deltaTime;

                    if (timer <= 4.5f)
                    {
                        inputTimerP1 = defaultInputTimer;
                        inputTimerP2 = defaultInputTimer;
                        P1_OK = false;
                        P2_OK = false;
                        phase3 = true;
                    }
                }
            }
        }

        if (phase3)
        {
            TutorialTextController.TTC.texts[2].SetActive(false);
            TutorialTextController.TTC.texts[3].SetActive(true); //"Well done, sirs! Note that doing a moulinette is only way to generate enough force for any proper damage, lighter attack will only wound your opponent."
            
            if (TutorialTextController.TTC.texts[3].activeSelf)
            {
                if (canInteractText && Input.GetKeyUp(KeyCode.Return) || canInteractText && InputManager.IM.GetA(1) || canInteractText && InputManager.IM.GetA(2))
                {
                    TutorialTextController.TTC.texts[3].SetActive(false);
                    TutorialTextController.TTC.texts[4].SetActive(true); //"When duelling with sabres you can be attacked from two different sides: outside (the side your back is pointing) and inside (the side your chest is pointing)"
                    canInteractText = false;
                }
            }
            
            if (TutorialTextController.TTC.texts[4].activeSelf)
            {
                TutorialTextController.TTC.texts[3].SetActive(false);

                //LAITA KONTROLLERIN INPUTIT MYÖS!!!

                if (Input.GetKeyUp(KeyCode.X) || InputManager.IM.isKeyboardAndMouseP1 && Input.GetMouseButtonDown(1))
                {
                    P1_OK = true;
                }

                if (InputManager.IM.P1_LB || InputManager.IM.P1_Triggers > 0)
                {
                    P1_OK = true;
                }

                if (Input.GetKeyUp(KeyCode.P) || InputManager.IM.isKeyboardAndMouseP2 && Input.GetMouseButtonDown(1))
                {
                    P2_OK = true;
                }

                if (InputManager.IM.P2_LB || InputManager.IM.P2_Triggers > 0)
                {
                    P2_OK = true;
                }

                if (P1_OK && P2_OK)
                {
                    phase4 = true;
                }
            }
        }

        if (phase4)
        {
            TutorialTextController.TTC.texts[4].SetActive(false);
            TutorialTextController.TTC.texts[5].SetActive(true); //"To block the attacks coming you have to make sure that you are guarding the correct side. The side you are guarding in indicated by the colour of your blade"

            if (canInteractText && Input.GetKeyUp(KeyCode.Return) || canInteractText && InputManager.IM.GetA(1) || canInteractText && InputManager.IM.GetA(2))
            {
                TutorialTextController.TTC.texts[6].SetActive(true); //"You can also change between hanging(sword pointing down) and regular(sword pointing up) guard. Now please rotate through each of your guard (The guard changing button or the other guard changing button)""
                canInteractText = false;
            }

            if (TutorialTextController.TTC.texts[6].activeSelf)
            {
                TutorialTextController.TTC.texts[5].SetActive(false);

                if (Input.GetKeyUp(KeyCode.C) || InputManager.IM.isKeyboardAndMouseP1 && Input.GetMouseButtonDown(2))
                {
                    P1Clear = true;
                }

                if (Input.GetKeyUp(KeyCode.I) || InputManager.IM.isKeyboardAndMouseP2 && Input.GetMouseButtonDown(2))
                {
                    P2Clear = true;
                }

                if (InputManager.IM.P1_LB || InputManager.IM.P1_Triggers > 0)
                {
                    P1Clear = true;
                }

                if (InputManager.IM.P2_LB || InputManager.IM.P2_Triggers > 0)
                {
                    P2Clear = true;
                }
            }

            if (P2Clear && P2Clear)
            {
                timer = defaultTimer;
                phase5 = true;
            }
        }

        if (phase5)
        {
            TutorialTextController.TTC.texts[6].SetActive(false);

            P1_OK = false;
            P2_OK = false;
            P1Clear = false;
            P2Clear = false;

            timer -= Time.deltaTime;

            if (timer > 4.5)
            {
                animP1.SetBool("forward", true);
                animP2.SetBool("forward", true);
            }

            if (timer < 2)
            {
                animP1.SetBool("forward", false);
                animP2.SetBool("forward", false);

                TutorialTextController.TTC.texts[7].SetActive(true); //Very good, sirs! On top of affecting the direction you're blocking guards also determine which direction your attacks will come from. Please try hitting each other.

                if (canInteractText && Input.GetKeyUp(KeyCode.Return) || canInteractText && InputManager.IM.GetA(1) || canInteractText && InputManager.IM.GetA(2))
                {
                    timer = defaultTimer;
                    phase6 = true;
                }
            }
        }

        if (phase6)
        {
            TutorialTextController.TTC.texts[7].SetActive(false);

            timer -= Time.deltaTime;

            if (!P1_OK && timer > 4.5)                                    //Players step out of striking distance
            {
                animP1.SetBool("back", true);
                animP2.SetBool("back", true);
            }

            if (!P1_OK && timer < 2)
            {
                animP1.SetBool("back", false);
                animP2.SetBool("back", false);
            }

            if (!animP1.GetBool("back") && !animP2.GetBool("back"))
            {
                TutorialTextController.TTC.texts[8].SetActive(true); //"You can also change your sword’s height (right controller stick, mouse or  t and g for player1 and O and L for player2) to attack and protect on different bodyparts."

                if (heightP1 < 0 || heightP1 > 0)
                {
                    P1_OK = true;
                }

                if (heightP2 < 0 || heightP2 > 0)
                {
                    P2_OK = true;
                }

                if (P1_OK && P2_OK)
                {
                    timer = defaultTimer;
                    phase7 = true;
                }
            }
        }

        if (phase7)
        {
            TutorialTextController.TTC.texts[8].SetActive(false);
            TutorialTextController.TTC.texts[9].SetActive(true); //"Absolutely splendid work sirs! Even if your guard is on correct side, it can't parry the attack if the blade isn't on the way of the attack. Now before we move on to footwork, I want to tell you a bit about what happens when your swords collide."

            P1_OK = false;
            P2_OK = false;
            P1Clear = false;
            P2Clear = false;
            
            if (canInteractText && Input.GetKeyUp(KeyCode.Return) || canInteractText && InputManager.IM.GetA(1) || canInteractText && InputManager.IM.GetA(2))
            {
                TutorialTextController.TTC.texts[9].SetActive(false);
                phase8 = true;
            }
        }

        if (phase8)
        {
            TutorialTextController.TTC.texts[10].SetActive(true); //"Your sword's blade can be divided to roughly two parts in lengthwise: the strong of the blade(The half closer to your hand, used for parrying) and the weak of the blade(the half further from the hand, used for attacking)"

            timer -= Time.deltaTime;

            if (timer < 4)
            {
                if (canInteractText && Input.GetKeyUp(KeyCode.Return) || canInteractText && InputManager.IM.GetA(1) || canInteractText && InputManager.IM.GetA(2))
                {
                    timer = defaultTimer;
                    phase9 = true;
                }
            }
        }

        if (phase9)
        {
            TutorialTextController.TTC.texts[10].SetActive(false);

            timer -= Time.deltaTime;

            if (timer < 4.5)
            {
                animP1.SetBool("forward", true);
                animP2.SetBool("forward", true);
            }

            if (timer < 2)
            {
                animP1.SetBool("forward", false);
                animP2.SetBool("forward", false);

                if (!animP1.GetBool("back") && !animP2.GetBool("back"))
                {
                    TutorialTextController.TTC.texts[11].SetActive(true); //"The laws of leverage dictate that when weak of the blade hits strong the former will bounce back more. How sword collisions end up are also affected by multitude of other factors such as momentum and the direction of the swings.But fun comes from figuring these things yourself.So once more, first to three!"

                    if (canInteractText && Input.GetKeyUp(KeyCode.Return) || canInteractText && InputManager.IM.GetA(1) || canInteractText && InputManager.IM.GetA(2))
                    {
                        timer = defaultTimer;
                        phase10 = true;
                    }
                }
            }
        }

        if (phase10)
        {
            timer -= Time.deltaTime;

            P1Clear = false;

            TutorialTextController.TTC.texts[11].SetActive(false);
            TutorialTextController.TTC.texts[12].SetActive(true); //"Being able to swing your sword is all well and good, but you’ll soon be hit by the swings of your opponent, lest you learn how to move out of the way of those swings."
            
            if (timer < 4)
            {
                if (canInteractText && Input.GetKeyUp(KeyCode.Return) || canInteractText && InputManager.IM.GetA(1) || canInteractText && InputManager.IM.GetA(2))
                {
                    phase11 = true;
                }
            }
        }

        if (phase11)
        {
            TutorialTextController.TTC.texts[12].SetActive(false);
            TutorialTextController.TTC.texts[13].SetActive(true); //"Footwork basicly boils down to stepping forward(tilt left stick towards your opponent, press D or left arrow) and stepping backward(tilt left stick away from your opponent, press A or right arrow).Give me one step in either direction."

            if (animP1.GetBool("forward") || animP1.GetBool("back"))
            {
                P1_OK = true;
            }

            if (animP2.GetBool("forward") || animP2.GetBool("back"))
            {
                P2_OK = true;
            }

            if (P1_OK && P2_OK)
            {
                timer = defaultTimer;
                P1_OK = false;
                P2_OK = false;
                phase12 = true;
            }
        }

        if (phase12)
        {
            TutorialTextController.TTC.texts[13].SetActive(false);
            TutorialTextController.TTC.texts[14].SetActive(true); //"Now I shall teach you some advanced footwork. First show me a leap backwards(Double tap leftstick away from opponent)!"

            if (animP1.GetBool("Jumped"))
            {
                P1_OK = true;
            }

            if (animP2.GetBool("Jumped"))
            {
                P2_OK = true;
            }

            if (P1_OK && P2_OK)
            {
                phase13 = true;
            }
        }

        if (phase13)
        {
            TutorialTextController.TTC.texts[14].SetActive(false);
            TutorialTextController.TTC.texts[15].SetActive(true); //"Clearly you’re no peasants!  And now for for the last and propably the most important move show me a Lunge(Double tap leftstick towards opponent)"

            if (animP1.GetBool("Lunged"))
            {
                P1_OK = false;
                P1Clear = true;
            }

            if (animP2.GetBool("Lunged"))
            {
                P2_OK = false;
                P2Clear = true;
            }

            if (P1Clear && P2Clear)
            {
                TutorialTextController.TTC.texts[15].SetActive(false);
                TutorialTextController.TTC.texts[16].SetActive(true); //"You inbred bastards! You can leave your lunged position by moving forward(move forward) or backward(move backward)"

                phase14 = true;
            }
        }

        if (phase14)
        {
            if (animP1.GetBool("forward") || animP1.GetBool("back"))
            {
                P1_OK = true;
            }

            if (animP2.GetBool("forward") || animP2.GetBool("back"))
            {
                P2_OK = true;
            }

            if (P1_OK && P2_OK)
            {
                TutorialTextController.TTC.texts[16].SetActive(false);
                TutorialTextController.TTC.texts[17].SetActive(true); //"In most cases you want to start preparing your swing just before lunging. Also don’t let yourself get too close to opponent, lest your fight will detoriate to a common brawl!"

                if (canInteractText && Input.GetKeyUp(KeyCode.Return) || canInteractText && InputManager.IM.GetA(1) || canInteractText && InputManager.IM.GetA(2))
                {
                    timer = defaultTimer;
                    phase15 = true;
                }
            }
        }

        if (phase15)
        {
            timer -= defaultTimer;

            TutorialTextController.TTC.texts[17].SetActive(false);
            TutorialTextController.TTC.texts[18].SetActive(true); //"The ideal distance from your opponent is from where you can hit your opponent by lunging but can’t be reached when standing in normally. This is called being at measure."
            
            if (timer < 0)
            {
                if (canInteractText && Input.GetKeyUp(KeyCode.Return) || canInteractText && InputManager.IM.GetA(1) || canInteractText && InputManager.IM.GetA(2))
                {
                    timer = defaultTimer;
                    tutorialClear = true;        //Move to duel mode
                }
            }
        }

        #endregion
    }

    void TutorialExit ()     //IF PLAYER EXITS TUTORIAL, SET BOOLEANS TO FALSE!!!
    {
        if (SceneManager.GetActiveScene().name != "TutorialScene")
        {
            tutorialNotStarted = true;
            phase1 = false;
            moveLock = false;
            guardLock = false;
            deathLock = false;
            heightLock = false;
            lungeLock = false;
            tutorialClear = false;
        }
    }

    void TutorialClear()
    {
        if (tutorialClear)
        {
            deathLock = false;
            TutorialTextController.TTC.texts[18].SetActive(false);
            TutorialTextController.TTC.texts[19].SetActive(true); //"I hope you swift deaths."

            timer -= Time.deltaTime;

            if (timer < 0)
            {
                SceneManager.LoadScene(3);
            }
        }
    }

    void TextInteraction()
    {
        if (!canInteractText)
        {
            textTimer -= Time.deltaTime;

            if (textTimer <= 0)
            {
                textTimer = defaultTextTimer;
                canInteractText = true;
            }
        }
    }
}