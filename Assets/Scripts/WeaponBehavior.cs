using System.Collections;
using UnityEngine;
public class WeaponBehavior : MonoBehaviour
{
    public string weaponName = "name";
    
    private WeaponBase weaponData;
    private WeaponItem WeaponItem;
    private PlayerInventory playerInv;
    private PlayerAnimController playerAC;
    private AudioSource audioSource;
    private AudioClip[] fireClips = default;
    private PlayerGUI playerGUI;
    
    private float fireRate;
    private int magSize;
    private int damage;
    private float reloadTime;
    private bool isReloading = false;
    private bool isFiring = false;
    
    [SerializeField]private LayerMask layer = default;
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
            playerGUI.Ammo(0, 0);
            return;
        }
        weaponData = playerInv.currentWeapon.GetComponent<WeaponItem>().weaponData;
        WeaponItem = playerInv.currentWeapon.GetComponent<WeaponItem>();
    
        weaponName = weaponData.weaponName;
        magSize = weaponData.magSize;
        damage = weaponData.damage;
        fireClips = weaponData.fireClips;
        reloadTime = weaponData.reloadTime;
        fireRate = weaponData.fireRate;

        isReloading = false;
        
        playerGUI.Ammo(WeaponItem.ammoInMag, WeaponItem.currentAmmo);

        StopAllCoroutines();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && !isReloading)
            StartCoroutine(Reload());
        
        if(Input.GetMouseButton(0) && !isReloading && !isFiring)
            StartCoroutine(Fire());
    }
    IEnumerator Fire()
    {
        if(WeaponItem.ammoInMag > 0 && !isReloading)
        {
            isFiring = true;
            playerAC.Fire(true);
            WeaponItem.ammoInMag--;
            
            ShootRaycast();
            
            fireRate = Time.time + weaponData.fireRate;
            playerGUI.Ammo(WeaponItem.ammoInMag, WeaponItem.currentAmmo);
            
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
        Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, 100, layer);
        if(hit.collider == null)
            return;
        
        if(hit.collider.CompareTag("Enemy"))
        {
            Enemy _enemy = hit.collider.GetComponent<Enemy>();
            _enemy.TakeDamage(damage);
            
            string _name = _enemy.gameObject.name;
            float _health = _enemy.health;
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
        playerGUI.Ammo(WeaponItem.ammoInMag, WeaponItem.currentAmmo);
    }
}
