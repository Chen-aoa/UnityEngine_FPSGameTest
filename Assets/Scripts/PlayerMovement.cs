using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //unity interface
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _gravity = -9.81f;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _groundDistance = 0.4f;
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _jumpHeight = 3f;
    [SerializeField] private GameObject _playerPrefabs;
    [SerializeField] private GameObject _playerParent;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _deceleration = 2f;
    //private
    private Vector3 _velocity;
    private bool _isGrounded;
    private float _velocityZ = 0f;
    private float _velocityX = 0f;
    private GameObject _character;
    private Animator _animator;
    //static
    private static string HORIZONTAL = "Horizontal";
    private static string VERTICAL = "Vertical";
    private static string JUMP = "Jump";
    private void Awake()
    {
        _character = Instantiate(_playerPrefabs, _playerParent.transform);
    }
    private void Start()
    {
        var charPos = _character.transform.position;
        _character.transform.eulerAngles = new Vector3(charPos.x, charPos.y + 60, charPos.z);
        _character.TryGetComponent(out _animator);

    }
    // Update is called once per frame
    void Update()
    {
        _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundDistance, _groundMask);
        if (_isGrounded && _velocity.y < 0)
        {
            _velocity.y = -2f;
        }

        float xPos = Input.GetAxis(HORIZONTAL);
        float zPos = Input.GetAxis(VERTICAL);
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backPressed = Input.GetKey("s");


        if (Input.GetButtonDown(JUMP) && _isGrounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * _gravity);
        }

        if (forwardPressed && _velocityZ < 1.01f)
        {
            _velocityZ += Time.deltaTime * _acceleration;
        }
        if (leftPressed && _velocityX < 1.01f)
        {
            _velocityX += Time.deltaTime * _acceleration;
        }
        if (backPressed && _velocityZ > -1.01f)
        {
            _velocityZ -= Time.deltaTime * _acceleration;
        }
        if (rightPressed && _velocityX > -1.01f)
        {
            _velocityX -= Time.deltaTime * _acceleration;
        }
        if (!rightPressed && !leftPressed && !forwardPressed && !backPressed)
        {
            if (_velocityZ > 0 || _velocityZ < 0)
            {
                _velocityZ = Mathf.Lerp(_velocityZ, 0, 5f * Time.deltaTime);
            }

            if (_velocityX > 0 || _velocityX < 0)
            {
                _velocityX = Mathf.Lerp(_velocityX, 0, 5f * Time.deltaTime);
            }
        }
        _animator.SetFloat("VelocityY", _velocityX);
        _animator.SetFloat("VelocityX", _velocityZ);
        Vector3 move = transform.right * xPos + transform.forward * zPos;
        _characterController.Move(move * _speed * Time.deltaTime);


        _velocity.y += _gravity * Time.deltaTime;
        _characterController.Move(_velocity * Time.deltaTime);

    }

}
