using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_3_UI : MonoBehaviour
{
    public Tween Welcome_Tw;
    public Tween Text_Tw;

    IEnumerator Start()
    {
        yield return new WaitForSeconds (3);
        Text_Tw.enabled = true;
        yield return new WaitForSeconds (0.5f);
        Welcome_Tw.enabled = true;
        yield return new WaitForSeconds (5);
        SceneManager.LoadScene ("0");
    }
}
