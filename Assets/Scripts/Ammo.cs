using UnityEngine;
using TMPro;
using System.Collections;
using StarterAssets;

public class Ammo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentAmmoText;
    [SerializeField] TextMeshProUGUI totalAmmoText;
    [SerializeField] WeaponSO startingWeaponSO;
    WeaponSO currentWeaponSo;
    public int currentAmmo;
    public int totalAmmo;
    StarterAssetsInputs starterAssetsInputs;
    void Start()
    {
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        currentWeaponSo = startingWeaponSO;
        currentAmmo = currentWeaponSo.MagzineSize;
        totalAmmo = 0;
        UpdateAmmoUI(totalAmmo, totalAmmoText);
        UpdateAmmoUI(currentAmmo, currentAmmoText);
        StartCoroutine(ReloadAmmo());
    }
    IEnumerator ReloadAmmo()
    {
        while (true)
        {
            if(totalAmmo <= 0)
            {
                yield return null;
                continue;
            }
            if(starterAssetsInputs.reload) {
                if (currentAmmo <= 0 && totalAmmo > currentWeaponSo.MagzineSize)
                {
                    currentAmmo = currentWeaponSo.MagzineSize;
                    totalAmmo = totalAmmo - currentAmmo;
                }
                if (currentAmmo <= 0 && totalAmmo > 0)
                {
                    currentAmmo = totalAmmo;
                    totalAmmo = 0;
                }
                if(currentAmmo > 0 && currentAmmo < currentWeaponSo.MagzineSize)
                {
                    int ammoToLoad = currentWeaponSo.MagzineSize - currentAmmo;
                    if(totalAmmo > ammoToLoad){ 
                        currentAmmo = currentWeaponSo.MagzineSize;
                        totalAmmo -= ammoToLoad;
                    }
                    if(totalAmmo <= ammoToLoad)
                    {
                        currentAmmo += totalAmmo;
                        totalAmmo = 0;
                    }
                }
                UpdateAmmoUI(totalAmmo, totalAmmoText);
                UpdateAmmoUI(currentAmmo, currentAmmoText); 
                starterAssetsInputs.ReloadInput(false);
                yield return null;
            }
            yield return null;
        }
    }
    public void AdjustAmmo(WeaponSO weaponSO)
    {
        currentWeaponSo = weaponSO;
        currentAmmo = currentWeaponSo.MagzineSize;
        totalAmmo = 0;
        UpdateAmmoUI(currentAmmo, currentAmmoText);
        UpdateAmmoUI(totalAmmo, totalAmmoText);
    }
    public void AdjustCurrentAmmo(int amount)
    {
        currentAmmo += amount;
        UpdateAmmoUI(currentAmmo, currentAmmoText);
    }
    public void AdjustTotalAmmo(int amount)
    {
        totalAmmo += amount;
        UpdateAmmoUI(totalAmmo, totalAmmoText);
    }
    void UpdateAmmoUI(int ammo, TextMeshProUGUI ammoText)
    {
        ammoText.text = ammo.ToString("D2");
    }
}
