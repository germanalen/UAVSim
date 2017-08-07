using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TargetSeeker : MonoBehaviour
{

	public float maxAngle = 15;
	//public float maxDistance = 1000;
	float maxDistance;
	public Transform target;

	void Start()
	{
		maxDistance = GetComponent<SphereCollider> ().radius;
	}

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

//	void FixedUpdate()
//	{
//		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
//		target = null;
//		for (int i = 0; i < players.Length; ++i) {
//			PlayerSetup playerSetup = players [i].GetComponent<PlayerSetup> ();
//			if (!playerSetup.isLocalPlayer) {
//				Transform playerCenter = players [i].transform.Find ("Center");
//				if (TargetScore (playerCenter) > TargetScore (target)) {
//					target = playerCenter;
//				}
//			}
//		}
//	}

	void OnTriggerStay (Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
			if (TargetScore (other.transform) > TargetScore (target))
				target = other.transform;
		}
	}

//	void OnTriggerExit(Collider other)
//	{
//		if (other.transform == target)
//			target = null;
//	}

	void FixedUpdate()
	{
		if (TargetScore (target) <= 0)
			target = null;
	}
}
