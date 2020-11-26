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
    private Animator _anim;

    private bool _jumping = false;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
        moveDirection = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        float yVelocity = moveDirection.y;
        if (_controller.isGrounded)
        {
            if (_jumping)
            {
                _jumping = false;
                _anim.SetBool("IsJumping", _jumping);
            }
            float horizontalInput = Input.GetAxisRaw("Horizontal");
            if ((transform.forward.z < 0 && horizontalInput > 0) || (transform.forward.z > 0 && horizontalInput < 0))
            {
                transform.Rotate(new Vector3(0,180,0));
            }
            if (_anim)
            {
                _anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
            }
            moveDirection = new Vector3(0, 0, horizontalInput * _speed);
            if (Input.GetButtonDown("Jump"))
            {
                _jumping = true;
                _anim.SetBool("IsJumping", _jumping);
                yVelocity = _jumpHeight;
            } 
        } else
        {
            yVelocity -= _gravity;
        }
        moveDirection.y = yVelocity;
        _controller.Move(moveDirection * Time.deltaTime);
    }
}
