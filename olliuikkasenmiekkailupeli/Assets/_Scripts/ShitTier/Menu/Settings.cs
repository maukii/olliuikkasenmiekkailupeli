using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    Animator anim;
    public GameObject mainMenuUI, settingsUI;
    public BetterMainMenu script;
    public Settings settings;

    [SerializeField]
    float ver, hor;

    void Start()
    {
        script = FindObjectOfType<BetterMainMenu>();
        anim = Camera.main.GetComponent<Animator>();
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Master);
    }

    public void SetMusicVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Music);
    }

    public void SetSfxVolume(float value)
    {
        AudioManager.instance.SetVolume(value, AudioManager.AudioChannel.Sfx);
    }

    public void BackToMenu()
    {
        anim.SetBool("SettingsMenu", false);
        anim.SetBool("MainMenu", true);

        script.enabled = true;
        settings.enabled = false;
    }

}
