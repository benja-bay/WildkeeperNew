using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSpawnManager : MonoBehaviour
{
    public static SceneSpawnManager Instance;

    private string spawnPointName;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetNextSpawnPoint(string name)
    {
        spawnPointName = name;
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode)
    {
        if (!string.IsNullOrEmpty(spawnPointName))
        {
            GameObject spawn = GameObject.Find(spawnPointName);
            if (spawn != null)
            {
                GameObject player = GameObject.FindGameObjectWithTag("Player");
                if (player != null)
                {
                    player.transform.position = spawn.transform.position;
                }
            }
            else
            {
                Debug.LogWarning("No se encontró el spawn point con nombre: " + spawnPointName);
            }
        }
    }
}
