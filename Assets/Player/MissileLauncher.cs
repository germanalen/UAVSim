using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MissileLauncher : NetworkBehaviour
{

	public GameObject missilePrefab;

	Transform missilePlaceholders;
	AeroplaneController controller;

	void Start ()
	{
		controller = GetComponent<AeroplaneController> ();
		missilePlaceholders = transform.Find ("MissilePlaceholders");
	}


	void Update ()
	{
		if (!isLocalPlayer)
			return;

		if (Input.GetKeyDown (KeyCode.Space)) {
			if (missilePlaceholders.childCount > 0) {
				GameObject placeholder = missilePlaceholders.GetChild (0).gameObject;
				CmdLaunch (placeholder.transform.position, placeholder.transform.rotation);
			}
		}
	}


	[Command]
	void CmdLaunch(Vector3 position, Quaternion rotation)
	{
		if (missilePlaceholders.childCount > 0) {
			RpcDestroyMissilePlaceholder ();

			GameObject missile = Instantiate (missilePrefab, position, rotation);
			missile.GetComponent<Rigidbody> ().velocity = transform.forward * controller.ForwardSpeed;
			NetworkServer.Spawn (missile);
		}
	}

	[ClientRpc]
	void RpcDestroyMissilePlaceholder()
	{
		GameObject placeholder = missilePlaceholders.GetChild (0).gameObject;
		Destroy (placeholder);
	}
}
