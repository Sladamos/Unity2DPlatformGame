using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private Vector2 spawnPoint;

    private void Awake()
    {
        spawnPoint = transform.position;
    }

    private void ReturnToSpawn()
    {
        transform.position = spawnPoint;
    }

    public void SetSpawnPoint(Vector2 newSpawnPoint)
    {
        spawnPoint = newSpawnPoint;
    }
}
