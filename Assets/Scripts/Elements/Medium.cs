using UnityEngine;

public class Medium : MonoBehaviour
{
    [SerializeField] private Transform dashTarget;
    [SerializeField] private float dashValue;
    [SerializeField] private float dashTime;

    private Vector3 Target;
    private float holdingTime;
    private CharacterController player;


    private void Start()
    {
        player = FindObjectOfType<CharacterController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        player.playerState = CharacterController.PlayerStates.Wave;
        player.dashTarget = dashTarget;
        player.dashValue = dashValue;
        player.dashTime = dashTime;
    }

    private void OnTriggerExit(Collider other)
    {
        player.dashTarget = null;
    }
}