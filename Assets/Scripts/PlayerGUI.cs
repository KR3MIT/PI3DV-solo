using TMPro;
using UnityEngine;

public class PlayerGUI : MonoBehaviour
{
    [SerializeField] private TMP_Text ammoText;
    [SerializeField] private TMP_Text invText;
    public void ammo(int ammoInMag, int currentAmmo)
    {
        ammoText.text = ammoInMag + " / " + currentAmmo;
    }
}
