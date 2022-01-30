using System;
using DG.Tweening;
using UnityEngine;
using VContainer;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    [Inject] private Player player;

    private Transform target;

    private void Awake()
    {
        target = FindObjectOfType<Player>().transform;
    }

    private void Update()
    {
        transform.position = Vector3.Slerp(transform.position, target.position + offset, Time.deltaTime * speed);
    }
}