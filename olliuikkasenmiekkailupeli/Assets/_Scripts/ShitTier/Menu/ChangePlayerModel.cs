using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangePlayerModel : MonoBehaviour
{
    Player player;

    [SerializeField]
    bool rotating;
    bool locked;

    [Header("-- Model infos --")]
    public GameObject[] models;
    public int modelCount;
    public float rotSpeed;
    public int activeModel;

    [Header("-- UI --")]
    public Text readyText;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        modelCount = models.Length;
        activeModel = 1;

        readyText.gameObject.SetActive(false);
    }

    IEnumerator RotateMe(Vector3 byAngles, float inTime)
    {
        rotating = true;
        var fromAngle = transform.rotation;
        var toAngle = Quaternion.Euler(transform.eulerAngles + byAngles);
        for (var t = 0f; t < 1; t += Time.deltaTime / inTime)
        {
            transform.rotation = Quaternion.Slerp(fromAngle, toAngle, t);
            yield return null;
        }
        rotating = false;
    }

    void Update()
    {

        if(!rotating && !locked)
        {
            if (player.input.Horizontal >= 1)
            {
                StartCoroutine(RotateMe(Vector3.up * 360 / modelCount, rotSpeed));
                activeModel += 1;
                if(activeModel > modelCount)
                {
                    activeModel = 0;
                }
            }
            if (player.input.Horizontal <= -1)
            {
                StartCoroutine(RotateMe(Vector3.up * 360 / -modelCount, rotSpeed));
                activeModel -= 1;
                if(activeModel < 0)
                {
                    activeModel = modelCount;
                }
            }

            if(player.input.ButtonIsDown())
            {
                locked = true;
                readyText.gameObject.SetActive(true);
                //GameHandler.instance.player1Model = activeModel;
            }
        }
    }
}   
