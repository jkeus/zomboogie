using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance; 
    private Text scoreText; 
    private int score = 0;

    private void Awake()
    {
        //check only one instance of ScoreManager exists
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject); //persist across scenes if needed
    }

    private void Start()
    {
        //find score
        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();
        if (scoreText == null)
        {
            Debug.LogError("ScoreText UI element not found in the scene.");
        }
        UpdateScoreUI();
    }

    public void AddPoints(int points) //add potins
    {
        score += points;
        UpdateScoreUI();
    }

    private void UpdateScoreUI() //update
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + score;
        }
    }
}
