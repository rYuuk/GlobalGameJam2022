using System;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(BoxCollider))]
[Serializable]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int id;
    
    [Inject] private GameManager gameManager;

    public int Id => id;
    
    public Vector3 SpawnPosition => spawnPoint.position;

    private const string PlayerTag = "Player";
    private Animator animator;
    private static readonly int IsActive = Animator.StringToHash("IsActive");

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerTag))
        {
            gameManager.SetCheckpointID(id);
            
            animator.SetBool(IsActive, true);
        }
    }
}
