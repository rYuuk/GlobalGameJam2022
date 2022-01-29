using DG.Tweening;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float speed;

    private void Update()
    {
        transform.position = Vector3.Slerp(transform.position, target.position + offset, Time.deltaTime * speed);
        //transform.DOMove(target.position + offset, Time.deltaTime * speed);
    }
}
