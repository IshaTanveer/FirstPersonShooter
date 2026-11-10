using UnityEngine;

[CreateAssetMenu(fileName="WeaponSO", menuName = "ScriptableObjects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public GameObject WeaponPrefab;
    public int Damage = 1;
    public float FireRate = 0.5f;
    public GameObject HitParticles;
    public bool IsAutomatic = false;
    public bool CanZoom = false;
    public float ZoomFOV = 15f;
    public float ZoomRotationSpeed = 0.75f;
}
