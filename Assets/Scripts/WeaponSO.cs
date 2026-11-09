using UnityEngine;

[CreateAssetMenu(fileName="WeaponSO", menuName = "ScriptableObjects/WeaponSO")]
public class WeaponSO : ScriptableObject
{
    public int Damage = 1;
    public float FireRate = 0.5f;
    public GameObject HitParticles;
    public bool IsAutomatic = false;
}
