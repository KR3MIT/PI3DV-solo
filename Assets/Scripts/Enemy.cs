using UnityEngine;
using UnityEngine.AI;
using System.Collections;
public class Enemy : MonoBehaviour
{
    //comments and code are written with the help of Copilot AI
    //enemy states
    private enum State
    {
        Idle,
        Move,
        Attack,
        Dead
    }
    // enemy stats
    public float sightRange, attackRange;
    public float fireRate = 0.5f;
    public float health = 100;
    public float initialAccuracy = 6f;
    public float minimumAccuracy = 2f;
    public int damage = 11;
    
    private float currentAccuracy;
    private bool isFiring = false;
    private bool playerInSight, playerInRange;
    // enemy components
    private NavMeshAgent agent;
    private RagdollManager ragdoll;
    private EnemyVFX vfx;
    
    [SerializeField] private State currentState;
    [SerializeField] private Transform Target;
    [SerializeField] private LayerMask playerLayer;
    [SerializeField] private Animator anim;
    private void Start()
    {
        //initialise enemy components
        Target = GameObject.Find("Player").transform;
        currentState = State.Idle;
        agent = GetComponent<NavMeshAgent>();
        currentAccuracy = initialAccuracy;
        ragdoll = GetComponent<RagdollManager>();
        vfx = GetComponent<EnemyVFX>();
    }
    private void FixedUpdate()
    {
        //update enemy state and animations
        UpdateState();
        UpdateAnim();
    }
    private void UpdateState()
    {
        //switch between enemy states
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
            case State.Dead:
                Dead();
                break;
        }
    }
    private void UpdateAnim()
    {
        // update enemy animations
        var maxSpeed = agent.speed;
        
        
        var normalisedSpeed = agent.velocity.magnitude / maxSpeed; 
        
        Vector3 localVelocity = transform.InverseTransformDirection(agent.velocity);
        anim.SetFloat("xMove", normalisedSpeed);
    }
    private void Idle()
    {
        // check if player is in sight
        playerInSight = Physics.CheckSphere(transform.position, sightRange, playerLayer);
        if (playerInSight)
            currentState = State.Move;
    }
    private void Move()
    {
        //move towards player
        currentAccuracy = initialAccuracy;
        agent.SetDestination(Target.position);
        playerInRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        
        if(playerInRange)
            currentState = State.Attack;
    }
    private void Attack()
    {
        //attack player
        agent.SetDestination(transform.position);
        this.transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Target.position - transform.position), 5f);
        
        playerInRange = Physics.CheckSphere(transform.position, attackRange, playerLayer);
        if(!playerInRange) 
            currentState = State.Move;
        
        anim.SetBool("isFiring",isFiring);
        
        var dirVector = Target.position - transform.position;
        
        if(Physics.Raycast(transform.position, dirVector, out RaycastHit hit, attackRange))
        {
            if (!hit.collider.CompareTag("Player"))
                return;
        }
        //accuracy
        currentAccuracy = Mathf.MoveTowards(currentAccuracy, minimumAccuracy, 0.02f);
        
        if (!isFiring)
        {
            //fire weapon
            isFiring = true;
            Fire();
            StartCoroutine(FireDelay());
        }
    }
    private void Dead() {} //empty function
    private void Fire()
    {
        vfx.FireWeaponVFX();
        
        var dirVector = Target.position - transform.position;
        var rotatedVector = Quaternion.Euler(Random.Range(-currentAccuracy, currentAccuracy), Random.Range(-currentAccuracy, currentAccuracy), Random.Range(-currentAccuracy, currentAccuracy)) * dirVector;
        //check if player is hit
        if(Physics.Raycast(transform.position, rotatedVector, out RaycastHit hit, attackRange))
        {
            if (hit.collider.CompareTag("Player"))
                Target.GetComponent<PlayerInfo>().TakeDamage(damage);
        }
    }
    private IEnumerator FireDelay() //delay between shots
    {
        yield return new WaitForSeconds(fireRate);
        isFiring = false;
    }
    public void TakeDamage(int _damage) // enemy takes damage
    {
        anim.SetTrigger("Hit");
        health -= _damage;
        if (health <= 0)
            Die();
    }
    private void Die() //enemy dies
    {
        agent.SetDestination(transform.position);
        currentState = State.Dead;
        ragdoll.ToggleRagdoll();
    }
}