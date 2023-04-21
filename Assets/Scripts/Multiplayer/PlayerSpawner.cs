using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawner : NetworkBehaviour, ISpawned
{
    [SerializeField] private NetworkObject playerPrefab;
    private MultiplayerStart multiplayerStart;

    public override void Spawned()
    {
        base.Spawned();
        multiplayerStart = FindObjectOfType<MultiplayerStart>();
        if (!Object.HasStateAuthority) return;
        foreach (var player in Runner.ActivePlayers)
        {
            SpawnPlayer(player);
        }
    }

    private void SpawnPlayer(PlayerRef player)
    {
        NetworkObject networkPlayerObject = Runner.Spawn(playerPrefab, transform.position, Quaternion.identity, player);
        multiplayerStart.spawnedCharacters.Add(player, networkPlayerObject);
    }
}
