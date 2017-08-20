
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadarVisualizer : MonoBehaviour
{
	public float markDistance = 2f;

	public Mesh markMesh;
	public Material markMaterial;

	public Material lockOnMaterial;

	void Update ()
	{
		GameObject localPlayer = GetComponent<MainCamera> ().localPlayer;
		if (localPlayer) {
			Radar radar = localPlayer.GetComponent<Radar> ();
			TargetSeeker targetSeeker = localPlayer.GetComponent<TargetSeeker> ();

			for (int i = 0; i < radar.toPlayers.Count; ++i) {
				Vector3 direction = radar.toPlayers [i].normalized;
				Vector3 markPos = transform.position + direction * markDistance;
				Quaternion ringRot = Quaternion.LookRotation (direction);

				Graphics.DrawMesh (markMesh, markPos, ringRot, markMaterial, 0, null, 0, null, false, false, false); 
			}


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

