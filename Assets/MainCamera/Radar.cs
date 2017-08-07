﻿//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
//public class Radar : MonoBehaviour
//{
//	public Mesh ringMesh;
//	public Material ringMaterial;
//	public float ringDistance = 2f;
//
//	void Update ()
//	{
//		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");
//
//		for (int i = 0; i < players.Length; ++i) {
//			PlayerSetup playerSetup = players [i].GetComponent<PlayerSetup> ();
//			if (!playerSetup.isLocalPlayer) {
//
//
//				Vector3 playerCenter = players [i].transform.Find ("Center").position;
//				Vector3 direction = (playerCenter - transform.position).normalized;
//				Vector3 ringPos = transform.position + direction * ringDistance;
//				Quaternion ringRot = Quaternion.LookRotation (direction);
//
//				Graphics.DrawMesh (ringMesh, ringPos, ringRot, ringMaterial, 0, null, 0, null, false, false, false); 
//			}
//		}
//	}
//}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
	public float markDistance = 2f;

	public Mesh markMesh;
	public Material markMaterial;

	public Material lockOnMaterial;
	public TargetSeeker targetSeeker;

	void Update ()
	{
		GameObject[] players = GameObject.FindGameObjectsWithTag ("Player");


		for (int i = 0; i < players.Length; ++i) {
			PlayerSetup playerSetup = players [i].GetComponent<PlayerSetup> ();
			if (!playerSetup.isLocalPlayer) {
				Vector3 playerCenter = players [i].transform.Find ("Center").position;
				Vector3 direction = (playerCenter - transform.position).normalized;
				Vector3 markPos = transform.position + direction * markDistance;
				Quaternion ringRot = Quaternion.LookRotation (direction);

				Graphics.DrawMesh (markMesh, markPos, ringRot, markMaterial, 0, null, 0, null, false, false, false); 
			}
		}

		if (targetSeeker) {
			if (targetSeeker.target) {
				DrawMark (targetSeeker.target.position, markDistance * 1.2f, markMesh, lockOnMaterial);
			}
		}
	}

	void DrawMark (Vector3 position, float radius, Mesh mesh, Material material)
	{
		Vector3 direction = (position - transform.position).normalized;
		Vector3 markPos = transform.position + direction * radius;
		Quaternion markRot = Quaternion.LookRotation (direction);

		Graphics.DrawMesh (mesh, markPos, markRot, material, 0, null, 0, null, false, false, false); 
	}
}

