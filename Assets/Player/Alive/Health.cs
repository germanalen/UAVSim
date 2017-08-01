using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Health : NetworkBehaviour
{

	public GameObject deadPlayerPrefab;

	[SyncVar] public int health = 20;


	void OnTriggerEnter (Collider impactObject)
	{
		if (!isServer)
			return;

		Damager damager = impactObject.gameObject.GetComponent<Damager> ();
		if (damager) {
			health -= damager.damagePoints;
		} else {
			health = 0;
		}

		if (health <= 0) {
			RpcDie ();
		}
	}


	[ClientRpc]
	void RpcDie ()
	{
		if (deadPlayerPrefab) {
			GameObject deadPlayer = Instantiate (deadPlayerPrefab, transform.position, transform.rotation);

			AeroplaneController aeroplaneController = GetComponent<AeroplaneController> ();
			ScrapSetup scrapSetup = deadPlayer.GetComponent<ScrapSetup> ();
			scrapSetup.initialVelocity = transform.forward * aeroplaneController.ForwardSpeed * 0.1f;
		}

		if (isServer) {
			Destroy (gameObject);
		}
	}
}
