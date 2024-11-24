using TMPro;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    [SerializeField] private TMP_Text text;

    private WeaponBehavior weapon;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        weapon = GetComponent<WeaponBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        ammo();
    }

    private void ammo()
    {
        text.text = weapon.magSize + " / " + weapon.currentAmmo;
    }
}
