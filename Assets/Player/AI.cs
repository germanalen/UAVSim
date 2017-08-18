using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class AI : MonoBehaviour
{
	Radar radar;
	AeroplaneController controller;
	TargetSeeker targetSeeker;

	PlayerInput playerInput;


	public Vector3 wishDirection;
	public float wishRoll = 0.0f;
	public float wishPitch = 0.0f;

	public float angleDiffThreshold = 0.0f;

	void Start ()
	{
		radar = GetComponent<Radar> ();
		controller = GetComponent<AeroplaneController> ();
		targetSeeker = GetComponent<TargetSeeker> ();

		playerInput = GetComponent<PlayerInput> ();
		playerInput.inputToggleBrakes = true;

		wishDirection = transform.forward;
	}

	void Update ()
	{
		wishDirection = (Chase () + AvoidGround ()).normalized;
		Attack ();
		Turn ();

		DebugDraw ();
	}

	Vector3 Chase ()
	{
		if (targetSeeker.target) {
			return (targetSeeker.target.position - transform.position).normalized;
		}
		return wishDirection;
	}

	Vector3 AvoidGround ()
	{
		if (controller.Altitude < 100) {
			return new Vector3 (0, 1, 0);
		}
		return wishDirection;
	}

	float firePeriod = 10;
	float lastFireTime = 0;
	void Attack ()
	{
		if (targetSeeker.target) {
			Vector3 toTarget = targetSeeker.target.position - transform.position;
			if (Vector3.Angle(transform.forward, toTarget) < 1) {
				if (Time.realtimeSinceStartup - lastFireTime > firePeriod) {
					lastFireTime = Time.realtimeSinceStartup;
					playerInput.inputFire = true;
				}
			}
		}
	}

	//The following function was copied from AeroplaneAiControl.cs and modified
	bool m_TakenOff = false;
	void Turn ()
	{
		Vector3 targetPos = transform.position + wishDirection;

		// adjust the yaw and pitch towards the target
		Vector3 localTarget = transform.InverseTransformPoint(targetPos);
		float targetAngleYaw = Mathf.Atan2(localTarget.x, localTarget.z);
		float targetAnglePitch = -Mathf.Atan2(localTarget.y, localTarget.z);

		// calculate the difference between current pitch and desired pitch
		float changePitch = targetAnglePitch - controller.PitchAngle;

		// AI always applies gentle forward throttle
		const float throttleInput = 0.5f;

		float m_PitchSensitivity = 0.5f;
		// AI applies elevator control (pitch, rotation around x) to reach the target angle
		float pitchInput = changePitch*m_PitchSensitivity;

		// clamp the planes roll
		float desiredRoll = targetAngleYaw;
		float yawInput = 0;
		float rollInput = 0;
		if (!m_TakenOff)
		{
			// If the planes altitude is above m_TakeoffHeight we class this as taken off
			float m_TakeoffHeight = 20;
			if (controller.Altitude > m_TakeoffHeight)
			{
				m_TakenOff = true;
			}
		}
		else
		{
			float m_RollSensitivity = 0.2f;
			// now we have taken off to a safe height, we can use the rudder and ailerons to yaw and roll
			yawInput = targetAngleYaw;
			rollInput = -(controller.RollAngle - desiredRoll)*m_RollSensitivity;
		}

		// adjust how fast the AI is changing the controls based on the speed. Faster speed = faster on the controls.
		float m_SpeedEffect = 0.01f;
		float currentSpeedEffect = 1 + (controller.ForwardSpeed*m_SpeedEffect);
		rollInput *= currentSpeedEffect;
		pitchInput *= currentSpeedEffect;
		yawInput *= currentSpeedEffect;

		playerInput.inputRoll = rollInput;
		playerInput.inputPitch = pitchInput;
	}

	void DebugDraw ()
	{
		float len = 40;
		Debug.DrawRay (transform.position, wishDirection * len, Color.yellow);
		Debug.DrawRay (transform.position, Vector3.ProjectOnPlane(wishDirection, transform.forward).normalized * len, Color.gray);
	}
}
