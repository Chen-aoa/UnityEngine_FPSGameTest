using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : MonoBehaviour
{
    Rigidbody rb;
    public Action ColliderOn;
    private void Awake()
    {
        TryGetComponent(out rb);
    }
    private void OnCollisionEnter(Collision collision)
    {
        ColliderOn?.Invoke();
    }
}
