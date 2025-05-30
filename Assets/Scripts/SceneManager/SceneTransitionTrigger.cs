using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class SceneTransitionTrigger : MonoBehaviour
{
    [HideInInspector]
    public string targetScene;

#if UNITY_EDITOR
    public SceneAsset sceneAsset;
#endif

    public SceneSpawnData spawnData;
    public int selectedSpawnIndex = 0;

    public string SelectedSpawnID =>
        spawnData != null && spawnData.spawnPointIDs.Count > selectedSpawnIndex
        ? spawnData.spawnPointIDs[selectedSpawnIndex]
        : "";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneSpawnManager.Instance.SetNextSpawnPoint(SelectedSpawnID);
            UnityEngine.SceneManagement.SceneManager.LoadScene(targetScene);
        }
    }
}
