using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] float maxHealth = 90f;
    float currentHealth;
    public bool isDead {get; set;} = false;
    void Start(){
        currentHealth = maxHealth;
    }
    public void TakeDamage(float damage){
        currentHealth = currentHealth - damage;
        if(currentHealth <= 0) playerDeath();
    }
    void playerDeath(){
        if(isDead) return;
        isDead = true;
        Destroy(this.gameObject); ///Dont use destroy, use pooling instead
    }
}
