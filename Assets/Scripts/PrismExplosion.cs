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
    
    private Collider collider;
    
    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
        collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isTriggered)
        {
            explosion.Play();
            animator.SetTrigger("Explode");
        }

        isTriggered = true;
    }
}
