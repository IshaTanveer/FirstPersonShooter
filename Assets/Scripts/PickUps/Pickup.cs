using System;
using UnityEngine;
using System.Collections;

public abstract class Pickup : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 90f;
    const string PLAYER_TAG = "Player";
    void Start()
    {
        StartCoroutine(RotatePickup());
    }

    IEnumerator RotatePickup()
    {
        while (true)
        {
            transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
            yield return null;
        }
    }

    public abstract void OnPickup(ActiveWeapon activeWeapon);
    void OnTriggerEnter(Collider other){
        if (!other.CompareTag(PLAYER_TAG)) return;
        ActiveWeapon activeWeapon = other.GetComponentInChildren<ActiveWeapon>();
        if(activeWeapon == null) return;
        OnPickup(activeWeapon);
        Destroy(gameObject);
    }
}
