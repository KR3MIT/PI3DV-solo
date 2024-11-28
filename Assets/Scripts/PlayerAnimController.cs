using System.Collections;
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
    private bool isFiring = false;
    private float time;
    [SerializeField] private float strafeSway = 1f;
    
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
    public void Fire(bool _isFiring)
    {
        weaponAC.SetBool("Fire", _isFiring);
        armAC.SetBool("Fire", _isFiring);
        if(!_isFiring)
            return;
        Debug.Log("Fire");
        time = 0;
        muzzle.Play();
        casing.Play();
        StartCoroutine(FireDelay());
    }
    IEnumerator FireDelay()
    {
        isFiring = true;
        yield return new WaitForSeconds(weaponData.fireRate);
        isFiring = false;
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
        
        if(isFiring)
            KickBack();
    }
    private void DirectionalSway()
    {   
        Transform _view = viewModel.GetComponent<ViewModelRef>().weaponObject.transform;

        float _velocity = fps._moveInput.x;
        float _sway = strafeSway * _velocity;

        Quaternion _swayVector = Quaternion.Euler(new Vector3( 0, 0, -_sway));
        _view.localRotation = Quaternion.Lerp(_view.localRotation, _swayVector, Time.deltaTime * 4f);
    }
    private void KickBack()
    {
        Transform _view = viewModel.GetComponent<ViewModelRef>().weaponObject.transform;
        time += Time.deltaTime;

        float _kick = weaponData.kickCurve.Evaluate(time);
        Debug.Log(_kick);
        Vector3 _kickVector = new Vector3(0, 0, -_kick * weaponData.kickStrenght);
        _view.localPosition = _kickVector;
    }
}