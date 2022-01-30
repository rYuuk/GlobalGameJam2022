using System;
using DG.Tweening;
using UnityEngine;
using VContainer;

[RequireComponent(typeof(BoxCollider))]
[Serializable]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int id;

    [Header("Camera Shake")] 
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float strength = 0.5f;
    [SerializeField] private float randomness = 90f;
    [SerializeField] private int vibrato = 10;
    
    [Inject] private GameManager gameManager;
    [Inject] private Camera mainCamera;
    
    public int Id => id;
    
    public Vector3 SpawnPosition => spawnPoint.position;

    private const string PlayerTag = "Player";
    private Animator animator;
    private bool isActivated;
    private static readonly int IsActive = Animator.StringToHash("IsActive");

    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerTag) && !isActivated)
        {
            Invoke(nameof(Shake), 1f);
            isActivated = true;
            gameManager.SetCheckpointID(id);
            audioSource.Play();
            animator.SetBool(IsActive, true);
        }
    }

    private void Shake()
    {
        mainCamera.transform.DOShakePosition(duration:duration,strength: strength,vibrato:vibrato, randomness:randomness);
    }
}
