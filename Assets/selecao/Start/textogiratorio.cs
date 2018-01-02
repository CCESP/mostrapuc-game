using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textogiratorio : MonoBehaviour
{

    private float MOVE_X_DISTANCE = 40;
    private float MOVE_X_OFFSET = 100;
    private float MOVE_Y_DISTANCE = 20;
    private float ANGLE_SPEED = 0.01f;
    private float RADIUS = 50f;
    private Transform t1;
    private Transform t2;
    private Transform t3;
    private float angle = 0;

    // Use this for initialization
    void Start()
    {
        t1 = transform.Find("t1");
        t2 = transform.Find("t2");
        t3 = transform.Find("t3");
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 p = transform.localPosition;
        p.Set(MOVE_X_OFFSET + Mathf.Sin(Time.time) * MOVE_X_DISTANCE, Mathf.Sin(Time.time) * MOVE_Y_DISTANCE, p.z);
        transform.localPosition = p;

        t1.localPosition = new Vector3(Mathf.Cos(angle) * RADIUS, Mathf.Sin(angle) * RADIUS, 0);
        t2.localPosition = new Vector3(Mathf.Cos(angle + Mathf.PI * 0.666f) * RADIUS, Mathf.Sin(angle + Mathf.PI * 0.666f) * RADIUS, 0);
        t3.localPosition = new Vector3(Mathf.Cos(angle + Mathf.PI * 0.666f * 2) * RADIUS, Mathf.Sin(angle + Mathf.PI * 0.666f * 2) * RADIUS, 0);
        angle += ANGLE_SPEED;
    }
}
