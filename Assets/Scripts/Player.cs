using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15.0f;

    private CharacterController _controller;

    private float _yVelocity;
    private bool _canDoubleJump;
    private int _playerCoins;

    public int Lives { get { return _lives; } }
    public int PlayerCoins { get { return _playerCoins;  } }

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        Vector3 direction = new Vector3(horizontal, 0, 0);
        Vector3 velocity = direction * _speed;

        if (_controller.isGrounded)
        {
            // Default keyboard control for Jump is space
            if (Input.GetButtonDown("Jump"))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetButtonDown("Jump") && _canDoubleJump)
            {
                _canDoubleJump = false;
                _yVelocity += _jumpHeight;
            } else
            {
                _yVelocity -= _gravity;
            }
        }
        velocity.y = _yVelocity;

        _controller.Move(velocity * Time.deltaTime);
    }
    public void AddCoin()
    {
        _playerCoins++;
    }
    public void Damage()
    {
        _lives--;
        if (_lives == 0)
        {
            SceneManager.LoadScene(0);
        }
    }
}
