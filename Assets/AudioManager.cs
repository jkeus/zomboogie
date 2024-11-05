using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource; // Drag the AudioSource with the music clip to play

    private void Start()
    {
        //play audio on start
        audioSource.time = 0;
        audioSource.Play();
    }

    private void Update()
    {
        // if the song is over go to mainmenu, check the audio length and gameover 
        if (!audioSource.isPlaying && !GameManager.Instance.GameOver)
        {
            SceneManager.LoadScene("MainMenu"); 
        }
    }

    public void StopAudio()
    {
        audioSource.Stop();
    }
}
