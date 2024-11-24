using UnityEngine;
using UnityEditor.Animations;

public class PlayerAnimController : MonoBehaviour
{
    private PlayerInventory _playerInv;
    private WeaponBase _weaponData;
    private GameObject _weaponPrefab;
    private Animator _weaponAC;
    private Animator _armAC;
    private GameObject viewModel = null;
    void Start()
    {
        _playerInv = GetComponent<PlayerInventory>();
    }
    public void InitiateWeapon(WeaponBase weaponData)
    {
        Destroy(viewModel);
        if (weaponData == null)
            return;
        else
            UpdateViewModel(weaponData);
    }
    private void UpdateViewModel(WeaponBase weaponData)
    {
        _weaponData = weaponData;
        _weaponPrefab = _weaponData.weaponPrefab;

        viewModel = Instantiate(_weaponPrefab, Camera.main.transform);

        _armAC = viewModel.transform.GetChild(0).GetComponent<Animator>();
        _weaponAC = viewModel.transform.GetChild(1).GetComponent<Animator>();
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
