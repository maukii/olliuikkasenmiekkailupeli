using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnSceneLoaded : MonoBehaviour
{
    public List<GameObject> players = new List<GameObject>();
    int modelIndex;

    public GameObject fade;
    Animator countdown;
    public float playerFreezeTimer = 3;
    float reset;
    bool timerStarted;

    void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    private void Start()
    {
        countdown = GameObject.Find("Countdown").gameObject.GetComponent<Animator>();
        countdown.SetTrigger("CountDown");
    }

    public void SceneLoaded(Scene scene, LoadSceneMode mode)
    {
        reset = playerFreezeTimer;

        fade.GetComponent<Animator>().Play("FadeOut");

        for (int i = 0; i < players.Count; i++)
        {
            players[i].gameObject.GetComponent<AlternativeMovement5>().enabled = false;
        }


        for (int i = 0; i < players.Count; i++)
        { 
            if(players[i].name == "P1") // or tag
            {
                players[i].GetComponent<ChooseIngameModel>().ChooseModel(GameHandler.instance.GetPlayer1Model());
            }
            else if(players[i].name == "P2")
            {
                players[i].GetComponent<ChooseIngameModel>().ChooseModel(GameHandler.instance.GetPlayer2Model());
            }
        }

        InputManager.IM.SetCorrectInputs();
        timerStarted = true;

        if(FindObjectOfType<AudioManager>() != null)
        {
            AudioManager.instance.GetComponent<MusicManager>().PlayMusic();
            AudioManager.instance.FadeInMusic();
        }
    }

    void Update()
    {
        if(timerStarted)
        {
            playerFreezeTimer -= Time.deltaTime;
            if (playerFreezeTimer <= 0f)
            {

                Debug.Log("FIGHT!!");

                for (int i = 0; i < players.Count; i++)
                {
                    players[i].gameObject.GetComponent<AlternativeMovement5>().enabled = true;
                }

                playerFreezeTimer = reset;
                timerStarted = false;

                GameHandler.instance.BattleStarted = true;
            }
        }
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneLoaded;
    }

}
