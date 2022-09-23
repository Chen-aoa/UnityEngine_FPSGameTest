using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightClicker : MonoBehaviour
{
    [SerializeField] private WeaponController _weaponController;
    [SerializeField] private GameObject _scopeUI;
    [SerializeField] private bool _scopeState = false;
    [SerializeField] private Camera _camera;
    [SerializeField] private GameObject _weaponCamera;
    [SerializeField] private WeaponType _weaponType;
    public event Action<bool> ScopeState;
    private void Start()
    {
        _weaponType = WeaponType.FIRSTGUN;
        _camera = gameObject.GetComponent<Camera>();
        _weaponController.WeaponTypeInvorker += WeaponTyper;
        _weaponController.WeaponTypeInvorker += ScopeStateChanger;
    }
    private void OnDestroy()
    {
        _weaponController.WeaponTypeInvorker -= WeaponTyper;
        _weaponController.WeaponTypeInvorker -= ScopeStateChanger;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && WeaponType.FIRSTGUN == _weaponType)
        {
            _scopeState = !_scopeState;
            _scopeUI.SetActive(_scopeState);
            if (_scopeState)
            {
                ScopeState?.Invoke(true);
                _weaponCamera.SetActive(false);
                _camera.fieldOfView = 30;
            }
            else
            {
                ScopeState?.Invoke(false);
                _weaponCamera.SetActive(true);
                _camera.fieldOfView = 60;
            }

        }

    }
    private void WeaponTyper(WeaponType weaponType)
    {
        _weaponType = weaponType;
    }
    public void ScopeStateChanger(WeaponType type)
    {
        if (type != WeaponType.FIRSTGUN)
        {
            _scopeState = false;
        }
    }
}
