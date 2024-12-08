using UnityEngine;
using UnityEngine.AI;

public class RagdollManager : MonoBehaviour
{
    // Ragdoll manager
    private CapsuleCollider collider;
    private Collider[] colliders;
    private Rigidbody[] rigidbodies;

    public Animator animator;
    private NavMeshAgent agent;
    private Enemy enemy;
    // Ragdoll state
    public bool isRagdoll;
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        
        colliders = GetComponentsInChildren<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        
        SetRagdoll(false);
    }
    public void ToggleRagdoll() // Toggle the ragdoll
    {
        foreach (var col in colliders) // Skip the main collider
        {
            if (col.transform.gameObject == gameObject)
            {
                continue;
            }
                
            col.enabled = isRagdoll;
        }
        foreach (var rb in rigidbodies) // sets the rigidbody kinematic to isRagdoll
        {
            rb.isKinematic = !isRagdoll;
        }
        // Set the animator, agent, enemy and collider to the opposite of isRagdoll
        animator.enabled = !isRagdoll;
        agent.enabled = !isRagdoll;
        enemy.enabled = !isRagdoll;
        collider.enabled = !isRagdoll;
        
        isRagdoll = !isRagdoll;
    }
    public void SetRagdoll(bool value) // Set the ragdoll state
    {
        foreach (var col in colliders) 
        {
            if (col.transform.gameObject == gameObject) // Skip the main collider
            {
                continue;
            }
            col.enabled = value;
        }
        foreach (var rb in rigidbodies) // sets the rigidbody kinematic to value
        {
            rb.isKinematic = !value;
        }
        // same as before
        animator.enabled = !value;
        agent.enabled = !value;
        enemy.enabled = !value;
        collider.enabled = !value;
        
        isRagdoll = value;
    }
}
