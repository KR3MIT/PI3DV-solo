using UnityEngine;

public class WeaponItem : MonoBehaviour
{
    public WeaponBase weaponData;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
        other.gameObject.GetComponentInParent<PlayerInventory>().addWeapon(weaponData);
        Debug.Log("picked up " + weaponData.weaponName);
        Destroy(gameObject);
        }
    }
}
