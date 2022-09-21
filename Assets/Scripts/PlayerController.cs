using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _playerPrefabs;
    [SerializeField] private GameObject _playerParent;
    [SerializeField] private float _velocityZ = 0f;
    [SerializeField] private float _velocityX = 0f;
    [SerializeField] private float _acceleration = 5f;
    [SerializeField] private float _deceleration = 2f;


    private GameObject _character;

    private void Awake()
    {
        _character = Instantiate(_playerPrefabs, _playerParent.transform);
    }
    private void Start()
    {
        _character.TryGetComponent(out _animator);
    }
    private void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool backPressed = Input.GetKey("s");


        if (forwardPressed && _velocityZ < 1.01f)
        {
            _velocityZ += Time.deltaTime * _acceleration;
        }
        if (backPressed && _velocityZ > -1.01f)
        {
            _velocityZ -= Time.deltaTime * _acceleration;
        }
        if (leftPressed && _velocityX < 1.01f)
        {
            _velocityX += Time.deltaTime * _acceleration;
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

    }










}
