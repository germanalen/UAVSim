using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AI : MonoBehaviour
{
	Radar radar;
	AeroplaneController controller;

	PlayerInput playerInput;


	void Start ()
	{
		radar = GetComponent<Radar> ();
		controller = GetComponent<AeroplaneController> ();

		playerInput = GetComponent<PlayerInput> ();
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.O))
			playerInput.inputFire = true;
		if (Input.GetKey (KeyCode.P))
			playerInput.inputFire = true;
	}
}
