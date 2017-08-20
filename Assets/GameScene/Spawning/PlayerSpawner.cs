using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerSpawner : NetworkBehaviour
{
	public NetworkManager networkManager;

	public void Spawn (int team)
	{
		SpawnMessage spawnMessage = new SpawnMessage();
		spawnMessage.team = team;
		ClientScene.AddPlayer (networkManager.client.connection, 0, spawnMessage);
	}
}

class SpawnMessage : MessageBase
{
	public int team;
}