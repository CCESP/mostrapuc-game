using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour
{

    public GameObject player;
    private float bgSizeX = 39.45f;
    private Transform bg1;
    private Transform bg2;

    // Use this for initialization
    void Start()
    {
        bg1 = transform.Find("bg1");
        bg2 = transform.Find("bg2");
    }

    // Update is called once per frame
    void Update()
    {
        float playerX = player.transform.position.x;

        if ((bg1.transform.position.x + bgSizeX / 2) < playerX) {
            bg1.transform.localPosition = new Vector3(bg2.transform.localPosition.x, bg2.transform.localPosition.y, bg2.transform.localPosition.z);
            bg2.transform.localPosition = new Vector3(bg1.transform.localPosition.x + bgSizeX, bg1.transform.localPosition.y, bg1.transform.localPosition.z); 
        }
    }
}
