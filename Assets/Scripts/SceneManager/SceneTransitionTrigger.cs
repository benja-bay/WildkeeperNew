using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneTransitionTrigger : MonoBehaviour
{
    [HideInInspector]
    public string targetScene; // Esto es lo que se usará en runtime

#if UNITY_EDITOR
    public SceneAsset sceneAsset; // Solo visible en el editor
#endif

    public string targetSpawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneSpawnManager.Instance.SetNextSpawnPoint(targetSpawnPoint);
            UnityEngine.SceneManagement.SceneManager.LoadScene(targetScene);
        }
    }
}
