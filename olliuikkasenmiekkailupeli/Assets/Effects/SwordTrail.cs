using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordTrailPart : MonoBehaviour {

    ParticleSystem ps;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        ps.Stop();
    }

    private void Update()
    {
        if (Input.GetKey("up"))
        {
            Debug.Log("Swing starts");
            HitStart();
        }
        else if (Input.GetKey("down"))
        {
            Debug.Log("Swing ends");
            HitEnd();
        }
    }

    public void HitStart()
    {
        ps.Play();
        var main = ps.main;
        main.loop = true;
    }

    public void HitEnd()
    {
        var main = ps.main;
        main.loop = false;
        ps.Stop();
    }
}
