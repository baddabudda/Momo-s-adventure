using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _player;
    // for walking
    [SerializeField] private KeyCode _leftKey = KeyCode.A;
    [SerializeField] private KeyCode _rightKey = KeyCode.D;
    private bool _left = false;
    private bool _right = false;

    private float _direction = 0f;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _deceleration = 5f;
    [SerializeField] private float _maxSpeed = 5f;

    // for jumping
    private bool _isTouchingGround;
    [Range(1,20)]
    [SerializeField] private float _jumpSpeed;
    [Range(1,10)]
    [SerializeField] private float _lowJumpMultiplier;
    [Range(1, 10)]
    [SerializeField] private float _fallMultiplier;
    private Vector2 _groundCheckBox;


    [SerializeField]private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    // for animator
    private Animator _playerAnimation;

    // for collisions
    public static bool _isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<Rigidbody2D>();

        _groundCheckBox = groundCheck.gameObject.GetComponent<BoxCollider2D>().size;

        _playerAnimation = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(_leftKey)) {
            _left = true;
            _direction = -1;
        }
        if (Input.GetKeyUp(_leftKey)) {
            _left = false;
            if (_direction == -1) {
                if (_right) {
                    _direction = 1;
                }
                else {
                    _direction = 0;
                }
            }
        }

        if (Input.GetKeyDown(_rightKey)) {
            _right = true;
            _direction = 1;
        }
        if (Input.GetKeyUp(_rightKey)) {
            _right = false;
            if (_direction == 1) {
                if (_left) {
                    _direction = -1;
                }
                else {
                    _direction = 0;
                }
            }
        }

        if (!_isAlive) {
            _player.velocity = Vector2.zero;
        }
        else {
            _isTouchingGround = Physics2D.OverlapBox(groundCheck.position, _groundCheckBox, 0, groundLayer);

            // walking: base version

            _player.velocity = new Vector2(_direction * _speed, _player.velocity.y);

            if (_direction < 0f) {
                transform.localScale = new Vector2(-1f, 1f);
            }
            else if (_direction > 0f) {
                transform.localScale = new Vector2(1f, 1f);
            }


            // jumping
            if (Input.GetKey("space") && _isTouchingGround) {
                _player.velocity = new Vector2(_player.velocity.x, _jumpSpeed);
            }
            if (_player.velocity.y < 0f) {
                _player.velocity += Vector2.up * Physics2D.gravity.y * (_fallMultiplier - 1) * Time.deltaTime;
            }
            else if (_player.velocity.y > 0f && !Input.GetKey("space")) {
                _player.velocity += Vector2.up * Physics2D.gravity.y * (_lowJumpMultiplier - 1) * Time.deltaTime;
            }

            _playerAnimation.SetFloat("Speed", Mathf.Abs(_player.velocity.x));
            _playerAnimation.SetBool("OnGround", _isTouchingGround);
            _playerAnimation.SetFloat("JumpVector", _player.velocity.y);
        }
    }
}