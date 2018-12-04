using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraficLight : MonoBehaviour {
	public bool GreenLight;
	public Material Green;
	public Material Red;

	void Start () {
		if (GreenLight) {
			gameObject.tag = "Go";
			gameObject.GetComponent<MeshRenderer>().material = Green;
		} else {
			gameObject.tag = "Stop";
			gameObject.GetComponent<MeshRenderer>().material = Red;
		}
	}
	
	void Update () {
		if (GreenLight) {
			gameObject.tag = "Go";
			gameObject.GetComponent<MeshRenderer>().material = Green;
		} else {
			gameObject.tag = "Stop";
			gameObject.GetComponent<MeshRenderer>().material = Red;
		}
	}
}
