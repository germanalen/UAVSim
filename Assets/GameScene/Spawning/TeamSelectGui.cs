using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeamSelectGui : MonoBehaviour {

	LocalPlayerFinder localPlayerFinder;
	Canvas canvas;

	void Start ()
	{
		localPlayerFinder = GetComponent<LocalPlayerFinder> ();
		canvas = GetComponent<Canvas> ();
	}

	void Update ()
	{
		canvas.enabled = localPlayerFinder.localPlayer == null;
	}
}
