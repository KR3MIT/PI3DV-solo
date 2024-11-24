using TMPro;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text invText;
    private void ammo(WeaponBase _weaponData)
    {
        ammoText.text = _weaponData.ammoInMag + " / " + _weaponData.currentAmmo;
    }
}
