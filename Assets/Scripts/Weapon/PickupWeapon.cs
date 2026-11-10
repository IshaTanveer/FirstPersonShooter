using UnityEngine;

public class PickupWeapon : MonoBehaviour
{
    [SerializeField] WeaponSO weaponSO;
    const string PLAYER_TAG = "Player";
    void OnTriggerEnter(Collider other){
        if (other.CompareTag(PLAYER_TAG))
        {
            ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
            if(activeWeapon != null){
                activeWeapon.SwitchWeapon(weaponSO);
                Destroy(gameObject);
                // destroy this pickup (add it to the pool instead of destroying it)
            }
        }
    }
}
