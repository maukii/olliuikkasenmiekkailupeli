using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public static LevelChanger instance = null;

    [SerializeField] Animator anim;

    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        if (instance == this)
            DontDestroyOnLoad(gameObject);
    }

    private void OnLevelWasLoaded(int level)
    {
        Debug.Log(level);
        anim.ResetTrigger("FadeOut");

        if (level != 0)
            anim.SetTrigger("FadeIn");
    }

    public void FadeToNextLevel()
    {
        anim.SetTrigger("FadeOut");
        anim.ResetTrigger("FadeIn");
    }

    public void FadeOutComplite()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToMain()
    {
        anim.SetTrigger("FadeToMain");
        anim.ResetTrigger("FadeIn");
        anim.ResetTrigger("FadeOut");
    }

    public void FadeToCharacterSelect()
    {
        anim.SetTrigger("FadeToCharacterSelect");
        anim.ResetTrigger("FadeToMain");
        anim.ResetTrigger("FadeIn");
        anim.ResetTrigger("FadeOut");
    }

    public void FadeToCharacterSelectComplite()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void FadeToMainComplite()
    {
        SceneManager.LoadScene(0);
    }

}
