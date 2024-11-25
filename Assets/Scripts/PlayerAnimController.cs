using UnityEngine;
using UnityEditor.Animations;
using UnityEditor.Timeline.Actions;

public class PlayerAnimController : MonoBehaviour
{
    [HideInInspector]public Animator weaponAC;
    private WeaponBase weaponData;
    private GameObject weaponPrefab;
    private Animator armAC;
    private GameObject viewModel = null;
    private FpsController fps;
    public void InitiateWeapon(GameObject _currentWeapon)
    {
        Destroy(viewModel);
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

    void Update()
    {
        DirectionalSway();
    }
    private void DirectionalSway()
    {
        //viewModel.transform
        Vector3 velocity = fps._velocity ;
    }
}