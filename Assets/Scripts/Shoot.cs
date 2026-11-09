using UnityEngine;
using StarterAssets;
using System.Collections;

public class Shoot : MonoBehaviour
{
    StarterAssetsInputs starterAssetsInputs;
    EnemyHealth EnemyHealth;
    [SerializeField] float damageAmount = 30f;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject hitParticles;
    [SerializeField] Animator animator;
    const string SHOOT_STRING = "Recoil";
    void Awake(){
        starterAssetsInputs = GetComponentInParent<StarterAssetsInputs>();
    }
    void Start(){
        StartCoroutine(HandleShoot());
    }
    IEnumerator HandleShoot(){
        while(true){
            if(!starterAssetsInputs.shoot) {
                yield return null;
                continue;
            }
            starterAssetsInputs.ShootInput(false);
            muzzleFlash.Play();
            animator.Play(SHOOT_STRING, 0, 0f);
            RaycastHit hit;
            if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity)){
                Debug.Log(hit.collider.name);
                Instantiate(hitParticles, hit.point, Quaternion.identity);
                EnemyHealth = hit.collider.GetComponent<EnemyHealth>();
                EnemyHealth?.TakeDamage(damageAmount);
            }
            yield return null;
        }
    }
}
