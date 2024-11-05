using UnityEngine;

public class DamageOnTouch : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1; //dmg num

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            //remove health, take away points, and reset streak
            playerHealth.TakeDamage(damageAmount);
            FindObjectOfType<EnemyController>().ResetSuccessfulClicks();
            FindObjectOfType<PlayerHealth>().AddPoints(-50);
        }
    }
}
