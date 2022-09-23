using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private Gun _gun;
    [SerializeField] private float mouseSensivity = 100f;
    [SerializeField] private Transform _playerBody;
    [SerializeField] private RightClicker _rightClicker;
    private float xRotation = 0f;
    private float _recoilVertical = 0f;
    private void Awake()
    {
        _rightClicker.ScopeState += ScopeSensivity;
        _gun.FireOn += FireOnRecoil;
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void OnDestroy()
    {
        _rightClicker.ScopeState -= ScopeSensivity;
        _gun.FireOn -= FireOnRecoil;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensivity * Time.deltaTime + _recoilVertical;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        _playerBody.Rotate(Vector3.up * mouseX);
    }
    private void ScopeSensivity(bool state)
    {
        if (state)
        {
            mouseSensivity = 50;
        }
        else
        {
            mouseSensivity = 100;
        }
    }
    private void FireOnRecoil(bool state)
    {
        if (state)
        {
            _recoilVertical = UnityEngine.Random.Range(-0.1f, 0.35f);
        }
        else
        {
            _recoilVertical = 0;
        }
    }
}
