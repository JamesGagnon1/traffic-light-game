using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointMove : MonoBehaviour {
	int RandNumbTurn;
	public float Speed;
	public float TurnSpeed;
	bool CanGo = true;
	int PathNumb;
	int TurnDir;
	bool Turning = false;
	bool DurTur = false;
	Transform RotPos;
	float StartEulr;
	float TargRot;
	public float Happy = 1.5f;
	public float AngerLimit;
	float StartHappy;
	public int HappyResetNumb;
	public static bool Crashed = false;
	float RealCF;
	public float CrashForce;
	public float CrashForceRange;
	Rigidbody RB;
	

	void Start () {
		StartHappy = Happy;
		RB = GetComponent<Rigidbody>();
	}
	
	
	void FixedUpdate () {
		if (!Crashed) {
			Color HappyColor = new Color(1f, Happy, Happy);
			GetComponentInChildren <MeshRenderer>().material.SetColor ("_Color", HappyColor);
			RaycastHit hit;
			Ray MoveRay = new Ray(transform.position + transform.forward * 1.52f  + new Vector3 (0, -2.5f,0), transform.forward);
			if ((!Physics.Raycast(MoveRay, out hit, 2.5f) && Happy > 0) || Happy <= 0) {
				if (CanGo) {
					if (Happy >= 1 && HappyResetNumb > 0) {
						HappyResetNumb--;
						Happy = StartHappy;
					}
					if (Turning) {
						RaycastHit turnHit;
						if (TurnDir > 0) {
							Ray RightRay = new Ray(transform.position + transform.right * 0.57f  + new Vector3 (0, 0.5f, 0), transform.right);
							if (Physics.Raycast(RightRay, out turnHit, 2.5f)) {
								if (turnHit.transform.gameObject.tag == "RotateAround") {
									RotPos = turnHit.transform;
									DurTur = true;
								} else {
									gameObject.transform.position += ((transform.forward * Speed) * Time.deltaTime);
								}
							} else {
								gameObject.transform.position += ((transform.forward * Speed) * Time.deltaTime);//make this here ^ on both turns
							}
							if (DurTur) {
								if (StartEulr + 90 >= 360) {
									TargRot = 360 - (StartEulr + 90);
								} else {
									TargRot = StartEulr + 90;
								}
								if (TargRot == 0) {
									if (transform.eulerAngles.y >= 270) {
										transform.RotateAround (RotPos.position, Vector3.up, TurnSpeed * Time.deltaTime);
									} else {
										transform.eulerAngles = new Vector3 (0, TargRot, 0);
										DurTur = false;
										Turning = false;
									}
								} else {
									if (transform.eulerAngles.y < TargRot) {
										transform.RotateAround (RotPos.position, Vector3.up, TurnSpeed * Time.deltaTime);
									} else {
										transform.eulerAngles = new Vector3 (0, TargRot, 0);
										DurTur = false;
										Turning = false;
									}
								}
							}
						}

						if (TurnDir < 0) {
							Ray LeftRay = new Ray(transform.position - transform.right * 0.57f  + new Vector3 (0, 0.5f, 0), -transform.right);
							if (Physics.Raycast(LeftRay, out turnHit, 2.5f)) {
								if (turnHit.transform.gameObject.tag == "RotateAround") {
									RotPos = turnHit.transform;
									DurTur = true;
								} else {
									gameObject.transform.position += ((transform.forward * Speed) * Time.deltaTime);
								}
							} else {
								gameObject.transform.position += ((transform.forward * Speed) * Time.deltaTime);
							}
							if (DurTur) {
								if (StartEulr - 90 < 0) {
									TargRot = 360 + (StartEulr - 90);
								} else {
									TargRot = StartEulr - 90;
								}
								if (TargRot == 270 && transform.eulerAngles.y == 0) {
									transform.eulerAngles -= new Vector3 (0, 0.01f, 0);
								} else {
									if (TargRot == 0) {
										if (transform.eulerAngles.y <= 90) {
											transform.RotateAround (RotPos.position, -Vector3.up, TurnSpeed * Time.deltaTime);
										} else {
											transform.eulerAngles = new Vector3 (0, TargRot, 0);
											Turning = false;
											DurTur = false;
										}
									} else {
										if (transform.eulerAngles.y > TargRot) {
											transform.RotateAround (RotPos.position, -Vector3.up, TurnSpeed * Time.deltaTime);
										} else {
											transform.eulerAngles = new Vector3 (0, TargRot, 0);
											Turning = false;
											DurTur = false;
										}
									}
								}
							}
						}

					} else {
						gameObject.transform.position += ((transform.forward * Speed) * Time.deltaTime);
						StartEulr = transform.eulerAngles.y;
					}
				}
			}
			if (Happy > 0) {
				if (Physics.Raycast(MoveRay, out hit, 2.5f) || !CanGo) {
					Happy -= Time.deltaTime / AngerLimit;
				}
			} else {
				Happy = 0;
			}
		}
	}

	void OnTriggerStay (Collider Hit) {
		if (Happy > 0) {
			if (Hit.gameObject.tag == "Stop") {
				CanGo = false;
			}
			if (Hit.gameObject.tag == "Go") {
				CanGo = true;
			}
		} else {
			CanGo = true;
		}
	}
	void OnTriggerEnter (Collider Hit) {
		if (Hit.gameObject.tag == "Die") {
			if (Happy > 1) {
				Happy = 1;
			}
			CarSpawn.Score += (int)(Happy * 10);
			Destroy(gameObject);
		}
		if (Hit.gameObject.tag == "Turn") {
			//number of child objects on turns/intersect/hit = number of choices, first number in name is the random number it gets, second is the direction it turns
			RotPos = Hit.gameObject.transform;
			PathNumb = Hit.gameObject.GetComponentsInChildren<Transform>().Length - 1;
			RandNumbTurn = Random.Range(0, PathNumb);
			foreach (Transform Child in Hit.gameObject.GetComponentsInChildren<Transform>()) {
				if (Child.name.Substring(0, 1) == RandNumbTurn.ToString()) {
					if (Child.name.Substring(4, 1) == "-") {
						TurnDir = -int.Parse(Child.name.Substring(5, 1));
					} else {
						TurnDir = int.Parse(Child.name.Substring(4, 1));
					}
					if (TurnDir != 0) {
						Turning = true;
					}
				}
			}
		}
	}
	void OnCollisionEnter (Collision Hit) {
		if (Hit.gameObject.name == "Car(Clone)") {
			foreach (GameObject Cars in GameObject.FindGameObjectsWithTag("Car")) {
				foreach (BoxCollider G in Cars.GetComponentsInChildren<BoxCollider>()) {
					if (!G.isTrigger) {
						G.center = new Vector3 (0, 0.53f, 0);
					} else {
						G.enabled = false;
					}
				}
				Cars.GetComponent<Rigidbody>().useGravity = true;
			}
			RealCF = CrashForce + Random.Range(-CrashForceRange, CrashForceRange);
			RB.AddExplosionForce (RealCF, Hit.gameObject.transform.position, 15, 1.5f);
			gameObject.tag = "CCar";
			Crashed = true;
		}
	}
}