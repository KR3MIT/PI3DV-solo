using UnityEngine;
public class PlayerInventory : MonoBehaviour
{
    // Player inventory
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
        // Drop the weapon
        if(Input.GetKeyDown(KeyCode.G))
            dropWeapon();
        // Switch the weapon
        if(Input.GetKeyDown(KeyCode.Q))
            SwitchWeapon();
    }
    private void SetCurrentWeapon() // Set the current weapon
    {
        currentWeapon = slots[currentSlot];
        weaponB.SetValues();
        playerAC.InitiateWeapon(currentWeapon);
    }
    public void addWeapon(GameObject _weapon) // Add a weapon to the inventory
    {
        if(slots[0] == null)
            slots[0] = _weapon;       
        else if(slots[1] == null)
            slots[1] = _weapon;
        SetCurrentWeapon();
    }
    public void dropWeapon() // Drop the weapon
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
    private void SwitchWeapon() // Switch the weapon
    {
        if(currentSlot == 0)
            currentSlot = 1;
        else
            currentSlot = 0;
        
        SetCurrentWeapon();
    }
}