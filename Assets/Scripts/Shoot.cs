using UnityEngine;
using StarterAssets;
using System.Collections;
using System;
using Unity.Cinemachine;

public class Shoot : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] WeaponSO weaponSO;
    [SerializeField] LayerMask interactionLayers;
    GameObject hitParticles;
    CinemachineImpulseSource impulseSource;
    void Awake()
    {
        impulseSource = GetComponent<CinemachineImpulseSource>();
    }
    public void HandleShoot(){
        muzzleFlash.Play();
        impulseSource.GenerateImpulse();

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, interactionLayers, QueryTriggerInteraction.Ignore)){
            //Debug.Log(hit.collider.name);
            hitParticles = Instantiate(weaponSO.HitParticles, hit.point, Quaternion.identity); 
            EnemyHealth EnemyHealth = hit.collider.GetComponent<EnemyHealth>();
            EnemyHealth?.TakeDamage(weaponSO.Damage);
            Invoke("DestroyHitParticles", 0.5f);
        }
    }

    private void DestroyHitParticles()
    {
        Destroy(hitParticles);
    }
}
