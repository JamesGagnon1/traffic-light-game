﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {
	

	void Start () {
		gameObject.GetComponent<Text>().text = "Score : " + CarSpawn.Score;
	}
}
