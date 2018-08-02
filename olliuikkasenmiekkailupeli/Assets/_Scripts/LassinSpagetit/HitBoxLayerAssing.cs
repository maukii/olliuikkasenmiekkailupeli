using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBoxLayerAssing : MonoBehaviour {

	
	void Start () {
        BoxCollider[] col;
        col = GetComponentsInChildren<BoxCollider>();
        if(transform.parent.name == "P1")
        {
            for (int i = 0; i < col.Length;i++)
            {
                if(col[i].transform.parent.name == "SwordCol")
                {
                    col[i].gameObject.layer = 9;
                    col[i].transform.parent.gameObject.layer = 9;
                }
                else
                {
                    col[i].gameObject.layer = 10;
                }
                
            }
        }
        else
        {
            for (int i = 0; i < col.Length; i++)
            {
                if (col[i].transform.parent.name == "SwordCol")
                {
                    col[i].gameObject.layer = 11;
                    col[i].transform.parent.gameObject.layer = 11;
                }
                else
                {
                    col[i].gameObject.layer = 12;
                }
            }
            SwordPart[] sp = gameObject.GetComponentsInChildren<SwordPart>();
            
            for(int i = 0; i < sp.Length; i++)
            {
                sp[i].ChangePlayer(2);
            }
            
        }
        
	}
}
