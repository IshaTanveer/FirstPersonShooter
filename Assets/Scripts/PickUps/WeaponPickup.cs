using UnityEngine;

public class WeaponPickup : Pickup
{
    public override void OnPickup(ActiveWeapon activeWeapon, WeaponSO weaponSO)
    {
        activeWeapon.SwitchWeapon(weaponSO);
    }
}
