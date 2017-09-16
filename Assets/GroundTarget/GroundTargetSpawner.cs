using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;

public class GroundTargetSpawner : NetworkBehaviour
{
	public int team;
	public GameObject groundTargetPrefab;

	public override void OnStartServer ()
	{
		GameObject groundTarget = Instantiate (groundTargetPrefab, transform.position, transform.rotation);
		NetworkServer.Spawn (groundTarget);
	}
}
