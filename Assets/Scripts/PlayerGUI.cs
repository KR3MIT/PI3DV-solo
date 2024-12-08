using TMPro;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    // Update the player GUI with the player stats
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text invText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Animator anim;
    public void Inventory(int inv)
    // Update the player inventory
    {
        invText.text = inv.ToString();
    }
    public void Ammo(int ammoInMag, int currentAmmo)
    // Update the player ammo
    {
        ammoText.text = ammoInMag + " / " + currentAmmo;
    }
    public void DeathScreen()
    // Show the death screen
    {
        anim.SetBool("isDead", true);
    }
    public void Health(int health)
    // Update the player health
    {
        healthText.text = health.ToString();
        anim.SetTrigger("hit");
    }
}
