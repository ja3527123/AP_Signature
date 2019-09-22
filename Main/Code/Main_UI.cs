using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Main_UI : ObjArray
{
    static public Main_UI _;

    public Texture2D Im;

    public LagButt ResetButt;

    void Awake ()
    {
        _ = this;
    }

    void Start ()
    {
        ResetButt.Del += (g) =>
        {
            SceneManager.LoadScene ("0");
        };
    }

    public void UpData ()
    {
        StartCoroutine (UpData (Im));
    }
    static public string URL = "https://script.google.com/macros/s/AKfycbyf16bxyBQR0FEk69p4dTQCz_uDNr224Xu4dN6RAcwShuaMSWs/exec";
    static public IEnumerator UpData (Texture2D Im)
    {
        string b64 = ImCon.Texture2DToBase64 (Im);
        
        string Name = $"{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Hour}_{DateTime.Now.Minute}_{DateTime.Now.Second}";

        WWWForm wwwf = new WWWForm ();
        wwwf.AddField ("base64Data", b64);
        wwwf.AddField ("Name", Name);
        UnityWebRequest WebRequest = UnityWebRequest.Post (URL, wwwf);
        yield return WebRequest.SendWebRequest ();
        if (!string.IsNullOrEmpty (WebRequest.error))
        {
            print ("失敗");
        }
        else
        {
            print (WebRequest.downloadHandler.text);
        }
    }
}
