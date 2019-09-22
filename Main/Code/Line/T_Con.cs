using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T_Con : MonoBehaviour
{
    //static public List <T_Con> All_T_Con;

    public Image Test_Im;

    public bool IsFirst;//筆畫中的第一個
    public bool IsLast;//筆畫中的最後一個
    public bool IsOK_0 => SpLineCon._.NowT == this;//可以觸發
    public bool IsOK_1;//已經觸發
    public bool IsOK_2;//觸發後已離開

    //public Action WinEvent;//勝利時會發生的事件
    //public Action FallEvent;//失敗時會發生的事件

    void Start ()
    {
        int s = (int)GetComponent<CircleCollider2D> ().radius;
        Test_Im.rectTransform.sizeDelta = new Vector2 (s * 2, s * 2);
    }

    void Update ()
    {
        if (IsOK_1)
        {
            if (!IsOK_2)
            {
                Test_Im.color = Color.gray;
            }
            else
            {
                Test_Im.color = Color.black;
            }
        }
        else if (IsFirst || IsLast)
        {
            if (IsOK_0)
            {
                Test_Im.color = Color.yellow;
            }
            else
            {
                Test_Im.color = Color.red;
            }
        }
        else
        {
            if (IsOK_0)
            {
                Test_Im.color = Color.green;
            }
            else
            {
                Test_Im.color = Color.blue;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Mouse")
        {
            //print ("進入"+name);
            if (IsOK_0)
            {
                IsOK_1 = true;
                if (!IsLast)//如果是筆畫的最後一節就改成放開時才去找最後一節
                {
                    SpLineCon._.LoadNext();
                }
            }
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Out")
        {
            //print ("離開"+name);
            if (IsOK_0)
            {
                if (IsOK_1)
                {
                    IsOK_2 = true;
                }
                else
                {
                    if (SpLineCon._.FallEvent != null) SpLineCon._.FallEvent();
                }
            }
            else if (IsOK_1)
            {
                IsOK_2 = true;
            }
        }
    }
}
