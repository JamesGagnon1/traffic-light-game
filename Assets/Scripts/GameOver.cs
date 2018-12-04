using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {
Color fadeColor;
Image fade;
public GameObject GOScreen;
public GameObject WScreen;
public static bool PlayMode;
float CrashWait = 2f;
float StartCarNumb;
float time;
float StarWateTime = 0.75f;
	
	void Start () {
		time = 0;
		PlayMode = true;
		fade = GetComponentInChildren<Image>();
		fadeColor.a = 0;
		StartCarNumb = GameObject.Find("Spawn").GetComponent<CarSpawn>().CarNumb;
	}
	
	void FixedUpdate () {
		if (PointMove.Crashed) {
			PlayMode = false;
			foreach (GameObject Car in GameObject.FindGameObjectsWithTag("CCar")) {
				if (Car.GetComponent<Rigidbody>().velocity.magnitude == 0) {
					if (CrashWait <= 0) {
						fadeColor.a += Time.deltaTime / 5;
						fade.color = fadeColor;
					} else {
						CrashWait -= Time.deltaTime;
					}
				}
			}
			if (fadeColor.a >= 1) {
				GOScreen.SetActive (true);
			}
		} else if (GameObject.Find("Car(Clone)") == null && GameObject.Find("Spawn").GetComponent<CarSpawn>().CarNumb <= 0) {
			PlayMode = false;
			WScreen.SetActive (true);
			time += Time.deltaTime;
			if (CarSpawn.Score >= (StartCarNumb * 10) * 0.25f) {
				GameObject.Find("NL").GetComponent<Button>().interactable = true;
				GameObject S1 = GameObject.Find("Star 1 F");
				if (time >= StarWateTime && !S1.GetComponent<Image>().enabled) {
					S1.GetComponent<Image>().enabled = true;
					S1.GetComponent<ParticleSystem>().Play();
					time = 0;
				}
				if (CarSpawn.Score >= (StartCarNumb * 10) * 0.5f) {
					GameObject S2 = GameObject.Find("Star 2 F");
					if (time >= StarWateTime && !S2.GetComponent<Image>().enabled) { 
						S2.GetComponent<Image>().enabled = true;
						S2.GetComponent<ParticleSystem>().Play();
						time = 0;
					}
					if (CarSpawn.Score >= (StartCarNumb * 10) * 0.75f) {
						GameObject S3 = GameObject.Find("Star 3 F");
						if (time >= StarWateTime && !S3.GetComponent<Image>().enabled) {
							S3.GetComponent<Image>().enabled = true;
							S3.GetComponent<ParticleSystem>().Play();
							time = 0;
						}
					}
				}
			}
		}
	}
}
