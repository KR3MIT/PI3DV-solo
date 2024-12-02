using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum State
    {
        Idle,
        Move,
        Attack
    }
    public float health = 100;
    private State currentState;
    [SerializeField] private Animator anim;
    [SerializeField] private float range = 50;
    [SerializeField] private State state;
    void Start()
    {
        currentState = State.Idle;
    }
    void Update()
    {
        switch (currentState)
        {
            case State.Idle:
                Idle();
                break;
            case State.Move:
                Move();
                break;
            case State.Attack:
                Attack();
                break;
        }
    }
    void Idle()
    {
        
        //currentState = State.Move;
    }
    void Move()
    {
        
        //currentState = State.Attack;
    }
    void Attack()
    {
        
        //currentState = State.Move;
    }
        
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