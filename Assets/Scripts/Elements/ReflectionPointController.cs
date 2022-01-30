using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ReflectionPointController : MonoBehaviour
{
    [SerializeField] private ReflectPoint[] attachPoint;
    [SerializeField] private bool isHoldingPlayer;
    [SerializeField] private float reflectTime;
    private int positionIndex = 0;
    private CharacterController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isHoldingPlayer && attachPoint[positionIndex].isPlayerInRange)
        {
            if (positionIndex < attachPoint.Length-1)
            {
                positionIndex++;
                player.transform.DOMove(attachPoint[positionIndex].transform.position, reflectTime);
                player.transform.DOLookAt(attachPoint[positionIndex].transform.position,0.15f);
                
            } else if (player.transform.position == attachPoint[attachPoint.Length-1].transform.position)
            {
                positionIndex = 0;
                isHoldingPlayer = false;
                player.playerState = CharacterController.PlayerStates.Walking;
                player.transform.DOLookAt(Vector3.right, 0.05f);
            }
            
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        player.transform.DOMove(attachPoint[positionIndex].transform.position, 0.3f);
        player.playerState = CharacterController.PlayerStates.Reflecting;
        isHoldingPlayer = true;
    }
}
