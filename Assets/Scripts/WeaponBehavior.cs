using UnityEngine;
public class WeaponBehavior : MonoBehaviour
{
    public string weaponName = "name";
    private WeaponBase weaponData;
    private WeaponItem WeaponItem;
    private PlayerInventory playerInv;
    private PlayerAnimController playerAC;
    private int maxAmmo;
    private int magSize;
    private float damage;
    private float fireRate;
    private AudioSource audioSource;
    private AudioClip[] fireClips = default;
    private PlayerGUI playerGUI;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        playerGUI = GetComponent<PlayerGUI>();
        playerInv = GetComponent<PlayerInventory>();
        playerAC = GetComponent<PlayerAnimController>();
    }
    public void SetValues()
    {
        if(playerInv.currentWeapon == null)
        {
            Debug.Log("Weapon data is null");
            playerGUI.ammo(0, 0);
            return;
        }
        weaponData = playerInv.currentWeapon.GetComponent<WeaponItem>().weaponData;
        WeaponItem = playerInv.currentWeapon.GetComponent<WeaponItem>();
    
        weaponName = weaponData.weaponName;
        maxAmmo = weaponData.maxAmmo;
        magSize = weaponData.magSize;
        damage = weaponData.damage;
        fireRate = weaponData.fireRate;
        fireClips = weaponData.fireClips;
        
        playerGUI.ammo(WeaponItem.ammoInMag, WeaponItem.currentAmmo);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
            Reload();

        if(Input.GetMouseButton(0))
            Fire();

    }
    private void Fire()
    {
        if(Time.time >= fireRate && WeaponItem.ammoInMag > 0)
        {
            playerAC.Fire(true);
            WeaponItem.ammoInMag--;
            fireRate = Time.time + weaponData.fireRate;
            playerAC.Fire(false);
            playerGUI.ammo(WeaponItem.ammoInMag, WeaponItem.currentAmmo);
        }
        else if(WeaponItem.ammoInMag == 0)
        {
            Reload();
        }
    }
    private void Reload()
    {
        if(WeaponItem.currentAmmo > 0 && WeaponItem.ammoInMag < magSize)
        {
            playerAC.Reload();
            int ammoNeeded = magSize - WeaponItem.ammoInMag;
            if(WeaponItem.currentAmmo >= ammoNeeded)
            {
                WeaponItem.currentAmmo -= ammoNeeded;
                WeaponItem.ammoInMag = magSize;
            }
            else if (WeaponItem.currentAmmo < ammoNeeded)
            {
                WeaponItem.ammoInMag += WeaponItem.currentAmmo;
                WeaponItem.currentAmmo = 0;
            }
        }
        playerGUI.ammo(WeaponItem.ammoInMag, WeaponItem.currentAmmo);
    }
}
