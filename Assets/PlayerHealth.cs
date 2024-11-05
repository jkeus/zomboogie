using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 6;
    private int currentHealth;

    public Image[] hearts;
    public Sprite fullHeartSprite;
    public Sprite halfHeartSprite;
    public Sprite emptyHeartSprite;

    [SerializeField] private float invulnerabilityDuration = 0.5f; //invulnerability duration after taking damage
    private bool isInvulnerable = false;

    [SerializeField] private AudioClip hitSound;
    private AudioSource audioSource;

    [SerializeField] private TextMeshProUGUI scoreText;
    private int score = 0;

    private void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the player.");
        }

        if (scoreText == null)
        {
            Debug.LogError("ScoreText is not assigned in the Inspector. Please assign it.");
        }
        else
        {
            UpdateScoreUI();
        }
    }

    public void TakeDamage(int damageAmount)
    {
        if (isInvulnerable) return; //skip if invulnerable

        currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
        
        if (audioSource != null && hitSound != null)
        {
            audioSource.PlayOneShot(hitSound);
        }

        UpdateHealthUI();

        if (currentHealth <= 0)
        {
            GameOver();
        }
        else
        {
            StartCoroutine(InvulnerabilityCoroutine());
        }
    }

    public void RestoreHealth(int amount)
    {
        currentHealth = Mathf.Min(currentHealth + amount, maxHealth); //restore health with max limit
        UpdateHealthUI();
    }

    private void UpdateHealthUI()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            int heartHealth = Mathf.Clamp(currentHealth - (i * 2), 0, 2);

            if (heartHealth == 2)
            {
                hearts[i].sprite = fullHeartSprite;
            }
            else if (heartHealth == 1)
            {
                hearts[i].sprite = halfHeartSprite;
            }
            else
            {
                hearts[i].sprite = emptyHeartSprite;
            }
        }
    }

    private void GameOver()
    {
        Debug.Log("Game over!");
        Time.timeScale = 0; //pause game
        StartCoroutine(ReturnToMainMenuAfterDelay(2f));
    }

    private IEnumerator ReturnToMainMenuAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay); //wait in real-time
        Time.timeScale = 1; //resume game
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration); //invulnerability period
        isInvulnerable = false;
    }

    public void AddPoints(int points)
    {
        score += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
        else
        {
            Debug.LogWarning("ScoreText is not assigned or found.");
        }
    }
}
