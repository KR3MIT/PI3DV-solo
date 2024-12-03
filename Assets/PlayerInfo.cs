using UnityEngine;

public class PlayerInfo : MonoBehaviour
{
    private FpsController fps;
    private PlayerInventory inv;
    private PlayerGUI gui;
    
    [SerializeField] private int health;
    void Start()
    {
        fps = GetComponent<FpsController>();
        inv = GetComponent<PlayerInventory>();
        gui = GetComponent<PlayerGUI>();
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        
        if (health <= 0)
            Die();
    }
    private void Die()
    {
        fps.enabled = false;
        inv.dropWeapon();
        inv.enabled = false;
        gui.DeathScreen();
    }
}