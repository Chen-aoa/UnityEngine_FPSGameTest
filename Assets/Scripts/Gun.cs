using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private float _damage = 10f;
    [SerializeField] private float _range = 100f;
    [SerializeField] private float fireRate = 15f;
    [SerializeField] private Camera _fpsCamera;
    [SerializeField] private ParticleSystem _muzzleFlash;
    [SerializeField] private GameObject _impactEffect;
    [SerializeField] private GameObject _mainCamera;
    [SerializeField] private TextMeshProUGUI _ammoInMagazineText;
    [SerializeField] private TextMeshProUGUI _ammoPoolText;
    [SerializeField] private GameObject _ammo;
    [SerializeField] private GameObject _ammoParent;
    WeaponType _weapon;
    private float nextTimeToFire = 0f;
    private int _ammoInMagazine = 30;
    private int _ammoPool = 960;
    private bool _ammoControl = false;
    public event Action<bool> FireOn;
    private void Awake()
    {
        _weapon = WeaponType.FIRSTGUN;
        MagazineTexter();
    }
    void Update()
    {
        if (_ammoInMagazine < 31 && _ammoControl && Input.GetKey(KeyCode.R))
        {
            //þarjör deðiþtir.
            StartCoroutine(Magazine());
            _ammoControl = true;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && _weapon == WeaponType.FIRSTGUN)
        {
            if (_ammoPool == 0 && _ammoInMagazine == 0 && _ammoControl)
            {
                _ammoInMagazineText.text = "0";
                StopAllCoroutines();
            }
            else if (_ammoInMagazine != 0)
            {
                MagazineTexter();
                nextTimeToFire = Time.time + 1f / fireRate;
                Shoot();
                _ammoInMagazine--;
                if (_ammoInMagazine < 31) { _ammoControl = true; }
            }
        }
        else
        {
            FireOn?.Invoke(false);
        }

    }
    IEnumerator Magazine()
    {
        _ammoInMagazineText.text = "R";
        yield return new WaitForSeconds(1.5f);
        _ammoInMagazine = 30;
        _ammoPool = _ammoPool - (30 - _ammoInMagazine);
        _ammoControl = false;
        MagazineTexter();
    }
    private void MagazineTexter()
    {
        _ammoInMagazineText.text = _ammoInMagazine.ToString();
        _ammoPoolText.text = _ammoPool.ToString();
    }

    private void Shoot()
    {
        Recoil();
        _muzzleFlash.Play();
        RaycastHit hit;
        if (Physics.Raycast(_fpsCamera.transform.position, _fpsCamera.transform.forward, out hit, _range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(_damage);
            }
            GameObject impactGO = Instantiate(_impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }
    private void Recoil()
    {
        FireOn?.Invoke(true);
    }

}
