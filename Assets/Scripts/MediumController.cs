using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class MediumController : MonoBehaviour
{
    [SerializeField] private bool isPlayerInside;

    [SerializeField] private float startHoldingTime;
    [SerializeField] private float velocityAffector;
    private float holdingTime;
    public float dashValue = 5f;
    private int positionIndex = 0;
    private CharacterController player;

    private void Start()
    {
        player = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayerInside && Input.GetKeyDown(KeyCode.E))
        {
            //DASH
            
            if (player.transform.position.x<transform.position.x)
            {
                Vector3 dashTarget = new Vector3(player.transform.position.x + dashValue, 0);
                player.transform.DOMove(player.transform.right + dashTarget,0.5f);    
            }
            else if (player.transform.position.x > transform.position.x)
            {
                Vector3 dashTarget = new Vector3(player.transform.position.x - dashValue, 0);
                player.transform.DOMove(player.transform.right - dashTarget,0.5f); 
            }
            
                   
            
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        isPlayerInside = true;
        player.playerState = CharacterController.PlayerStates.Wave;
        player.GetComponent<Rigidbody>().velocity =Vector3.zero;
    }

    private void OnTriggerExit(Collider other)
    {
        player.playerState = CharacterController.PlayerStates.Walking;
        isPlayerInside = false;
    }
}
