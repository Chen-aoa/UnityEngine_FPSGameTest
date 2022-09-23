using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public event Action<WeaponType> WeaponTypeInvorker;
    WeaponType _weapon;
    public event Action<int> Movement;
    [SerializeField] private GameObject _firstGun;
    //[SerializeField] private GameObject _secondGun;
    [SerializeField] private GameObject _knife;
    void Start()
    {
        _weapon = WeaponType.FIRSTGUN;

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponTypeInvorker?.Invoke(WeaponType.FIRSTGUN);
            _weapon = WeaponType.FIRSTGUN;
            Movement?.Invoke(4);
            WeaponState(WeaponType.FIRSTGUN);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            WeaponTypeInvorker?.Invoke(WeaponType.KNIFE);
            Movement?.Invoke(4);
            WeaponState(WeaponType.KNIFE);
            _weapon = WeaponType.KNIFE;
            Movement?.Invoke(8);
        }
    }
    private void WeaponState(WeaponType weaponType)
    {
        _firstGun.SetActive(false);
        //   _secondGun.SetActive(false);
        _knife.SetActive(false);
        switch (weaponType)
        {
            case WeaponType.UNDEFINITION:
                break;
            case WeaponType.FIRSTGUN:
                _firstGun.SetActive(true);
                break;
            /*    case WeaponType.SECONDGUN:
                    break;
                    _secondGun.SetActive(true);*/
            case WeaponType.KNIFE:
                _knife.SetActive(true);
                break;
            default:
                break;
        }
    }
}
