using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMenuPlayerTag : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {
        //Huom! Kirjoita Tag():iin vasemman ja oikean pelaajan objektien nimet
        Tag();
    }

    void Tag()
    {
        if (InputManager.IM.isLeftP1 && this.gameObject.name == "L") //tähän vasemman nimi
        {
            this.gameObject.tag = "Player 1";
        }

        if (InputManager.IM.isRightP1 && this.gameObject.name == "R") //tähän oikean nimi
        {
            this.gameObject.tag = "Player 1";
        }

        if (InputManager.IM.isLeftP2 && this.gameObject.name == "L") //tähän vasemman nimi
        {
            this.gameObject.tag = "Player 2";
        }

        if (InputManager.IM.isRightP2 && this.gameObject.name == "R") //tähän oikean nimi
        {
            this.gameObject.tag = "Player 2";
        }
    }
}