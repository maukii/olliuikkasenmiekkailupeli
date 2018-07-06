using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int playerNumber;
    public Color color;
    public PlayerInput input { get; set; }

    private void Start()
    {
        input = GetComponent<PlayerInput>();
    }

}
