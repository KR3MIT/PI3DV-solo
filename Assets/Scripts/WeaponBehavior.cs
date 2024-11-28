using System.Collections;
using NUnit.Framework;
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
    private bool isReloading = false;
    private float reloadTime;
    private bool isFiring = false;
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
        reloadTime = weaponData.reloadTime;

        isReloading = false;
        
        playerGUI.ammo(WeaponItem.ammoInMag, WeaponItem.currentAmmo);

        StopAllCoroutines();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && !isReloading)
            StartCoroutine(Reload());
        
        if(Input.GetMouseButton(0) && !isReloading && !isFiring)
        {
            StartCoroutine(Fire());
        }

    }
    IEnumerator Fire()
    {
        if(WeaponItem.ammoInMag > 0 && !isReloading)
        {
            isFiring = true;
            playerAC.Fire(true);
            WeaponItem.ammoInMag--;
            fireRate = Time.time + weaponData.fireRate;
            playerGUI.ammo(WeaponItem.ammoInMag, WeaponItem.currentAmmo);
            yield return new WaitForSeconds(weaponData.fireRate);
            isFiring = false;
        }
        else if(WeaponItem.ammoInMag == 0 && !isReloading)
        {
            StartCoroutine(Reload());
        }
        playerAC.Fire(false);
        isFiring = false;
    }
    private void ShootRaycast()
    {
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 100))
        {
            if(hit.collider.gameObject.tag == "Enemy")
            {
                hit.collider.gameObject.GetComponent<Enemy>().TakeDamage(damage);
            }
        }
    }
    IEnumerator Reload()
    {
        if(WeaponItem.currentAmmo > 0 && WeaponItem.ammoInMag < magSize)
        {
            isReloading = true;
            playerAC.Reload();

            yield return new WaitForSeconds(reloadTime);
            
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
            isReloading = false;
        }
        playerGUI.ammo(WeaponItem.ammoInMag, WeaponItem.currentAmmo);
    }
    // private void Fire()
    // {
    //     if(Time.time >= fireRate && WeaponItem.ammoInMag > 0 && !isReloading)
    //     {
    //         playerAC.Fire(true);
    //         WeaponItem.ammoInMag--;
    //         fireRate = Time.time + weaponData.fireRate;
    //         playerGUI.ammo(WeaponItem.ammoInMag, WeaponItem.currentAmmo);
    //     }
    //     else if(WeaponItem.ammoInMag == 0 && !isReloading)
    //     {
    //         StartCoroutine(Reload());
    //     }
    // }
    // private void Reload()
    // {
    //     if(WeaponItem.currentAmmo > 0 && WeaponItem.ammoInMag < magSize)
    //     {
    //         playerAC.Reload();
    //         int ammoNeeded = magSize - WeaponItem.ammoInMag;
    //         if(WeaponItem.currentAmmo >= ammoNeeded)
    //         {
    //             WeaponItem.currentAmmo -= ammoNeeded;
    //             WeaponItem.ammoInMag = magSize;
    //         }
    //         else if (WeaponItem.currentAmmo < ammoNeeded)
    //         {
    //             WeaponItem.ammoInMag += WeaponItem.currentAmmo;
    //             WeaponItem.currentAmmo = 0;
    //         }
    //     }
    //     playerGUI.ammo(WeaponItem.ammoInMag, WeaponItem.currentAmmo);
    // }
}
