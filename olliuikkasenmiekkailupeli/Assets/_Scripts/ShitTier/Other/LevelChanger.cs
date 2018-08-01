using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{

    public static LevelChanger instance = null;

    [SerializeField] Animator anim;
    private int levelToLoad;

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
        anim.ResetTrigger("FadeOut");
        anim.SetTrigger("FadeIn");
    }

    public void FadeToNextLevel()
    {
        FadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FadeToLevel(int levelIndex)
    {
        levelToLoad = levelIndex;
        anim.SetTrigger("FadeOut");
    }

    public void OnFadeComplite()
    {
        SceneManager.LoadScene(levelToLoad);
    }

}
