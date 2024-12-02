using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class Enemy : MonoBehaviour
{
    private enum State
    {
        Idle,
        Move,
        Attack
    }

    public float sightRange, attackRange;
    public float fireRate = 0.5f;
    public float health = 100;
    private bool isFiring = false;
    private bool playerInSight, playerInRange;
    private State currentState;
    private NavMeshAgent agent;
    [SerializeField] private State state;
    [SerializeField] private Transform Target;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator anim;
    
    private void Start()
    {
        currentState = State.Idle;
        agent = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        UpdateState();
        UpdateAnim();
    }
    private void UpdateState()
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
    private void UpdateAnim()
    {
        
    }
    private void Idle()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        if(playerInSight)
            currentState = State.Move;
    }
    private void Move()
    {
        agent.SetDestination(Target.position);
        playerInRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if(playerInRange)
            currentState = State.Attack;
    }
    private void Attack()
    {
        agent.SetDestination(transform.position);
        if(!playerInRange) 
            currentState = State.Move;
        
        if (!isFiring)
        {
            isFiring = true;
            Fire();
            StartCoroutine(FireDelay());
        }
    }
    private void Fire()
    {
        
    }

    private IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(fireRate);
    }
    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}