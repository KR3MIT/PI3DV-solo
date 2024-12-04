using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public WeaponBase weaponData;
    public int ammoInMag;
    public int currentAmmo;
    private void OnTriggerEnter(Collider _other)
    {
        if(_other.gameObject.CompareTag("Player"))
        {
        _other.gameObject.GetComponentInParent<PlayerInventory>().addWeapon(this.gameObject);
        gameObject.SetActive(false);
        }
    }
    public void updateValues(int _ammoInMag, int _currentAmmo)
    {
        ammoInMag = _ammoInMag;
        currentAmmo = _currentAmmo;
    }
    void Start()
    {
        updateValues(weaponData.magSize, weaponData.maxAmmo);
    }
}
