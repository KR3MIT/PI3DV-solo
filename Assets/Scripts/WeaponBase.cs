using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.Animations.Rigging;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon")]
public class WeaponBase : ScriptableObject
{
    public string weaponName = "name";
    public GameObject weaponPrefab;
    public GameObject worldModelPrefab;
    public int maxAmmo = 150;
    public int magSize = 30;
    public int ammoInMag = 30;
    public int currentAmmo = 150;
    public float damage = 30f;
    public float fireRate = 0.1f;
    public float reloadTime = 1.5f;
    public AudioClip[] fireClips = default;
}
