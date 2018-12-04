using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawn : MonoBehaviour {

	public static int Score;
	public GameObject CarToSpawn;
	public int SpawnPointNumb;
	public float CarNumb;
	public float SpawnTime;
	public float SpawnRange;
	public float Speed;
	public float AngerSpeed;
	int RandSpawn;
	float ActlTime;
	float time;
	Transform SpawnTran;

	void Start () {
		PointMove.Crashed = false;
		Score = 0;
		ActlTime = SpawnTime + Random.Range(-SpawnRange, SpawnRange);
		time = ActlTime;
		RandSpawn= Random.Range(1, SpawnPointNumb + 1);
		SpawnTran = GameObject.Find("Go" + RandSpawn).transform;
	}
	

	void Update () {
		//print (Score);
		time += Time.deltaTime;
		if (time >= ActlTime && CarNumb > 0 && !PointMove.Crashed) {
			time = 0;
			CarNumb--;
			GameObject G = Instantiate (CarToSpawn, SpawnTran.position, SpawnTran.rotation);
			G.GetComponent<PointMove>().Speed = Speed;
			G.GetComponent<PointMove>().TurnSpeed = Speed * 15;
			G.GetComponent<PointMove>().AngerLimit = AngerSpeed;
			RandSpawn= Random.Range(1, SpawnPointNumb + 1);
			SpawnTran = GameObject.Find("Go" + RandSpawn).transform;
		}
	}
}
