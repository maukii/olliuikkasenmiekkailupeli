using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnLoad : MonoBehaviour {

    public void OnEnable()
    {
        if(FindObjectOfType<AudioManager>() != null)
        {
            //AudioManager.instance.GetComponent<MusicManager>().PlayMusic();
            //AudioManager.instance.FadeInMusic();
            //Debug.Log("animate music in");
        }
    }
}
