using UnityEngine;
public class MediumController : MonoBehaviour
{
    [SerializeField] private bool isPlayerInside;
    [SerializeField] Transform dashTarget;
    private Vector3 Target;
    private float holdingTime;
    private CharacterController player;

    private void Start()
    {
        player = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        isPlayerInside = true;
        player.playerState = CharacterController.PlayerStates.Wave;
    }

    private void OnTriggerExit(Collider other)
    {
        isPlayerInside = false;
    }
    
    
}
