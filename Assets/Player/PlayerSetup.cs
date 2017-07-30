using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSetup : NetworkBehaviour
{

	Rigidbody rigidbodyComponent;

	public void Start ()
	{
		rigidbodyComponent = GetComponent<Rigidbody> ();
		if (isLocalPlayer) {

		} else {
			GameObject camera = transform.Find ("Camera").gameObject;
			camera.SetActive (false);
			AeroplaneUserControl2Axis controls = GetComponent<AeroplaneUserControl2Axis> ();
			controls.enabled = false;
		}
	}


	void Update()
	{
		if (isLocalPlayer && !isServer) {
			//CmdPrintTransformDiff (transform.position, transform.rotation, rigidbodyComponent.velocity);
		}
	}


	[Command(channel=1)]
	void CmdPrintTransformDiff(Vector3 pos, Quaternion rot, Vector3 vel)
	{
		float posdiff = (pos - transform.position).magnitude;
		float rotdiff = Quaternion.Angle (rot, transform.rotation);
		float veldiff = (vel - rigidbodyComponent.velocity).magnitude;
		Debug.Log (posdiff + " " + rotdiff + " " + veldiff);
	}
}
