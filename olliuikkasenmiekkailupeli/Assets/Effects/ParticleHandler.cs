using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleHandler : MonoBehaviour {

    // This is a script for handling the particles and trails
    // So sword-swoosh-trail, sword-on-sword -spark, and sword-on-player- blood.
    // Dunno how the sword-on-sword and sword-on-player collisions should be checked, since
    // they need to know the collision's position ¯\_(ツ)_/¯

    // How to use:
    // Put this script on the player object
    // Put the particle systems/trails that are always on the same place (pipe smoke, sword swoosh)
    // where they belong (child of object "terä", pipe, etc) and drag them on this script.
    // The other psystemt/trail, just drag them from the assets folder on this script u know 
    // this stuff why am I even telling this.

    // --- Current particle systems and trails explained: ---
    // trailSwoosh = When sword is in "hitting" mode.
    // partSpark = When sword collides with other sword.
    // partBlood = When sword collides with player

    public ParticleSystem partSpark, partBlood;
    public TrailRenderer trailSwoosh;
    HandAnimationControl hcon;

    ParticleSystem.EmissionModule bloodEmi, sparkEmi;
    float swooshDefaultTime;
    public bool swoosh;

    void Start ()
    {
        hcon = GetComponent<HandAnimationControl>();

        // --- STUFF FOR trailSwoosh --- //
        swooshDefaultTime = trailSwoosh.time;           //Gets the default lifetime of the trail
        trailSwoosh.time = 0;                           //Sets the current lifetime to zero

        // ---- STUFF FOR partBlood --- //
        bloodEmi = partBlood.emission;
        //bloodEmi.rate = new ParticleSystem.MinMaxCurve(0.0f, 10.0f);

    }
	
	void Update ()
    {
        #region SwooshCheck
        // --- Check if trailSwoosh should be played --- //
        if(hcon.swordSwinging == true && swoosh)
        {
            trailSwoosh.enabled = true;
            if (trailSwoosh.time == 0)
            {
                trailSwoosh.time = swooshDefaultTime;
            }
        }
        else
        {
            trailSwoosh.enabled = false;
            if (trailSwoosh.time == swooshDefaultTime)
            {

                trailSwoosh.time = 0;

            }
        }

        #endregion

        #region ContactCheck
        // --- Check if partBlood should be instantiated --- //

        // instantiate blood effect on collision.contactpoint[0] (LASSI'S COLLISION SCRIPT)
        // take the direction of the normals and align the rotation of the particle system to that
        // Destroy the instance of the prefab once it's done i has done the sparkly thing
        // if the collision is sword-on-sword = spark
        // if sword-on-player = blood
        #endregion
    }
    public void InstantiateSpark(Vector3 position, Quaternion rotation)
    {
        Instantiate(partSpark, position, rotation);
    }
    public void InstantiateBlood(Vector3 position, Quaternion rotation)
    {
        Instantiate(partBlood, position, rotation);
    }

}
