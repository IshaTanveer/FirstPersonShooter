using UnityEngine;
using System.Collections;
using StarterAssets;
using Unity.Cinemachine;
using UnityEngine.UI;

public class ActiveWeapon : MonoBehaviour
{
    float timeSinceLastShoot = 0f;
    StarterAssetsInputs starterAssetsInputs;
    Animator animator;
    const string SHOOT_STRING = "Recoil";
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] CinemachineCamera playerFollowCamera;
    [SerializeField] Image zoomVignette;
    float originalFOV;
    float defaultRotationSpeed;
    Shoot currentWeapon;
    FirstPersonController firstPersonController;
    void Awake(){
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
        currentWeapon = GetComponentInChildren<Shoot>();
        animator = GetComponent<Animator>();
        originalFOV = playerFollowCamera.Lens.FieldOfView;
        firstPersonController = GetComponentInParent<FirstPersonController>();
        defaultRotationSpeed = firstPersonController.RotationSpeed;
    }
    void Start(){
        StartCoroutine(HandleShoot());
        StartCoroutine(HandleZoom());
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
            currentWeapon.HandleShoot();
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
            this.weaponSO = weaponSO;
        }
        Debug.Log("Player picked up " + weaponSO.name);
    }
    IEnumerator HandleZoom()
    {
        while (true)
        {
            if(!weaponSO.CanZoom) {
                yield return null;
                continue;
            }
            if(starterAssetsInputs.zoom){
                playerFollowCamera.Lens.FieldOfView = weaponSO.ZoomFOV;
                firstPersonController.changeRotationSpeed(weaponSO.ZoomRotationSpeed);
                zoomVignette.enabled = true;
            }
            else if (!starterAssetsInputs.zoom){
                playerFollowCamera.Lens.FieldOfView = originalFOV;
                firstPersonController.changeRotationSpeed(defaultRotationSpeed);
                zoomVignette.enabled = false;
            }
            yield return null;
        }
    }
}
