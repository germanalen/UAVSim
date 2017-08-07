using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class MissileLauncher : NetworkBehaviour
{

	public GameObject missilePrefab;


	void Update ()
	{
		if (!isLocalPlayer)
			return;

		if (Input.GetKeyDown (KeyCode.Space)) {
			CmdLaunch (transform.position + (new Vector3(0,-10,0)), transform.rotation);
		}
	}


	[Command]
	void CmdLaunch(Vector3 position, Quaternion rotation)
	{
		GameObject missile = Instantiate (missilePrefab, position, rotation);
		NetworkServer.Spawn (missile);
	}
}
