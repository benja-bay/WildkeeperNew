using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSpawnManager : MonoBehaviour
{
    public static SceneSpawnManager Instance;

    private string nextSpawnPointID;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetNextSpawnPoint(string spawnPointID)
    {
        nextSpawnPointID = spawnPointID;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (!string.IsNullOrEmpty(nextSpawnPointID))
        {
            GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
            foreach (var obj in allObjects)
            {
                SpawnPoint sp = obj.GetComponent<SpawnPoint>();
                if (sp != null && sp.spawnID == nextSpawnPointID)
                {
                    GameObject player = GameObject.FindGameObjectWithTag("Player");
                    if (player != null)
                    {
                        player.transform.position = sp.transform.position;
                        return;
                    }
                }
            }

            Debug.LogWarning($"No se encontró un SpawnPoint con ID '{nextSpawnPointID}' en la escena '{scene.name}'.");
        }
    }
}
