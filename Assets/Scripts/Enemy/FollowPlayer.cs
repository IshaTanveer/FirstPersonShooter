using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using StarterAssets;

public class FollowPlayer : MonoBehaviour
{
    FirstPersonController player;
    NavMeshAgent agent;
    [SerializeField] Transform target;
    EnemyHealth EnemyHealth;
    void Awake(){
        player = FindAnyObjectByType<FirstPersonController>();
        agent = GetComponent<NavMeshAgent>();
        EnemyHealth = GetComponent<EnemyHealth>();
    }
    void Start(){
        StartCoroutine(FollowTarget());
    }
    IEnumerator FollowTarget(){
        while(!EnemyHealth.isDead){
            agent.SetDestination(player.transform.position);
            yield return null;
        }
    }
}
