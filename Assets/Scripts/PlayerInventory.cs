using JetBrains.Annotations;
using UnityEditor.Build.Player;
using UnityEngine;
public class PlayerInventory : MonoBehaviour
{
    public WeaponBase[] slots = new WeaponBase[2];
    public WeaponBase currentWeapon;
    public WeaponBase weaponData;
    public float dropForce = 10f;
    [SerializeField] private CapsuleCollider playerCollider;
    private PlayerAnimController playerAC;
    private WeaponBehavior weaponB;
    private int currentSlot;
    void Start()
    {
        playerAC = GetComponent<PlayerAnimController>();
        weaponB = GetComponent<WeaponBehavior>();

        SetCurrentWeapon();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            dropWeapon();
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SwitchWeapon();
        }
    }
    private void SetCurrentWeapon()
    {
        currentWeapon = slots[currentSlot];
        weaponData = currentWeapon;
        playerAC.InitiateWeapon(weaponData);
        
        if(weaponData != null)
        {
            currentWeapon = slots[currentSlot];
            weaponData = currentWeapon;
            weaponB.SetValues();    
        }
        
    }
    public void addWeapon(WeaponBase weapon)
    {
        if(slots[0] == null)
        {
            slots[0] = weapon;
            SetCurrentWeapon();
        }
        else if(slots[1] == null)
        {
            slots[1] = weapon;
            SetCurrentWeapon();
        }
    }
    private void dropWeapon()
    {
        if(currentWeapon != null)
        {
            GameObject droppedWeapon = Instantiate(currentWeapon.worldModelPrefab, Camera.main.transform.position + transform.forward, Camera.main.transform.rotation);
            droppedWeapon.GetComponent<WeaponItem>().weaponData = currentWeapon;
            Rigidbody rb = droppedWeapon.GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * dropForce, ForceMode.Impulse);
            
            currentWeapon = null;
            slots[currentSlot] = null;
            SetCurrentWeapon();
        }
    }
    private void SwitchWeapon()
    {
        if(currentSlot == 0)
        {
            currentSlot = 1;
            SetCurrentWeapon();
        }
        else
        {
            currentSlot = 0;
            SetCurrentWeapon();
        }
    }
}