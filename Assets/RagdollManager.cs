using UnityEngine;
using UnityEngine.AI;

public class RagdollManager : MonoBehaviour
{
    private Collider[] colliders;
    private Rigidbody[] rigidbodies;

    public Animator animator;
    private NavMeshAgent agent;
    private Enemy enemy;
    private CapsuleCollider collider;

    public bool isRagdoll;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        enemy = GetComponent<Enemy>();
        collider = GetComponent<CapsuleCollider>();
        
        colliders = GetComponentsInChildren<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        
        SetRagdoll(false);
    }
    public void ToggleRagdoll()
    {
        foreach (var col in colliders)
        {
            if (col.transform.gameObject == gameObject)
            {
                continue;
            }
                
            col.enabled = isRagdoll;
        }
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = !isRagdoll;
        }
        
        animator.enabled = !isRagdoll;
        agent.enabled = !isRagdoll;
        enemy.enabled = !isRagdoll;
        collider.enabled = !isRagdoll;
        
        isRagdoll = !isRagdoll;
    }
    public void SetRagdoll(bool value)
    {
        foreach (var col in colliders)
        {
            if (col.transform.gameObject == gameObject)
            {
                continue;
            }
            col.enabled = value;
        }
        foreach (var rb in rigidbodies)
        {
            rb.isKinematic = !value;
        }
        
        animator.enabled = !value;
        agent.enabled = !value;
        enemy.enabled = !value;
        collider.enabled = !value;
        
        isRagdoll = value;
    }
}
