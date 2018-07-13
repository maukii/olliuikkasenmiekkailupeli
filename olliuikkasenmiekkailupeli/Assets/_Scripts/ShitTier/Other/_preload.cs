using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _preload : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        Debug.Log("DOL " + gameObject.name);
    }

    private void Start()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
