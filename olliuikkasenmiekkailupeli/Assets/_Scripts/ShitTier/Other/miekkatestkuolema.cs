using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class miekkatestkuolema : MonoBehaviour {

    Animator anim;

    [SerializeField]
    GameObject[] swords;

	void Start () {
        anim = GetComponent<Animator>();
	}
	
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space))
        {
            for (int i = 0; i < swords.Length; i++)
            {
                swords[i].gameObject.SetActive(false);
            }
            anim.SetTrigger("Die");
        }
	}
}
