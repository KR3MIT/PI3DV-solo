using UnityEngine;
using UnityEngine.Events;

public class TriggerBox : MonoBehaviour
{
    public UnityEvent OnEnter;
    private void OnTriggerEnter(Collider other)
    {
        this.gameObject.SetActive(false);
        OnEnter.Invoke();
    }
}
