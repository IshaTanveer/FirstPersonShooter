using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] int maxHealth = 3;
    int currentHealth;
    public bool isDead {get; set;} = false;
    void Start(){
        currentHealth = maxHealth;
    }
    public void TakeDamage(int damage){
        currentHealth = currentHealth - damage;
        if(currentHealth <= 0) playerDeath();
    }
    void playerDeath(){
        if(isDead) return;
        isDead = true;
        Destroy(this.gameObject); ///Dont use destroy, use pooling instead
    }
}
