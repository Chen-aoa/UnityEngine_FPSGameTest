
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    public void TakeDamage(float amount)
    {
        _health -= (int)amount;
        if (_health < 1)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
}
