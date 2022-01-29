using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReflectPoint : MonoBehaviour
{
    [SerializeField] private float range;
    private CharacterController player;
    private float distanceToPlayer;
    public bool isPlayerInRange;

    private void Start()
    {
        player = FindObjectOfType<CharacterController>();
    }

    private void Update()
    {
        CheckPlayerDistance();
    }

    private bool CheckPlayerDistance()
    {
        distanceToPlayer = Vector3.Distance(player.transform.position,this.transform.position);
        isPlayerInRange = distanceToPlayer < range;
        return isPlayerInRange;
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position,range);
    }
}
