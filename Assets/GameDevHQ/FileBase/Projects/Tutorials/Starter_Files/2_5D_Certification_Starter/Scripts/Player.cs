using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    private Vector3 moveDirection;
    CharacterController _controller;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        moveDirection = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float yVelocity = moveDirection.y;
        float horizontalInput = Input.GetAxis("Horizontal");
        if (_controller.isGrounded)
        {
            moveDirection = new Vector3(0, 0, horizontalInput * _speed);
            if (Input.GetButtonDown("Jump"))
            {
                yVelocity = _jumpHeight;
            } 
        } else
        {
            yVelocity -= _gravity;
        }
        moveDirection.y = yVelocity;
        _controller.Move(moveDirection * Time.deltaTime);
        // if grounded
        // calculate direction based on input
        // or jump
    }
}
