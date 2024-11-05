using UnityEngine;

public class ClickEffectController : MonoBehaviour
{
    public GameObject clickEffectPrefab; // assign the prefab in the inspector
    private GameObject currentClickEffectInstance;
    
    private AudioSource songAudioSource; // Reference to the song's AudioSource
    private AudioManager audioManager;   // Reference to the AudioManager

    private void Start()
    {
        // Find the AudioManager in the scene and get its AudioSource
        audioManager = FindObjectOfType<AudioManager>();
        if (audioManager != null)
        {
            songAudioSource = audioManager.audioSource;
        }
        else
        {
            Debug.LogWarning("AudioManager not found in the scene. Click timing will not be recorded.");
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) // check for left mouse click
        {
            //destroy existing mouse animation
            if (currentClickEffectInstance != null)
            {
                Destroy(currentClickEffectInstance);
            }

            //get mouse coords
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; //z=0 cuz 2d

            //make a new click effect at new locations
            currentClickEffectInstance = Instantiate(clickEffectPrefab, mousePosition, Quaternion.identity);

            Animator animator = currentClickEffectInstance.GetComponent<Animator>();

            //play the audio on the prefab
            AudioSource audioSource = currentClickEffectInstance.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }

            //get the animation length and schedule deletion
            float animationLength = animator.GetCurrentAnimatorStateInfo(0).length;
            Destroy(currentClickEffectInstance, animationLength);

            //debug to help with the map building python script
            if (songAudioSource != null)
            {
                Debug.Log("Click at time (from beginning of song): " + songAudioSource.time + " seconds, Position: (" + mousePosition.x + ", " + mousePosition.y + ")");
            }

            FindObjectOfType<PlayerHealth>().AddPoints(-25);
        }
    }
}
