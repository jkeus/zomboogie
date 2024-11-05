using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool GameOver { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EndGame()
    {
        GameOver = true;
        FindObjectOfType<AudioManager>().StopAudio(); //stop audio on game over
        SceneManager.LoadScene("MainMenu");
    }
}
