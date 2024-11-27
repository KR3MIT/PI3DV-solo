using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAnimController : MonoBehaviour
{
    [HideInInspector]public Animator weaponAC;
    private WeaponBase weaponData;
    private GameObject weaponPrefab;
    private Animator armAC;
    private GameObject viewModel = null;
    private FpsController fps;
    private VisualEffect muzzle;
    private ParticleSystem casing;
    [SerializeField] private float strafeSway = 1f;
    [SerializeField] private float kickStrenght;
    
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

        armAC = viewModel.GetComponent<ViewModelRef>().viewObject.GetComponent<Animator>();
        weaponAC = viewModel.GetComponent<ViewModelRef>().weaponObject.GetComponent<Animator>();

        muzzle = viewModel.GetComponent<ViewModelRef>().muzzle;
        casing = viewModel.GetComponent<ViewModelRef>().casing;
    }
    public void Fire(bool isFiring)
    {
        weaponAC.SetBool("Fire", isFiring);
        armAC.SetBool("Fire", isFiring);
        if (isFiring)
        {
            StartCoroutine(KickBack());
            muzzle.Play();
            casing.Play();
        }
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
    IEnumerator KickBack()
    {
        Transform _view = viewModel.GetComponent<ViewModelRef>().weaponObject.transform;

        Vector3 _kick = new Vector3(0, 0, -weaponData.kickBack * kickStrenght);

        _view.localPosition = Vector3.Lerp(_view.localPosition, _kick, 1.5f);

        yield return new WaitForSeconds(weaponData.fireRate);
    }
}