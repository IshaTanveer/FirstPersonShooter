using UnityEngine;
using System.Collections;
using StarterAssets;

public class ActiveWeapon : MonoBehaviour
{
    float timeSinceLastShoot = 0f;
    StarterAssetsInputs starterAssetsInputs;
    Animator animator;
    const string SHOOT_STRING = "Recoil";
    [SerializeField] WeaponSO weaponSO;
    Shoot shoot;
    void Awake(){
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        shoot = GetComponentInChildren<Shoot>();
        animator = GetComponent<Animator>();
    }
    void Start(){
        StartCoroutine(HandleShoot());
    }
    IEnumerator HandleShoot(){
        while(true){
            timeSinceLastShoot += Time.deltaTime;
            if(!starterAssetsInputs.shoot) {
                yield return null;
                continue;
            }

            if (!weaponSO.IsAutomatic) starterAssetsInputs.ShootInput(false); // i waan set it to false regardless of the fire rate, so that the player has to press the button again to shoot again.

            if(timeSinceLastShoot < weaponSO.FireRate) {
                yield return null;
                continue;
            }
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShoot = 0f;
            shoot.HandleShoot();
            yield return null;
        }
    }
}
