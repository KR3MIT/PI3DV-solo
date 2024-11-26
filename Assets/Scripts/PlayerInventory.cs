using UnityEditor.Build.Player;
using UnityEngine;
public class PlayerInventory : MonoBehaviour
{
    public GameObject[] slots = new GameObject[2];
    public GameObject currentWeapon;
    public float dropForce = 10f;
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
            dropWeapon();
        
        if(Input.GetKeyDown(KeyCode.Q))
            SwitchWeapon();
    }
    private void SetCurrentWeapon()
    {
        currentWeapon = slots[currentSlot];
        weaponB.SetValues();
        playerAC.InitiateWeapon(currentWeapon);
    }
    public void addWeapon(GameObject _weapon)
    {
        if(slots[0] == null)
            slots[0] = _weapon;       
        else if(slots[1] == null)
            slots[1] = _weapon;
        
         SetCurrentWeapon();
    }
    private void dropWeapon()
    {
        if(currentWeapon != null)
        {
            currentWeapon.SetActive(true);
            currentWeapon.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
            currentWeapon.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * dropForce, ForceMode.Impulse);
            currentWeapon = null;
            slots[currentSlot] = null;
        }
        SetCurrentWeapon();
    }
    private void SwitchWeapon()
    {
        if(currentSlot == 0)
            currentSlot = 1;
        else
            currentSlot = 0;
        
        SetCurrentWeapon();
    }
}