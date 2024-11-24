using UnityEngine;
using UnityEditor.Animations;

public class PlayerAnimController : MonoBehaviour
{
    [SerializeField]private WeaponBehavior _weapon;
    [SerializeField]private WeaponBase _weaponData;
    [SerializeField]private GameObject _weaponPrefab;
    [SerializeField]private Animator _weaponAC;
    [SerializeField]private Animator _armAC;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitiateWeapon();
    }

    private void InitiateWeapon()
    {
        _weapon = GetComponent<WeaponBehavior>();
        _weaponData = _weapon.weaponData;
        _weaponPrefab = _weaponData.weaponPrefab;
        
        GameObject weapon = Instantiate(_weaponPrefab, Camera.main.transform);
        _armAC = weapon.transform.GetChild(0).GetComponent<Animator>();
        _weaponAC = weapon.transform.GetChild(1).GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {

    }
    public void Fire()
    {
        _weaponAC.SetTrigger("Fire");
        _armAC.SetTrigger("Fire");
    }
    public void Reload()
    {
        _weaponAC.SetTrigger("Reload");
        _armAC.SetTrigger("Reload");
    }
}
