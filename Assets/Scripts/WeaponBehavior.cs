using UnityEngine;
using UnityEditor.Animations;

public class WeaponBehavior : MonoBehaviour
{
    public WeaponBase weaponData;
    public string weaponName = "name";
    private PlayerAnimController playerAC;
    private int maxAmmo;
    [HideInInspector]public int magSize;
    public int currentAmmo;
    private float damage;
    private float fireRate;
    private int ammoInMag;
    [SerializeField]private AudioSource audioSource;
    [SerializeField]private AudioClip[] fireClips = default;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetValues();
        SetAudio();
    }
    private void SetAudio()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void SetValues()
    {
        playerAC = GetComponent<PlayerAnimController>();
        WeaponBase weapon = (WeaponBase)weaponData;
        weaponName = weapon.weaponName;
        maxAmmo = weapon.maxAmmo;
        magSize = weapon.magSize;
        currentAmmo = weapon.currentAmmo;
        damage = weapon.damage;
        fireRate = weapon.fireRate;
        ammoInMag = weapon.ammoInMag;

        ammoInMag = magSize;
        currentAmmo = maxAmmo;
    }
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }
    private void Fire()
    {
            playerAC.Fire();

    }
    private void Reload()
    {
        
        playerAC.Reload();
    }
}
