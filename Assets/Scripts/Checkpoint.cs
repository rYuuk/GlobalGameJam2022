using UnityEngine;
using VContainer;

[RequireComponent(typeof(BoxCollider))]
public class Checkpoint : MonoBehaviour
{
    [Inject] private GameManager gameManager;

    public Vector3 SpawnPosition => spawnPoint.position;

    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int id;
    
    private const string PlayerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerTag))
        {
            gameManager.SetCheckpointID(id);
        }
    }
}