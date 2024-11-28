using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float health = 100;
    public void TakeDamage(float _damage)
    {
        health -= _damage;
        if (health <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Destroy(gameObject);
    }
}
