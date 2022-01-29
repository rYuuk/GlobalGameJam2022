using System;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    [Inject] private CharacterController player;
    [Inject] private GameData data;

    private const string PlayerTag = "Player";

    private void Awake()
    {
    }

    private void MovePlayerToCheckpoint()
    {
        player.transform.position = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerTag))
        {
            // Subscribe to player death here -> SpawnNewPlayer
        }
    }
}
