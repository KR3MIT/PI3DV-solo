using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public WeaponBase weaponData; // Weapon data
    public int ammoInMag;
    public int currentAmmo;
    private void OnTriggerEnter(Collider _other) // When the player enters the trigger box
    {
        if(_other.gameObject.CompareTag("Player"))
        {
            _other.gameObject.GetComponentInParent<PlayerInventory>().addWeapon(this.gameObject); // Add the weapon to the player inventory
            gameObject.SetActive(false);
        }
    }
    public void updateValues(int _ammoInMag, int _currentAmmo) // Update the weapon values
    {
        ammoInMag = _ammoInMag;
        currentAmmo = _currentAmmo;
    }
    void Start() // Set the weapon values
    {
        updateValues(weaponData.magSize, weaponData.maxAmmo);
    }
}
