using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

	Transform target;
	public GameObject localPlayer { get; private set; }


	void Update ()
	{
		if (target) {
			transform.position = target.position;
			transform.rotation = target.rotation;
		} else {
			GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
			for (int i = 0; i < players.Length; ++i) {
				PlayerSetup playerSetup = players [i].GetComponent<PlayerSetup> ();
				if (playerSetup.isLocalPlayer) {
					target = playerSetup.transform.Find ("CameraTarget");
					localPlayer = players [i];
					break;
				}
			}
		}
	}
}
