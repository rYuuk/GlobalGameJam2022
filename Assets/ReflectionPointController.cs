using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ReflectionPointController : MonoBehaviour
{
    [SerializeField] private Transform[] attachPoint;
    [SerializeField] private bool isHoldingPlayer;
    [SerializeField] private float startHoldingTime;
    private float holdingTime;
    private int positionIndex = 0;
    private CharacterController player;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<CharacterController>();
        holdingTime = startHoldingTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (isHoldingPlayer)
        {
            holdingTime -= Time.deltaTime;
            if (holdingTime <= 0f)
            {
                holdingTime = startHoldingTime;
                isHoldingPlayer = false;
                player.playerState = CharacterController.PlayerStates.Walking;
                positionIndex = 0;
            }

            if (Input.GetKeyDown(KeyCode.Q) && positionIndex < attachPoint.Length-1)
            {
                holdingTime = startHoldingTime;
                positionIndex++;
                player.transform.DOMove(attachPoint[positionIndex].position, 0.3f);
                player.transform.DOLookAt(attachPoint[positionIndex].position,0.15f);
            }
            
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (Input.GetKey(KeyCode.E))
        {
            player.transform.DOMove(attachPoint[positionIndex].position, 0.3f);
            player.playerState = CharacterController.PlayerStates.Dashing;
            isHoldingPlayer = true;
        }
    }
}
