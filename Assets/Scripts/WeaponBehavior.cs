using UnityEngine;

public class WeaponBehavior : MonoBehaviour
{
    public string weaponName = "name";
    public int currentAmmo;
    public int ammoInMag;
    private WeaponBase weaponData;
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
    }
    public void SetValues()
    {
        playerInv = GetComponent<PlayerInventory>();
        playerAC = GetComponent<PlayerAnimController>();

        weaponData = playerInv.weaponData;

        weaponName = weaponData.weaponName;
        maxAmmo = weaponData.maxAmmo;
        magSize = weaponData.magSize;
        currentAmmo = weaponData.currentAmmo;
        ammoInMag = weaponData.ammoInMag;
        damage = weaponData.damage;
        fireRate = weaponData.fireRate;
        fireClips = weaponData.fireClips;

        ammoInMag = magSize;
        currentAmmo = maxAmmo;

        playerGUI.ammo(ammoInMag, currentAmmo);
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
        if(Time.time >= fireRate && ammoInMag > 0)
        {
            playerAC.Fire(true);
            ammoInMag--;
            fireRate = Time.time + weaponData.fireRate;
            playerAC.Fire(false);
            playerGUI.ammo(ammoInMag, currentAmmo);
        }
        else if(ammoInMag == 0)
        {
            Reload();
        }
    }
    private void Reload()
    {
        if(currentAmmo > 0 && ammoInMag < magSize)
        {
            playerAC.Reload();
            int ammoNeeded = magSize - ammoInMag;
            if(currentAmmo >= ammoNeeded)
            {
                currentAmmo -= ammoNeeded;
                ammoInMag = magSize;
            }
            else if (currentAmmo < ammoNeeded)
            {
                ammoInMag += currentAmmo;
                currentAmmo = 0;
            }
        }
        playerGUI.ammo(ammoInMag, currentAmmo);
    }
}
