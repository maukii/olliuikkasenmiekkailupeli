using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodypartHealth : MonoBehaviour
{
    [Header("--- Bodypart info ---")]
    public float maxHealth;
    public float health;

    private void Start()
    {
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
    }

}
