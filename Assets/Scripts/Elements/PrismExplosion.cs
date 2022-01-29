using UnityEngine;
using UnityEngine.VFX;
using VContainer;

[RequireComponent(typeof(SphereCollider))]
public class PrismExplosion : MonoBehaviour
{
    [Inject] private GameManager gameManager;

    [SerializeField] private VisualEffect explosion;
    [SerializeField] private Animator animator;

    private bool isTriggered;
    
    private Collider col;
    private static readonly int Explode = Animator.StringToHash("Explode");

    private void Awake()
    {
        col = GetComponent<SphereCollider>();
        col.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            explosion.Play();
            animator.SetTrigger(Explode);
            gameManager.Finish();
        }

        isTriggered = true;
    }
}
