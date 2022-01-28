using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveForce = 1;
    [SerializeField] private float jumpForce = 1;
    [SerializeField] private Camera mainCamera;
    
    private Rigidbody rigidbody;
    Vector3 cameraPos;
    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        cameraPos = mainCamera.transform.position;
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            rigidbody.velocity = Vector3.left * moveForce;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            rigidbody.velocity = (Vector3.right * moveForce);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rigidbody.velocity = (Vector3.up * jumpForce);
        }
        
        mainCamera.transform.position = new Vector3(transform.position.x, transform.position.y, cameraPos.z);
    }
}