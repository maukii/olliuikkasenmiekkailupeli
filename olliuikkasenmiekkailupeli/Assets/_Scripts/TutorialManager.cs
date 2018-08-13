using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    public float defaultTimer, timer = 5f;
    public float defaultInputTimer, inputTimerP1 = 0.5f, inputTimerP2 = 0.5f;

    [Header("---Affects movement---")]
    public bool moveLock;
    public bool guardLock;
    public bool heightLock;
    public bool lungeLock;

    [Header("---Checks for player input---")]
    public bool P1_OK;
    public bool P2_OK;
    public bool P1Clear;
    public bool P2Clear;

    [Header("---Tutorial phases---")]
    public bool tutorialNotStarted;
    public bool tutorialClear;

    [SerializeField]
    bool phase1, phase2, phase3, phase4, phase5, phase6, phase7, phase8, phase9;

    [SerializeField]
    float hangingP1, hangingP2, insideP1, insideP2;     //FOR TEST PURPOSES
    
    Animator animP1, animP2;
    InputManager im;
    GameObject P1, P2;

    public static TutorialManager TM;

    void Start ()
    {
        im = FindObjectOfType<InputManager>();
        TM = this;
        tutorialNotStarted = true;
        defaultTimer = timer;
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

        TutorialExit();
    }

    void TutorialPhases ()
    {
        if (!tutorialNotStarted && MainMenuController.MMC.isTutorial)
        {
            P1 = GameObject.FindGameObjectWithTag("Player 1");
            P2 = GameObject.FindGameObjectWithTag("Player 2");

            P1.GetComponent<AlternativeMovement5>().enabled = false;
            P2.GetComponent<AlternativeMovement5>().enabled = false;

            animP1 = P1.GetComponentInChildren<Animator>();
            animP2 = P2.GetComponentInChildren<Animator>();

            hangingP1 = animP1.GetFloat("Hanging");
            insideP1 = animP1.GetFloat("Inside");

            hangingP2 = animP2.GetFloat("Hanging");
            insideP2 = animP2.GetFloat("Inside");

            moveLock = true;
            guardLock = true;
            heightLock = true;
            lungeLock = true;

            phase1 = true;

            #region PhaseFalseStuff

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
            }

            if (phase5)
            {
                phase1 = false;
                phase4 = false;
            }

            if (phase6)
            {
                phase1 = false;
                phase5 = false;
                heightLock = false;
            }

            if (phase7)
            {
                phase1 = false;
                phase6 = false;
                moveLock = false;
            }

            if (phase8)
            {
                phase1 = false;
                phase7 = false;
            }

            if (phase9)
            {
                phase1 = false;
                phase8 = false;
                lungeLock = false;
            }
            #endregion
        }

        #region Tutorial
        if (phase1)
        {
            //READY FOR TEXTS

            /*
            Players start out of striking distance from eachother and can only do light vertical and horizontal attack

            "Welcome noble gentlemen! Today I shall teach you how to defend your honour."

            "Let’s start with doing a vertical attack (Vertical attack button) and a horizontal attack (Horizontal attack button)."
            */

            if (animP1.GetBool("SwingDia"))
            {
                P1_OK = true;
            }

            if (P1_OK && animP1.GetBool("SwingHor"))
            {
                P1_OK = false;
                P1Clear = true;
            }

            if (animP2.GetBool("SwingDia"))
            {
                P2_OK = true;
            }

            if (P2_OK && animP2.GetBool("SwingHor"))
            {
                P2_OK = false;
                P2Clear = true;
            }

            if (P1Clear && P2Clear)
            {
                timer -= Time.deltaTime;

                if (timer <= 4.75f)
                {
                    phase2 = true;
                }
            }
        }

        if (phase2)
        {
            //READY FOR TEXTS

            P1Clear = false;
            P2Clear = false;

            /*
            Moulinettes are unlocked

            "You never seize to amaze sirs! You can also spin your sword around and do an attack from the opposite direction(Hold down attack button). This technique is called a moulinette."
            */

            if (animP1.GetBool("Strong"))
            {
                inputTimerP1 -= Time.deltaTime;
            }

            if (animP2.GetBool("Strong"))
            {
                inputTimerP2 -= Time.deltaTime;
            }

            if (!animP1.GetBool("Strong"))
            {
                inputTimerP1 = defaultInputTimer;
            }

            if (!animP2.GetBool("Strong"))
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

        if (phase3)     //NEEDS SOME FIXING
        {
            P1_OK = false;
            P2_OK = false;

            /*
            "Just marvelous! Note that doing a moulinette is only way to generate enough force for a fatal blow, lighter attack will only wound your opponent."

            "Well done sirs! When duelling with sabres you can be attacked from two different sides: outside (the side your back is pointing) and inside (the side your chest is pointing)"

            Unlock guard changes
            */


            /*

            "To block the attacks coming you have to make sure that you are guarding the correct side. The side you are guarding in indicated by the colour of your blade"

            "You can also change between hanging(sword pointing down) and regular(sword pointing up) guard"

            "Now please rotate through each of your guard (The guard changing button or the other guard changing button)"
            */


            if (Input.anyKey && hangingP1 == 0f || insideP1 == 0f)
            {
                if (hangingP1 == 1f)
                {
                    P1_OK = true;
                }

                if (insideP1 == 1f)
                {
                    P1_OK = true;
                }
            }

            if (Input.anyKey && hangingP1 == 1f || insideP1 == 1f)
            {
                if (hangingP1 == 0f)
                {
                    P1_OK = true;
                }

                if (insideP1 == 0f)
                {
                    P1_OK = true;
                }
            }

            if (Input.anyKey && hangingP2 == 0f || insideP2 == 0f)
            {
                if (hangingP2 == 1f)
                {
                    P2_OK = true;
                }

                if (insideP2 == 1f)
                {
                    P2_OK = true;
                }
            }

            if (Input.anyKey && hangingP2 == 1f || insideP2 == 1f)
            {
                if (hangingP2 == 0f)
                {
                    P1_OK = true;
                }

                if (insideP2 == 0f)
                {
                    P1_OK = true;
                }
            }

            if (P1_OK && P2_OK)
            {
                phase4 = true;
            }
        }

        if (phase4)
        {
            P1_OK = false;
            P2_OK = false;

            timer = timer - Time.deltaTime;

            if (timer > 2)
            {
                animP1.SetBool("forward", true);
                animP2.SetBool("forward", true);
            }

            if (timer < 1.5f)
            {
                animP1.SetBool("forward", false);
                animP2.SetBool("forward", false);
            }



            /*
            Players step in striking distance

            Damage animations play but players don’t get hurt 

            "Very good sirs! On top of affecting the direction you're blocking guards also determine which direction your attacks will come from. Please try hitting eachother untill one of you has landed three hits"
            */
        }

        if (phase5)
        {
            /*
            Players step out of striking distance 

            Players step in striking distance 

            Instead of death animations play normal damage animations 

            "And now, as usual, you get to try out what you've learned on eachother. First to three moulinette hits!" 
            */
        }

        if (phase6)
        {

            /*
            Players step out of striking distance

            "Now that you've become adept at attacking and defending from different sides, we'll add a new factor to our equation: Sword height!"

            Sword height gets unlocked

            "Use your (right controller stick, mouse or  t and g for player1 and O and L for player2) to change the height of your sword now."

            "Absolutely splendid work sirs! The height of your sword affects both where your attack will land and where you are blocking. Even if your guard is on correct side, it can't parry the attack if the blade isn't on the way of the attack."

            "Now before we move on to footwork, I want tell you a bit about what happens when your swords collide."

            "Your sword's blade can be divided to roughly two parts in lengthwise: the forte(The half closer to your hand, used for parrying) and the foible(the half further from the hand, used for attacking)"

            "The laws of leverage dictate that when foible hits forte  the former will bounce back more"
             */
        }

        if (phase7)
        {

            /*
            Players step in striking distance 

            ”How sword collisions end up are also affected by multitude of   other factors such as momentum and the direction of the swings. But fun comes from figuring these things yourself. So once more, first to three!” 

            Normal steps get unlocked 

            ”Being able to swing your sword is all well and good, but you’ll soon be hit by the swings of your opponent, lest you learn how to move out of the way of those swings.” 

            ”Footwork basicly boils down to stepping forward(tilt left stick towards your opponent, press D or left arrow) and stepping backward(tilt left stick away from your opponent, press A or right arrow). Give me one step in either direction.” 

            ”Very impressive! You can also fool your opponents by cancelling your steps (While making the step tilt the right stick to opposite direction/press the opposite movement key), Can you show me a cancelled step in both directions?” 

            ”Clearly you’re no peasants! Now test what you’ve learned with by sparring with first to three hits and we can move to final lessons!”
            */
        }

        if (phase8)
        {
            /*
            Players Get introduced to basic footwork

            Unlock bacward leap 

            ”Now I shall teach you some advanced footwork. First show me a leap backwards(Double tap leftstick away from opponent)!” 
            */
        }

        if (phase9)
        {

            /*
            Unlock lunge 

            ”You never seize to amaze!And now for for the last and propably the most important move show me a Lunge(Double tap leftstick towards opponent)” 

            “You inbred bastards!You can leave your lunged position by moving forward(move forward) or backward(move backward)” 

            ” In most cases you want to start preparing your swing just before lunging.” 

            ”The ideal distance from your opponent is from where you can hit your opponent by lunging but can’t be reached when standing in normally.This is called being at measure.” 

            ”I hope you swift deaths.” 
            */
        }
        #endregion
    }

    void TutorialExit ()     //IF PLAYER EXITS TUTORIAL, SET BOOLEANS TO FALSE!!!
    {
        if (SceneManager.GetActiveScene().name != "TutorialScene")
        {
            tutorialNotStarted = true;
            moveLock = false;
            guardLock = false;
            heightLock = false;
            lungeLock = false;
            tutorialClear = false;
        }
    }

    void TutorialClear()
    {
        //Move to duel mode
    }
}