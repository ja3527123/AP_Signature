using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tween_Array : Tween {
	public List <Tween> All_Tween = new List<Tween> ();

	public override void Update () {
		base.Update ();
		if (!enabled) return;

		float _f = animationCurve.Evaluate (f);
		for (int i = 0; i < All_Tween.Count; i++) {
			
			if (All_Tween [i].Del != null) {
				_f = All_Tween [i].animationCurve.Evaluate (_f);
				All_Tween [i].Del (_f);
			}
		}
		
		if (Mod == 0) {
			for (int i = 0; i < All_Tween.Count; i++) {
				if (All_Tween [i].End_Del != null) {
					End_Del ();
				}
			}
		}
	}
}
