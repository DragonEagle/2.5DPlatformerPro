using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private float _gravity = 1.0f;
    [SerializeField]
    private float _jumpHeight = 15.0f;
    [SerializeField]
    private float _pushPower = 2.0f;

    private float _yVelocity;
    private bool _canDoubleJump = false;
    [SerializeField]
    private int _coins;
    private UIManager _uiManager;
    [SerializeField]
    private int _lives = 3;

    private Vector3 _direction, _velocity;
    private bool _canWallJump = false;
    private Vector3 _wallJumpDirection;

    public int Coins { get { return _coins; } }

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is NULL."); 
        }

        _uiManager.UpdateLivesDisplay(_lives);
    }

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        if (_controller.isGrounded == true)
        {
            _direction = new Vector3(horizontalInput, 0, 0);
            _velocity = _direction * _speed;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _yVelocity = _jumpHeight;
                _canDoubleJump = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump == false)
            {
                if (_canDoubleJump == true)
                {
                    _yVelocity += _jumpHeight;
                    _canDoubleJump = false;
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) && _canWallJump)
            {
                _velocity = _wallJumpDirection * _speed;
                _yVelocity = _jumpHeight;
            }

            _yVelocity -= _gravity;
        }

        _velocity.y = _yVelocity;

        _controller.Move(_velocity * Time.deltaTime);
    }

    public void AddCoins()
    {
        _coins++;

        _uiManager.UpdateCoinDisplay(_coins);
    }

    public void Damage()
    {
        _lives--;

        _uiManager.UpdateLivesDisplay(_lives);

        if (_lives < 1)
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        _canWallJump = false;
        if(!_controller.isGrounded && hit.transform.CompareTag("Wall")){
            _canWallJump = true;
            _wallJumpDirection = hit.normal;
        }
        else if (hit.transform.CompareTag("MovingBox"))
        {
            Rigidbody body = hit.transform.GetComponent<Rigidbody>();
            if (body)
            {
                Vector3 pushDir = new Vector3(hit.moveDirection.x, 0, 0);
                body.velocity = pushDir * _pushPower;
            }
        }
    }
}
