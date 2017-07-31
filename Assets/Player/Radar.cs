using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
	public Mesh ringMesh;
	public Material ringMaterial;
	public float ringDistance = 2f;

	void Update ()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");

		for (int i = 0; i < players.Length; ++i) {
			if (players [i] != transform.parent.gameObject) {


				Vector3 playerCenter = players [i].transform.Find ("Center").position;
				Vector3 direction = (playerCenter - transform.position).normalized;
				Vector3 ringPos = transform.position + direction * ringDistance;
				Quaternion ringRot = Quaternion.LookRotation (direction);

				Graphics.DrawMesh (ringMesh, ringPos, ringRot, ringMaterial, 0, null, 0, null, false, false, false); 
			}
		}
	}
}
