using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

	Transform target;

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
					break;
				}
			}
		}
	}
}
