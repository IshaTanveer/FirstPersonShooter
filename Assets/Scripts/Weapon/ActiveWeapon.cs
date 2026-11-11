using UnityEngine;
using System.Collections;
using StarterAssets;
using Unity.Cinemachine;
using UnityEngine.UI;
using TMPro;

public class ActiveWeapon : MonoBehaviour
{
    [SerializeField] CinemachineCamera playerFollowCamera;
    [SerializeField] Camera WeaponCamera;
    [SerializeField] Image zoomVignette;
    [SerializeField] WeaponSO startingWeaponSO;
    float timeSinceLastShoot = 0f;
    StarterAssetsInputs starterAssetsInputs;
    Animator animator;
    const string SHOOT_STRING = "Recoil";
    WeaponSO currentWeaponSO;
    float originalFOV;
    float defaultRotationSpeed;
    Shoot currentWeapon;
    FirstPersonController firstPersonController;
    Ammo ammo;
    void Awake(){
        ammo = GetComponent<Ammo>();
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        currentWeapon = GetComponentInChildren<Shoot>();
        animator = GetComponent<Animator>();
        originalFOV = playerFollowCamera.Lens.FieldOfView;
        firstPersonController = GetComponentInParent<FirstPersonController>();
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }
    void Start(){
        SwitchWeapon(startingWeaponSO);
        StartCoroutine(HandleShoot());
        StartCoroutine(HandleZoom());
    }
    IEnumerator HandleShoot(){
        while(true){
            if(ammo.currentAmmo <= 0){
                yield return null;
                continue;
            }
            timeSinceLastShoot += Time.deltaTime;
            if(!starterAssetsInputs.shoot) {
                yield return null;
                continue;
            }

            if (!currentWeaponSO.IsAutomatic) starterAssetsInputs.ShootInput(false); // i waan set it to false regardless of the fire rate, so that the player has to press the button again to shoot again.

            if(timeSinceLastShoot < currentWeaponSO.FireRate) {
                yield return null;
                continue;
            }
            animator.Play(SHOOT_STRING, 0, 0f);
            timeSinceLastShoot = 0f;
            currentWeapon.HandleShoot();
            ammo.AdjustCurrentAmmo(-1);
            yield return null;
        }
    }
    public void SwitchWeapon(WeaponSO weaponSO)
    {
        if (currentWeapon)
        {
            Destroy(currentWeapon.gameObject);
            Shoot newWeapon = Instantiate(weaponSO.WeaponPrefab, transform).GetComponent<Shoot>();
            currentWeapon = newWeapon;
            this.currentWeaponSO = weaponSO;
            ammo.AdjustAmmo(weaponSO);
        }
        Debug.Log("Player picked up " + weaponSO.name);
    }
    IEnumerator HandleZoom()
    {
        while (true)
        {
            if(!currentWeaponSO.CanZoom) {
                yield return null;
                continue;
            }
            if(starterAssetsInputs.zoom){
                playerFollowCamera.Lens.FieldOfView = currentWeaponSO.ZoomFOV;
                WeaponCamera.fieldOfView = currentWeaponSO.ZoomFOV;
                firstPersonController.changeRotationSpeed(currentWeaponSO.ZoomRotationSpeed);
                zoomVignette.enabled = true;
            }
            else if (!starterAssetsInputs.zoom){
                playerFollowCamera.Lens.FieldOfView = originalFOV;
                WeaponCamera.fieldOfView = originalFOV;
                firstPersonController.changeRotationSpeed(defaultRotationSpeed);
                zoomVignette.enabled = false;
            }
            yield return null;
        }
    }
}
