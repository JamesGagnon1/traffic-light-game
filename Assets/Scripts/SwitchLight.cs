using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchLight : MonoBehaviour {

	void OnMouseDown() {
		if (GameOver.PlayMode) {
			foreach (Transform Lights in this.gameObject.GetComponentsInChildren<Transform>()) {
				if (Lights != transform) {
					if (Lights.gameObject.GetComponent<TraficLight>().GreenLight) {
						Lights.gameObject.GetComponent<TraficLight>().GreenLight = false;
					} else {
						Lights.gameObject.GetComponent<TraficLight>().GreenLight = true;
					}
				}
			}
		}
	}
}
