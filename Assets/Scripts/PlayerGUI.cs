using TMPro;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text invText;
    [SerializeField] private TMP_Text healthText;
    [SerializeField] private Animator anim;
    public void Inventory(int inv)
    {
        invText.text = inv.ToString();
    }
    public void Ammo(int ammoInMag, int currentAmmo)
    {
        ammoText.text = ammoInMag + " / " + currentAmmo;
    }
    public void DeathScreen()
    {
        anim.SetBool("isDead", true);
    }
    public void Health(int health)
    {
        healthText.text = health.ToString();
        anim.SetTrigger("hit");
    }
}
