using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent (typeof(AeroplaneController))]
public class AeroplaneUserControl2Axis : MonoBehaviour
{
	private AeroplaneController m_Aeroplane;

	bool throttleInput = false;

	private void Awake ()
	{
		m_Aeroplane = GetComponent<AeroplaneController> ();
	}


	private void FixedUpdate ()
	{
		float roll = Input.GetAxis ("Horizontal");
		float pitch = Input.GetAxis ("Vertical");

		if (Input.GetKeyDown (KeyCode.LeftControl)) {
			throttleInput = !throttleInput;
		}


		float throttle = throttleInput ? 1 : -1;
		bool airBrakes = !throttleInput;

		m_Aeroplane.Move (roll, pitch, 0, throttle, airBrakes);
	}
}
