using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private bool isClicked = false; //track if enemy is clicked
    private Collider2D enemyCollider;

    private static int successfulClicks = 0; //track successful clicks across enemies

    [Header("Animation Speed Settings")]
    public float spawnSpeed = 1.0f;
    public float hitSpeed = 0.5f;

    [Header("Lifetime Settings")]
    public float idleDuration = 1.0f;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyCollider = GetComponent<Collider2D>();
        enemyCollider.enabled = false; //disable until clickable

        animator.SetFloat("SpawnSpeed", spawnSpeed);
        Invoke(nameof(EnableClick), spawnSpeed); //enable click after spawn animation
    }

    private void EnableClick()
    {
        animator.SetBool("CanClick", true);
        enemyCollider.enabled = true;
        Invoke(nameof(Despawn), idleDuration); //despawn after idle duration
    }

    private void OnMouseDown()
    {
        if (animator.GetBool("CanClick") && !isClicked)
        {
            isClicked = true;
            FindObjectOfType<PlayerHealth>().AddPoints(100);
            TriggerHitAnimation();
            CancelInvoke(nameof(Despawn)); //cancel despawn if clicked

            successfulClicks++;
            if (successfulClicks == 4) //restore health every 4 clicks
            {
                FindObjectOfType<PlayerHealth>().RestoreHealth(1);
                successfulClicks = 0;
            }
        }
    }

    public void ResetSuccessfulClicks() //manual reset of click counter
    {
        successfulClicks = 0;
    }

    private void TriggerHitAnimation()
    {
        animator.SetTrigger("OnHit");
        StartCoroutine(HitAnimationCoroutine());
    }

    private System.Collections.IEnumerator HitAnimationCoroutine()
    {
        yield return new WaitForSeconds(hitSpeed);
        Despawn();
    }

    private void Despawn()
    {
        Destroy(gameObject);
    }
}
