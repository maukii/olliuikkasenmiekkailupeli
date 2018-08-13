using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [Header("---Affects movement---")]
    public bool moveLock;
    public bool guardLock;
    public bool heightLock;
    public bool lungeLock;

    [Header("---Tutorial phases---")]
    public bool tutorialNotStarted;
    public bool tutorialClear;
    
    InputManager im;
    GameObject P1, P2;

    public static TutorialManager TM;

    void Start ()
    {
        im = FindObjectOfType<InputManager>();
        TM = this;
        tutorialNotStarted = true;
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

            moveLock = true;
            guardLock = true;
            heightLock = true;
            lungeLock = true;

            Phase1();
        }
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

    void Phase1 ()
    {
        /*
        Players start out of striking distance from eachother and can only do light vertical and horizontal attack

        "Welcome noble gentlemen! Today I shall teach you how to defend your honour."

        "Let’s start with doing a vertical attack (Vertical attack button) and a horizontal attack (Horizontal attack button)."
        
        Moulinettes are unlocked

        "You never seize to amaze sirs! You can also spin your sword around and do an attack from the opposite direction(Hold down attack button). This technique is called a moulinette."

        "Just marvelous! Note that doing a moulinette is only way to generate enough force for a fatal blow, lighter attack will only wound your opponent."

        "Well done sirs! When duelling with sabres you can be attacked from two different sides: outside (the side your back is pointing) and inside (the side your chest is pointing)"
         */
    }

    void Phase2 ()
    {
        guardLock = false;

        /*
        Unlock guard changes
        
        "To block the attacks coming you have to make sure that you are guarding the correct side. The side you are guarding in indicated by the colour of your blade"
        
        "You can also change between hanging(sword pointing down) and regular(sword pointing up) guard"
        
        "Now please rotate through each of your guard (The guard changing button or the other guard changing button)"
        */
    }

    void Phase3 ()
    {
        /*
        Players step in striking distance

        Damage animations play but players don’t get hurt 

        "Very good sirs! On top of affecting the direction you're blocking guards also determine which direction your attacks will come from. Please try hitting eachother untill one of you has landed three hits"
        */
    }

    void Phase4 ()
    {
        /*
        Players step out of striking distance 

        Players step in striking distance 

        Instead of death animations play normal damage animations 

        "And now, as usual, you get to try out what you've learned on eachother. First to three moulinette hits!" 
        */
    }

    void Phase5 ()
    {
        heightLock = false;

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

    void Phase6 ()
    {
        moveLock = false;

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

    void Phase7 ()
    {
        /*
        Players Get introduced to basic footwork

        Unlock bacward leap 

        ”Now I shall teach you some advanced footwork. First show me a leap backwards(Double tap leftstick away from opponent)!” 
        */
    }

    void Phase8()
    {
        lungeLock = false;

        /*
        Unlock lunge 

        ”You never seize to amaze!And now for for the last and propably the most important move show me a Lunge(Double tap leftstick towards opponent)” 

        “You inbred bastards!You can leave your lunged position by moving forward(move forward) or backward(move backward)” 

        ” In most cases you want to start preparing your swing just before lunging.” 

        ”The ideal distance from your opponent is from where you can hit your opponent by lunging but can’t be reached when standing in normally.This is called being at measure.” 

        ”I hope you swift deaths.” 
        */
    }

    void TutorialClear()
    {
        //Move to duel mode
    }
}