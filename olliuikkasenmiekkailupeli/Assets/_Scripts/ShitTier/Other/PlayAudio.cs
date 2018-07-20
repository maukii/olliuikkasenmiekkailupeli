using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour {

	public void PlaySoundPlz()
    {
        AudioManager.instance.PlaySound("uuf");
    }
}
