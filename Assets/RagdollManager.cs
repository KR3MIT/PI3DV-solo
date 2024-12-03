using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class RagdollManager : MonoBehaviour
{
    private Collider[] colliders;
    private Rigidbody[] rigidbodies;

    public Animator animator;
    private NavMeshAgent agent;
    private Enemy enemy;

    public bool isRagdoll;
    void Start()
    {
        enemy = GetComponent<Enemy>();
        agent = GetComponent<NavMeshAgent>();
        
        colliders = GetComponentsInChildren<Collider>();
        rigidbodies = GetComponentsInChildren<Rigidbody>();
        
        SetRagdoll(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleRagdoll();
        }
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
        
        enemy.enabled = !value;
        animator.enabled = !value;
        agent.enabled = !value;
        
        isRagdoll = value;
    }
}
