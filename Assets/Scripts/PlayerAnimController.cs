using System.Collections;
using UnityEngine;
using UnityEngine.VFX;

public class PlayerAnimController : MonoBehaviour
{
    [HideInInspector]public Animator weaponAC;
    // Get the weapon data from player inventory and other components   
    private WeaponBase weaponData;
    private GameObject weaponPrefab;
    private Animator armAC;
    private GameObject viewModel = null;
    private FpsController fps;
    private VisualEffect muzzle;
    private VisualEffect casing;
    
    private bool isFiring = false;
    private float time;
    //  Sway the weapon when moving left or right
    [SerializeField] private float strafeSway = 1f;
    [SerializeField] private Light light = default;
    
    void Awake()
    {
        fps = GetComponent<FpsController>();
    }
    public void InitiateWeapon(GameObject _currentWeapon) // Get the weapon data from player inventory
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
    private void UpdateViewModel() // Update the view model with the weapon data
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
        // Fire the weapon
        if(viewModel == null)
            return;
        weaponAC.SetBool("Fire", _isFiring);
        armAC.SetBool("Fire", _isFiring);
        if(!_isFiring)
            return;
        time = 0;
        muzzle.Play();
        casing.Play();

        Light _light = Instantiate(light, muzzle.transform.position, Quaternion.identity);
        Destroy(_light.gameObject,.02f);

        StartCoroutine(FireDelay());
    }
    IEnumerator FireDelay()
    {
        // Delay between shots
        isFiring = true;
        yield return new WaitForSeconds(weaponData.fireRate);
        isFiring = false;
    }
    public void Reload()
    {
        // set reload animation
        weaponAC.SetTrigger("Reload");
        armAC.SetTrigger("Reload");
    }

    void Update()
    {
        // Update the weapon view model
        if(viewModel == null)
            return;
        DirectionalSway();
        
        if(isFiring)
            KickBack();
    }
    private void DirectionalSway()
    {   
        // Sway the weapon when moving left or right
        Transform _view = viewModel.GetComponent<ViewModelRef>().weaponObject.transform;

        float _velocity = fps._moveInput.x;
        float _sway = strafeSway * _velocity;

        Quaternion _swayVector = Quaternion.Euler(new Vector3( 0, 0, -_sway));
        _view.localRotation = Quaternion.Lerp(_view.localRotation, _swayVector, Time.deltaTime * 4f);
    }
    private void KickBack()
    {
        // Kick back the weapon when firing
        Transform _view = viewModel.GetComponent<ViewModelRef>().weaponObject.transform;
        time += Time.deltaTime;

        float _kick = weaponData.kickCurve.Evaluate(time);
        Vector3 _kickVector = new Vector3(0, 0, -_kick * weaponData.kickStrenght);
        _view.localPosition = _kickVector;
    }
}