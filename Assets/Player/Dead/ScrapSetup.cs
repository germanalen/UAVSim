using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapSetup : MonoBehaviour
{

	public Material material;
	public Vector3 initialVelocity = Vector3.zero;
	public float maxAngularSpeed = 1;

	void Start ()
	{
		Transform scrap = transform.Find("Scrap");

		foreach (Transform fragment in scrap) {
			Rigidbody body = fragment.gameObject.AddComponent<Rigidbody> () as Rigidbody;
			body.velocity = initialVelocity;
			body.angularVelocity = Random.insideUnitSphere * maxAngularSpeed;

			fragment.gameObject.AddComponent<BoxCollider> ();
			fragment.gameObject.layer = LayerMask.NameToLayer ("Scrap");

			MeshRenderer meshRenderer = fragment.gameObject.GetComponent<MeshRenderer> ();
			meshRenderer.material = material;
		}

		Destroy (gameObject, 20);
	}
}
