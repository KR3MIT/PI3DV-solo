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
    public float initialAccuracy = 0.5f;
    private float currentAccuracy;
    private float damage = 11;
    private bool isFiring = false;
    private bool playerInSight, playerInRange;
    private State currentState;
    private NavMeshAgent agent;
    private CharacterController cc;
    
    [SerializeField] private State state;
    [SerializeField] private Transform Target;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator anim;
    private void Start()
    {
        currentState = State.Idle;
        agent = GetComponent<NavMeshAgent>();
        cc = GetComponent<CharacterController>();
        currentAccuracy = initialAccuracy;
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
        var maxSpeed = agent.speed;
        
        
        var normalisedSpeed = agent.velocity.magnitude / maxSpeed; 
        
        Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
        anim.SetFloat("xMove", normalisedSpeed);
    }
    private void Idle()
    {
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        if (playerInSight)
            currentState = State.Move;
    }
    private void Move()
    {
        currentAccuracy = initialAccuracy;
        agent.SetDestination(Target.position);
        playerInRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        
        if(playerInRange)
            currentState = State.Attack;
    }
    private void Attack()
    {
        currentAccuracy = Mathf.MoveTowards(currentAccuracy, 0, 0.02f);
        agent.SetDestination(transform.position);
        
        playerInRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if(!playerInRange) 
            currentState = State.Move;
        
        anim.SetBool("isFiring",isFiring);
        
        if (!isFiring)
        {
            isFiring = true;
            Fire();
            StartCoroutine(FireDelay());
        }
        
    }
    private void Fire()
    {
        var dirVector = Target.position - transform.position;
        var rotatedVector = Quaternion.Euler(Random.Range(-currentAccuracy, currentAccuracy), Random.Range(-currentAccuracy, currentAccuracy), Random.Range(-currentAccuracy, currentAccuracy)) * dirVector;
        
        if(Physics.Raycast(transform.position, rotatedVector, out RaycastHit hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
            {
                
            }
        }
    }
    private IEnumerator FireDelay()
    {
        yield return new WaitForSeconds(fireRate);
        isFiring = false;
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