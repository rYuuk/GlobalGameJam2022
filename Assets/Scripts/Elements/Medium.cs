using UnityEngine;

public class Medium : MonoBehaviour
{
    [SerializeField] private Transform dashTarget;
    [SerializeField] private float dashValue;
    [SerializeField] private float dashTime;

    private Vector3 Target;
    private float holdingTime;
    private Player player;


    private void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.EnableDash(dashTarget, dashValue, dashTime);
    }

    private void OnTriggerExit(Collider other)
    {
        player.DisableDash();
    }
}