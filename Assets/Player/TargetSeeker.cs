using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TargetSeeker : MonoBehaviour
{

	public float maxAngle = 15;
	public float maxDistance = 1000;

	public Transform target { get; private set; }

	public int team;


	float TargetScore (Transform possibleTarget)
	{
		if (possibleTarget == null)
			return 0;
		
//		if (possibleTarget.gameObject.tag != "Player")
//			return 0;

		Vector3 toTarget = possibleTarget.position - transform.position;
		float angle = Vector3.Angle (transform.forward, toTarget.normalized);

		if (angle > maxAngle)
			return 0;

		float distance = Vector3.Distance (transform.position, possibleTarget.position);
		if (distance > maxDistance)
			return 0;

		return 1.0f / (angle * distance); // 1.0f/0 == Mathf.Infinity
	}



	void Update()
	{
		if (Time.frameCount % 6 == 0) {
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			target = null;
			for (int i = 0; i < players.Length; ++i) {
				PlayerSetup playerSetup = players [i].GetComponent<PlayerSetup> ();
				if (playerSetup.team != team) {
					Transform playerCenter = players [i].transform.Find ("Center");
					if (TargetScore (playerCenter) > TargetScore (target)) {
						target = playerCenter;
					}
				}
			}
		}
	}

}
