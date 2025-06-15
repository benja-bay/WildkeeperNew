using UnityEngine;
using Objects;

#if UNITY_EDITOR
using UnityEditor;
#endif

[RequireComponent(typeof(Collider2D))]
public class DoorObject : MonoBehaviour, IInteractable
{
    public SceneSpawnData spawnData;
    public int selectedSpawnIndex = 0;

    [HideInInspector]
    public string targetScene;

#if UNITY_EDITOR
    public SceneAsset sceneAsset; // solo visible en el Editor
#endif

    private string SelectedSpawnID =>
        spawnData != null && spawnData.spawnPointIDs.Count > selectedSpawnIndex
            ? spawnData.spawnPointIDs[selectedSpawnIndex]
            : "";

    public void Interact(Player.Player player)
    {
        if (string.IsNullOrEmpty(targetScene) || string.IsNullOrEmpty(SelectedSpawnID))
        {
            Debug.LogWarning("Faltan datos para la transición de escena.");
            return;
        }

        SceneSpawnManager.Instance.SetNextSpawnPoint(SelectedSpawnID);
        UnityEngine.SceneManagement.SceneManager.LoadScene(targetScene);
    }
}
