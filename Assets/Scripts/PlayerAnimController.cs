using UnityEngine;
using UnityEditor.Animations;

public class PlayerAnimController : MonoBehaviour
{
    private WeaponBase weaponData;
    private GameObject weaponPrefab;
    private Animator weaponAC;
    private Animator armAC;
    private GameObject viewModel = null;
    public void InitiateWeapon(GameObject _currentWeapon)
    {
        Destroy(viewModel);
        Debug.Log("destoryed viewmodel");
        if (_currentWeapon == null)
            return;
        else
        {
            weaponData = _currentWeapon.GetComponent<WeaponItem>().weaponData;
            UpdateViewModel();
        }
    }
    private void UpdateViewModel()
    {
        weaponPrefab = weaponData.weaponPrefab;

        viewModel = Instantiate(weaponPrefab, Camera.main.transform);

        armAC = viewModel.transform.GetChild(0).GetComponent<Animator>();
        weaponAC = viewModel.transform.GetChild(1).GetComponent<Animator>();
    }
    public void Fire(bool isFiring)
    {
        weaponAC.SetBool("Fire", isFiring);
        armAC.SetBool("Fire", isFiring);
    }
    public void Reload()
    {
        weaponAC.SetTrigger("Reload");
        armAC.SetTrigger("Reload");
    }
}