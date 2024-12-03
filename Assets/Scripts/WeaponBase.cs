using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponBase : ScriptableObject
{
    [Header("Weapon Visual")]
    public string weaponName = "name";
    public GameObject weaponPrefab;
    public GameObject worldModelPrefab;
    [Header("Weapon Stats")]
    public int maxAmmo = 150;
    public int magSize = 30;
    public int ammoInMag = 30;
    public int currentAmmo = 150;
    public int damage = 30;
    public float fireRate = 0.1f;
    public float reloadTime = 1.5f;
    [Header("Weapon Audio")]
    public AudioClip[] fireClips = default;
    [Header("Weapon Kick")]
    public float force = 10f;
    public float kickStrenght = 1;
    [Tooltip("Adjust x axis according to fire rate")]
    public AnimationCurve kickCurve;
}