using TMPro;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text invText;
    [SerializeField] private TMP_Text healthText;
    public void Ammo(int ammoInMag, int currentAmmo)
    {
        ammoText.text = ammoInMag + " / " + currentAmmo;
    }
    public void DeathScreen()
    {
        
    }
    public void Health(int health)
    {
        healthText.text = health.ToString();
    }
}
