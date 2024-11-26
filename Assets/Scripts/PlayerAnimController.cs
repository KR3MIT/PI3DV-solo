using UnityEngine;
using UnityEditor.Animations;
using UnityEditor.Timeline.Actions;
using Unity.VisualScripting;
using Unity.Mathematics;
using UnityEngine.Rendering;

public class PlayerAnimController : MonoBehaviour
{
    [HideInInspector]public Animator weaponAC;
    private WeaponBase weaponData;
    private GameObject weaponPrefab;
    private Animator armAC;
    private GameObject viewModel = null;
    private FpsController fps;
    public float strafeSway = 1f;
    void Awake()
    {
        fps = GetComponent<FpsController>();
    }
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
        if(viewModel == null)
            return;
            DirectionalSway();
    }
    private void DirectionalSway()
    {   
        Transform _view = viewModel.transform.GetChild(1);

        float _velocity = fps._moveInput.x;
        float _sway = strafeSway * _velocity;

        Quaternion _swayVector = Quaternion.Euler(new Vector3( 0, 0, -_sway));
        _view.localRotation = Quaternion.Lerp(_view.localRotation, _swayVector, Time.deltaTime * 4f);
    }
}