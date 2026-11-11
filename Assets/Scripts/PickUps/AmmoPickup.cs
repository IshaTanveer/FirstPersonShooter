using UnityEngine;

public class AmmoPickup : Pickup
{
    public override void OnPickup(ActiveWeapon activeWeapon)
    {
        Ammo ammo = activeWeapon.GetComponent<Ammo>();
        ammo.AdjustTotalAmmo(50);
    }
}
