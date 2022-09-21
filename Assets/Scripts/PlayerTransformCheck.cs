using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformCheck : MonoBehaviour
{
    private void LateUpdate()
    {
        //because it animation error.
        gameObject.transform.localPosition = new Vector3(0f, 0f, 0f);
        gameObject.transform.localRotation = Quaternion.Euler(0f, 60f, 0f);
    }
}
