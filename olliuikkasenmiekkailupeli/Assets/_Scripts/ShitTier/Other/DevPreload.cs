using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DevPreload : MonoBehaviour {

    public int sceneIndex;

     void Awake()
     {
          sceneIndex = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
          
          GameObject check = GameObject.Find("_app");
          if (check == null)
          {
             UnityEngine.SceneManagement.SceneManager.LoadScene("_preload");
             UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);
          }
     }
}
