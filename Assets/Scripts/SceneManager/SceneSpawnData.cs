using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "SpawnPoints_", menuName = "Scene Management/Spawn Point Database")]
public class SceneSpawnData : ScriptableObject
{
    public string sceneName;
    public List<string> spawnPointIDs = new List<string>();
}
