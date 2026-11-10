using UnityEngine;
using StarterAssets;
using System.Collections;
using System;

public class Shoot : MonoBehaviour
{
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] WeaponSO weaponSO;
    GameObject hitParticles;
    public void HandleShoot(){
        muzzleFlash.Play();
        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity)){
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
