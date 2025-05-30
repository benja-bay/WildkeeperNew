using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SpawnPoint : MonoBehaviour
{
    public string spawnID;

    private void OnValidate()
    {
        if (!string.IsNullOrEmpty(spawnID))
        {
            gameObject.name = "Spawn_" + spawnID;
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogWarning($"SpawnPoint en {gameObject.name} no tiene un ID asignado.");
        }
#endif
    }
}
