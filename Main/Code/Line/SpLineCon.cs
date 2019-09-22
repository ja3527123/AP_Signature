using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpLineCon : MonoBehaviour
{
    //還存在的ＢＵＧ
    //1.畫下去時如果落點不再黃點上不會產生錯誤
    //2.畫到筆畫的最後一點只要不放開就能隨便亂華
    static public SpLineCon _;

    public Text Text;

    public LineRenderer LineObj;
    public T_Con TObj;

    public DragButt DragButt;
    public Transform Ts;
    public List <LineRenderer> Lines = new List<LineRenderer> ();
    public LineRenderer NowLine => Lines [Lines.Count - 1];

    public List <T_Con> Strokes;//座標點
    public T_Con NowT;

    public Transform Mouse;//座標碰撞器

    public Word_Data Data;

    public Action FallEvent;//失敗時會發生的事件
    public Action WinEvent;//勝利時會發生的事件

    void Awake ()
    {
        _ = this;
    }

    void Start()
    {

        void SetPos (Vector3 v3)
        {
            if (Lines.Count <= 0) return;

            NowLine.positionCount++;
            NowLine.SetPosition (NowLine.positionCount - 1,v3);
            Mouse.position = v3;
        }
        DragButt.Start_Del += (v3) =>
        {
            Mouse.gameObject.SetActive (true);
            GameObject obj = MyCalculate.SpObj (LineObj.gameObject, transform, Vector3.zero);
            obj.transform.position = v3;
            Lines.Add (obj.GetComponent <LineRenderer>());

            SetPos (v3);
        };
        DragButt.Update_Del += (v3) =>
        {
            SetPos (v3);
        };
        DragButt.End_Del += (v3) =>
        {
            if (NowT.IsLast)
            {
                StartCoroutine (_End_Del());
            }
            Mouse.gameObject.SetActive (false);
        };
        IEnumerator _End_Del()//跳到下一畫必須要晚一偵執行避免半段錯誤
        {
            yield return null;
            NowT.IsOK_2 = true;
            LoadNext ();
            // print ("x");
        }

//==========================================================================================
        IEnumerator OX (string text)
        {
            Text.GetComponentsInChildren<Text> ()[1].text = text;
            yield return new WaitForSeconds (1);
            Text.GetComponentsInChildren<Text> ()[1].text = "";
        }
        FallEvent += () =>
        {
            print ("失敗！");
            StartCoroutine (OX("X"));
            ResetLine ();
        };
        WinEvent += () =>
        {
            print ("勝利！");
            StartCoroutine (OX("O"));
            ResetLine ();
        };
        //Open (Data);
    }

    void Update ()
    {
        if (Input.GetKeyUp ("r"))
        {
            ResetLine ();
        }
    }

    public void Open (Word_Data Data)
    {
        this.Data = Data;

        Strokes = new List<T_Con> ();
        for (int i = 0; i < Data.Strokes.Length; i++)
        {
            for (int j = 0; j < Data.Strokes [i].Pot.Length; j++)
            {
                Vector2 V2 = Data.Strokes [i].Pot [j];

                Rect rect = ((RectTransform)transform).rect;
                V2 = new Vector2 (V2.x * rect.width / 2, V2.y * rect.height / 2);
                T_Con g = Instantiate<T_Con> (TObj, Ts);
                g.transform.localPosition = V2;
                g.name = $"T_{i}_{j}";

                if (j == 0) g.IsFirst = true;
                if (j == Data.Strokes [i].Pot.Length - 1) g.IsLast = true;

                Strokes.Add (g);
            }
        }
        ResetLine ();
    }

    //重置整個場地
    public void ResetLine ()
    {
        foreach (LineRenderer i in Lines)
        {
            Destroy (i.gameObject);
        }
        Lines = new List<LineRenderer> ();
        foreach (T_Con i in Strokes)
        {
            //i.IsOK_0 = false;
            NowT = null;
            i.IsOK_1 = false;
            i.IsOK_2 = false;
        }
        NowT = Strokes [0];
    }

    public void LoadNext ()
    {
        for (int i = 0; i < Strokes.Count; i++)
        {
            T_Con _i = Strokes [i];
            if (_i.IsOK_1) continue;

            SpLineCon._.NowT = Strokes [i];
            return;
        }
        //全部的點都用完了
        if (WinEvent != null) WinEvent ();
    }

    [ContextMenu ("Get_T")]
    public void Get_T ()
    {
        T_Con [] tt = Ts.GetComponentsInChildren <T_Con> ();
        Strokes = new List<T_Con> ();
        foreach (T_Con i in tt)
        {
            Strokes.Add (i);
        }
    }
}

[Serializable]
public struct Word_Data
{
    public char Word_Name;
    [Serializable]
    public struct Stroke
    {
        public Vector2 [] Pot;
    }
    public Stroke [] Strokes;
}