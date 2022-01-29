using UnityEngine;

public class Medium : MonoBehaviour
{
    [SerializeField] Transform dashTarget;

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
    }

    private void OnTriggerExit(Collider other)
    {
        player.dashTarget = null;
    }
}