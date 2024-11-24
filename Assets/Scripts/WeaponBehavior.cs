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
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
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
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    private void Fire()
    {
        if(ammoInMag > 0)
        {
            ammoInMag--;
            playerAC.Fire();
        }
        else
        {
            Reload();
        }
        playerAC.Fire();
    }
    private void Reload()
    {
        if(ammoInMag <= magSize - 1)
        {
        playerAC.Reload();
        }
    }
}
