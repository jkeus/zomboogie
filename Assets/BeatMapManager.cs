using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

[System.Serializable]

//this stuff below is if you want to make your beatmaps using hte inspector
public class SpawnEvent
{
    [Header("General Settings")]
    public float spawnTime;          //time to spawn
    public string prefabName;        //which prefab (e.g., "Enemy", "Shuriken")
    
    [Header("Location Settings")]
    public Vector3 location;         //spawn location

    [Header("Enemy Settings")]
    public float idleDuration = 1.0f; //how long enemy stays idl
    public float smokeDuration = 1.0f; //how long smoke last to spawn enemy
    public float hitSpeed = 0.5f; //how long enemies last after hit

    [Header("Shuriken Settings")]
    public float shurikenLifeTime = 3.0f; //lifetime for shurikens before despawn
}

//if you want to use json datat to spawn in enemies
public class BeatMapManager : MonoBehaviour
{
    public TextAsset baseSpawnFile; //json for spawn
    public List<SpawnEvent> spawnEvents;  //list to put them in

    public GameObject enemyPrefab;
    public GameObject shurikenPrefab;

    private Dictionary<string, GameObject> prefabDictionary;
    private AudioManager audioManager;

    private void Awake()
    {
        prefabDictionary = new Dictionary<string, GameObject> //dict to parse existing jsons
        {
            { "Enemy", enemyPrefab },
            { "Shuriken", shurikenPrefab }
        };

        LoadSpawnEventsFromJSON();
    }

    private void LoadSpawnEventsFromJSON() //parsing json if there
    {
        if (baseSpawnFile != null)
        {
            spawnEvents = JsonUtility.FromJson<SpawnEventsList>(baseSpawnFile.text).events;
        }
        else
        {
            Debug.LogError("No baseSpawnFile assigned in the Inspector!");
        }
    }


    private void Start() //start a couruoutine for each item in the list so that they are independant
    {
        audioManager = FindObjectOfType<AudioManager>();
        
        foreach (var spawnEvent in spawnEvents)
        {
            StartCoroutine(SpawnEventCoroutine(spawnEvent));
        }

        StartCoroutine(CheckAudioEnd());
    }

    private IEnumerator SpawnEventCoroutine(SpawnEvent spawnEvent)
    {
        //wait until the spawn time
        yield return new WaitForSeconds(spawnEvent.spawnTime);

        if (prefabDictionary.TryGetValue(spawnEvent.prefabName, out GameObject prefabToSpawn)) //if there is a prefab
        {
            GameObject spawnedObject = Instantiate(prefabToSpawn, spawnEvent.location, Quaternion.identity);

            if (spawnEvent.prefabName == "Enemy") //if enemy make one
            {
                HandleEnemy(spawnedObject, spawnEvent);
            }
            else if (spawnEvent.prefabName == "Shuriken") //if shuriken make one
            {
                StartCoroutine(DestroyShurikenAfterLifetime(spawnedObject, spawnEvent.shurikenLifeTime)); //new object spawn
            }
        }
        else
        {
            Debug.LogError("Prefab for " + spawnEvent.prefabName + " is not found in the dictionary.");
        }
    }

    private IEnumerator CheckAudioEnd() //check for if the song is over
    {
        yield return new WaitUntil(() => !audioManager.audioSource.isPlaying);
        SceneManager.LoadScene("MainMenu");
    }

    private void HandleEnemy(GameObject enemy, SpawnEvent spawnEvent) //set params for enemy
    {
        EnemyController enemyController = enemy.GetComponent<EnemyController>();
        if (enemyController != null)
        {
            enemyController.idleDuration = spawnEvent.idleDuration;
            enemyController.spawnSpeed = spawnEvent.smokeDuration;
            enemyController.hitSpeed = spawnEvent.hitSpeed;

            StartCoroutine(DestroyEnemyAfterIdle(enemy, spawnEvent.idleDuration + spawnEvent.smokeDuration));
        }
    }

    //since i scrapped the complexity from the shuriken all we need is the destroy since it just drops at the locaiton given

    private IEnumerator DestroyEnemyAfterIdle(GameObject enemy, float idleDuration) //destroy the enemy
    {
        yield return new WaitForSeconds(idleDuration);

        if (enemy != null)
        {
            Destroy(enemy);
        }
    }

    private IEnumerator DestroyShurikenAfterLifetime(GameObject shuriken, float lifeTime) //destroy the sjuriken
    {
        yield return new WaitForSeconds(lifeTime);

        if (shuriken != null)
        {
            Destroy(shuriken);
        }
    }
}

[System.Serializable]
public class SpawnEventsList
{
    public List<SpawnEvent> events;
}
