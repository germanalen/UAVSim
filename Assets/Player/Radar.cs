using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
	public List<Vector3> toPlayers { get; private set; }
	public float maxDistance = 2000;

	void Start ()
	{
		toPlayers = new List<Vector3> ();
	}

	void Update ()
	{
		if (Time.frameCount % 6 == 0) {
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			toPlayers.Clear ();

			for (int i = 0; i < players.Length; ++i) {
				PlayerSetup playerSetup = players [i].GetComponent<PlayerSetup> ();
				if (!playerSetup.isLocalPlayer) {
					Vector3 playerCenter = players [i].transform.Find ("Center").position;
					Vector3 toPlayer = playerCenter - transform.position;

					if (toPlayer.magnitude <= maxDistance)
						toPlayers.Add (toPlayer);
				}
			}
		}
	}
}
