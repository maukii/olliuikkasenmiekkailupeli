using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyboxRotator : MonoBehaviour
{
    public float speedMultiplier;

    void Start()
    {
        RenderSettings.skybox.SetFloat("_Rotation", 0);
    }

    void Update()
    {
        RenderSettings.skybox.SetFloat("_Rotation", Time.time * speedMultiplier);
    }
}
