using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponBase : ScriptableObject
{
    public string weaponName = "name";
    public GameObject weaponPrefab;
    public int maxAmmo = 150;
    public int magSize = 30;
    public int ammoInMag;
    public int currentAmmo;
    public float damage = 30f;
    public float fireRate = 0.1f;
    public AudioClip[] fireClips = default;
}
