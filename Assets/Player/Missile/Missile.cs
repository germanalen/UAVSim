using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Missile : NetworkBehaviour
{
	public TargetSeeker targetSeeker;
	public float speed = 300;
	public float maxTurn = 2;

	Health health;

	Rigidbody body;

	void Start ()
	{
		body = GetComponent<Rigidbody> ();
		health = GetComponent<Health> ();
	}



	void FixedUpdate ()
	{
		if (isServer) {
			body.velocity = (transform.forward * speed);
			if (targetSeeker) {
				if (targetSeeker.target) {
					Vector3 wishDir = (targetSeeker.target.position - transform.position);
					Quaternion wishRot = Quaternion.LookRotation (wishDir);
					float turn = Mathf.Min (maxTurn, Quaternion.Angle (transform.rotation, wishRot));
					body.MoveRotation (Quaternion.RotateTowards (transform.rotation, wishRot, turn));
				}
			}
			health.scrapVelocity = transform.forward * speed;
		}
	}
}
