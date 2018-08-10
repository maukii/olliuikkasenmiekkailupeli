using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager : MonoBehaviour
{
    [Header("---Affects movement---")]
    public bool moveLock;
    public bool strongLock;
    public bool guardLock;
    public bool heightLock;
    public bool lungeLock;

    [Header("---Tutorial phases---")]
    public bool tutorialNotStarted;
    public bool phase1;
    public bool phase2;
    public bool phase3;
    public bool phase4;
    public bool phase5;
    public bool phase6;
    public bool phase7;
    public bool phase8;
    public bool phase9;
    public bool phase10;
    public bool phase11;
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

        if (!tutorialNotStarted)
        {
            TutorialPhases();
            TutorialExit();
        }

    }

    void TutorialPhases ()
    {
        if (!tutorialNotStarted)
        {
            P1 = GameObject.FindGameObjectWithTag("Player 1");
            P2 = GameObject.FindGameObjectWithTag("Player 2");

            P1.GetComponent<AlternativeMovement5>().enabled = false;
            P2.GetComponent<AlternativeMovement5>().enabled = false;

            moveLock = true;
            strongLock = true;
            guardLock = true;
            heightLock = true;
            lungeLock = true;

            phase1 = true;
        }

        if (phase1)
        {
            Phase1(); //Only light vertical and horizontal attack
        }

        if (phase2)
        {
            //Unlock guard changes
            guardLock = false;
        }

        if (phase3)
        {
            //Players step in striking distance
            //Damage animations play but players don’t get hurt
        }

        if (phase4)
        {
            //Players step out of striking distance
            //Moulinettes are unlocked
            strongLock = false;
        }

        if (phase5)
        {
            //Players step in striking distance
            //Instead of death animations play normal damage animations 
        }

        if (phase6)
        {
            //Players step out of striking distance
        }

        if (phase7)
        {
            //Sword height gets unlocked
            heightLock = false;
        }

        if (phase8)
        {
            //Players step in striking distance
        }

        if (phase9)
        {
            //Normal steps get unlocked
            moveLock = false;
        }

        if (phase10)
        {
            //Players Get introduced to basic footwork
            //Unlock backward leap
        }

        if (phase11)
        {
            lungeLock = false; //Unlock lunge
            //Everything is unlocked
        }

        if (tutorialClear)
        {
            //Move to duel mode
        }
    }

    void TutorialExit ()     //IF PLAYER EXITS TUTORIAL, SET BOOLEANS TO FALSE!!!
    {
        if (MainMenuController.MMC.isTutorial == false)
        {
            moveLock = false;
            strongLock = false;
            guardLock = false;
            heightLock = false;
            lungeLock = false;
            phase1 = false;
            phase2 = false;
            phase3 = false;
            phase4 = false;
            phase5 = false;
            phase6 = false;
            phase7 = false;
            phase8 = false;
            phase9 = false;
            phase10 = false;
            phase11 = false;
            tutorialClear = false;
        }
    }

    void Phase1 ()
    {

    }
}
/*
1. Players start out of striking distance from eachother and can  only do light vertical and horizontal attack 

                    "Welcome noble gentlemen! Today I shall teach you how to defend your honour." 

                    "Let’s start with doing a vertical attack (Vertical attack button) and a horizontal attack (Horizontal attack button)." 

                    "Well done sirs! When duelling with sabres you can be attacked from two different sides: outside (the side your back is pointing) and inside (the side your chest is pointing)" 

2. Unlock guard changes 

                    "To block the attacks coming you have to make sure that you are guarding the correct side. The side you are guarding in indicated by the colour of your blade" 

                    "You can also change between hanging(sword pointing down) and regular(sword pointing up) guard" 

                    "Now please rotate through each of your guard (The guard changing button or the other guard changing button)" 

3. Players step in striking distance 

4. Damage animations play but players don’t get hurt 

                    "Very good sirs! On top of affecting the direction you're blocking guards also determine which direction your attacks will come from. Please try hitting eachother untill one of you has landed three hits" 

5. Players step out of striking distance 

6. Moulinettes are unlocked 

                    "You never seize to amaze sirs! You can also spin your sword around and do an attack from the opposite direction(Hold down attack button). This technique is called a moulinette." 

                    "Just marvelous! Note that doing a moulinette is only way to generate enough force for a fatal blow, lighter attack will only wound your opponent." 

7. Players step in striking distance 

8. Instead of death animations play normal damage animations 

                    "And now, as usual, you get to try out what you've learned on eachother. First to three moulinette hits!" 

9. Players step out of striking distance 

                    "Now that you've become adept at attacking and defending from different sides, we'll add a new factor to our equation: Sword height!" 

10. Sword height gets unlocked 

                    "Use your (right controller stick, mouse or  t and g for player1 and O and L for player2) to change the height of your sword now." 

                    "Absolutely splendid work sirs! The height of your sword affects both where your attack will land and where you are blocking. Even if your guard is on correct side, it can't parry the attack if the blade isn't on the way of the attack." 

                    "Now before we move on to footwork, I want tell you a bit about what happens when your swords collide." 

                    "Your sword's blade can be divided to roughly two parts in lengthwise: the forte(The half closer to your hand, used for parrying) and the foible(the half further from the hand, used for attacking)" 

                    "The laws of leverage dictate that when foible hits forte  the former will bounce back more" 

11. Players step in striking distance 

                    ”How sword collisions end up are also affected by multitude of   other factors such as momentum and the direction of the swings. But fun comes from figuring these things yourself. So once more, first to three!” 

12. Normal steps get unlocked 

                    ”Being able to swing your sword is all well and good, but you’ll soon be hit by the swings of your opponent, lest you learn how to move out of the way of those swings.” 

                    ”Footwork basicly boils down to stepping forward(tilt left stick towards your opponent, press D or left arrow) and stepping backward(tilt left stick away from your opponent, press A or right arrow). Give me one step in either direction.” 

                    ”Very impressive! You can also fool your opponents by cancelling your steps (While making the step tilt the right stick to opposite direction/press the opposite movement key), Can you show me a cancelled step in both directions?” 

                    ”Clearly you’re no peasants! Now test what you’ve learned with by sparring with first to three hits and we can move to final lessons!” 

13. Players Get introduced to basic footwork 

14. Unlock bacward leap 

                    ”Now  I shall teach you some advanced footwork. First show me a leap backwards(Double tap leftstick away from opponent)!” 

15. Unlock lunge 

                    ”You never seize to amaze! And now for for the last and propably the most important move show me a Lunge(Double tap leftstick towards opponent)” 

                    “You inbred bastards! You can leave your lunged position by moving forward(move forward) or backward(move backward)” 

                    ” In most cases you want to start preparing your swing just before lunging.” 

                    ”The ideal distance from your opponent is from where you can hit your opponent by lunging but can’t be reached when standing in normally. This is called being at measure.” 

                    ”I hope you swift deaths.” 

16. Move to duel mode  
 */
