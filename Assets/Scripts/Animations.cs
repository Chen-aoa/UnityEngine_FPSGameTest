using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    //MissScripts

    //static
    private static string FIRE = "Fire";
    private static string IDLE = "Idle";
    private static string RUN = "Run";
    private static string JUMP = "Jump";
    //SerializedField
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        TryGetComponent(out _animator);
    }

    public void RunAnim()
    {
        _animator.SetBool(RUN, true);
    }
    public void JumpAnim()
    {
        _animator.SetTrigger(JUMP);
        gameObject.transform.position = new Vector3(gameObject.transform.position.x, 0.1f, gameObject.transform.position.z);
    }

}
