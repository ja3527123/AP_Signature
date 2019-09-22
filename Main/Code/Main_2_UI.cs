using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main_2_UI : MonoBehaviour
{
    public Tween_Array [] Tweens;

    IEnumerator Start()
    {
        yield return new WaitForSeconds (1);
        Tweens [0].enabled = true;
        yield return new WaitForSeconds (2);
        Tweens [1].enabled = true;
        yield return new WaitForSeconds (0.1f);
        Tweens [2].enabled = true;
        yield return new WaitForSeconds (0.1f);
        Tweens [3].enabled = true;
        yield return new WaitForSeconds (0.1f);
        Tweens [4].enabled = true;
        yield return new WaitForSeconds (0.1f);
        Tweens [5].enabled = true;
        yield return new WaitForSeconds (6);
        Main_UI._.NextObj ();
    }
}
