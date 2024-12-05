using UnityEngine;
using UnityEngine.VFX;

public class EnemyVFX : MonoBehaviour
{
    [SerializeField] private VisualEffect muzzle;
    [SerializeField] private VisualEffect casing;
    [SerializeField] private Light muzzleLight = default;
    public void FireWeaponVFX()
    {
        muzzle.Play();
        casing.Play();

        Light _light = Instantiate(muzzleLight, muzzle.transform.position, Quaternion.identity);
        Destroy(_light.gameObject,.02f);
    }
}
