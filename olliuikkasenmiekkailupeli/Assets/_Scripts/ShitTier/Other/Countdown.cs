using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

    public int timeLeft = 3;
    public Text counter;

    private void Start()
    {
        counter.enabled = true;
        counter.text = timeLeft.ToString();
    }

    private void Update()
    {
        if(timeLeft > 0)
        {
            counter.text = timeLeft.ToString();
        }
    }

    public void LoverTime()
    {
        timeLeft--;
        if(timeLeft == 0)
        {
            counter.fontSize = 200;
            counter.text = "En Garde!";
        }
    }

    public void Disable()
    {
        counter.gameObject.SetActive(false);
    }

}
