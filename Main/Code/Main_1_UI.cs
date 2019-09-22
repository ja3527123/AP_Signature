using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_1_UI : MonoBehaviour
{
    // public DragButt Butt;
    public Tween Tw;

    void Start()
    {
        // Butt.Start_Del += (v3) =>
        // {
        //     LoadObj = Load ();
        //     StartCoroutine (LoadObj);
        //     Tw.enabled = true;
        // };
        // Butt.End_Del += (v3) =>
        // {
        //     StopCoroutine (LoadObj);
        //     LoadObj = null;
        //     Tw.enabled = false;
        // };
    }

    // IEnumerator LoadObj;
    // IEnumerator Load ()
    // {
    //     yield return new WaitForSeconds (3);
    //     Main_UI._.NextObj ();
    // }

    float StartTime = -1;

    void Update ()
    {
        if (Input.GetMouseButton (0) != Tw.enabled)
        {
            Tw.enabled = Input.GetMouseButton (0);
            if (Input.GetMouseButton (0))
            {
                StartTime = Time.time;
            }
        }
        if (Tw.enabled && StartTime + 3 < Time.time)
        {
            Main_UI._.NextObj ();
        }
    }
}
