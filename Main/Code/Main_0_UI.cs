using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_0_UI : MonoBehaviour
{
    public DragButt DragButt;
    public LineRenderer LineObj;
    public Photograph Photograph;

    public ButtObj Butt_0;
    public ButtObj Butt_1;

    public LineRenderer NowLine;
    public List <LineRenderer> All_Lines = new List<LineRenderer> ();

    void Start ()
    {
        void SetPos (Vector3 v3)
        {
            NowLine.positionCount++;
            NowLine.SetPosition (NowLine.positionCount - 1,v3);
        }
        DragButt.Start_Del += (v3) =>
        {
            NowLine = Instantiate (LineObj);
            NowLine.transform.SetParent (transform);
            All_Lines.Add (NowLine);

            SetPos (v3);
        };
        DragButt.Update_Del += (v3) =>
        {
            SetPos (v3);
        };
        DragButt.End_Del += (v3) =>
        {
            Butt_0.gameObject.SetActive (true);
            Butt_1.gameObject.SetActive (true);
        };

        Butt_0.Del += (g) => 
        {
            Main_UI._.Im = Photograph.GetCaIm (new Rect (0, 0, 600, 300));
            Main_UI._.UpData ();
            Main_UI._.NextObj ();
        };
        Butt_1.Del += (g) => 
        {
            foreach (LineRenderer i in All_Lines)
            {
                Destroy (i.gameObject);
            }
            All_Lines = new List<LineRenderer> ();
        };
    }
}
