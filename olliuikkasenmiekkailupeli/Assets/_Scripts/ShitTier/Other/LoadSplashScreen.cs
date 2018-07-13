using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSplashScreen : MonoBehaviour {

	void Update ()
    {
        if(Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            Reload();
        }
	}

    void Reload()
    {
        SceneManager.LoadScene(0);
    }
	
}
