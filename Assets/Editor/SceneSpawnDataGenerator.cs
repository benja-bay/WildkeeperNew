using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor.SceneManagement;
using System.IO;
using System.Collections.Generic;

public class SceneSpawnDataGenerator : EditorWindow
{
    private SceneAsset sceneAsset;

    [MenuItem("Tools/Scene Spawn Data Generator")]
    public static void ShowWindow()
    {
        GetWindow<SceneSpawnDataGenerator>("Spawn Data Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Generador Automático de Spawn Points", EditorStyles.boldLabel);

        sceneAsset = (SceneAsset)EditorGUILayout.ObjectField("Escena", sceneAsset, typeof(SceneAsset), false);

        if (sceneAsset != null && GUILayout.Button("Generar SceneSpawnData"))
        {
            GenerateSpawnData(sceneAsset);
        }
    }

    private void GenerateSpawnData(SceneAsset sceneAsset)
    {
        string scenePath = AssetDatabase.GetAssetPath(sceneAsset);

        if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
        {
            Scene openedScene = EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Single);

            SpawnPoint[] spawnPoints = GameObject.FindObjectsOfType<SpawnPoint>();

            if (spawnPoints.Length == 0)
            {
                Debug.LogWarning("No se encontraron SpawnPoints en la escena: " + sceneAsset.name);
                return;
            }

            List<string> ids = new List<string>();

            foreach (SpawnPoint sp in spawnPoints)
            {
                if (!string.IsNullOrEmpty(sp.spawnID))
                {
                    ids.Add(sp.spawnID);
                }
                else
                {
                    Debug.LogWarning($"SpawnPoint sin ID en objeto: {sp.gameObject.name}");
                }
            }

            string folderPath = "Assets/Data/SpawnPoints";
            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);

            string assetPath = $"{folderPath}/SpawnPoints_{sceneAsset.name}.asset";

            SceneSpawnData spawnData = AssetDatabase.LoadAssetAtPath<SceneSpawnData>(assetPath);

            if (spawnData == null)
            {
                spawnData = ScriptableObject.CreateInstance<SceneSpawnData>();
                AssetDatabase.CreateAsset(spawnData, assetPath);
            }

            spawnData.sceneName = sceneAsset.name;
            spawnData.spawnPointIDs = ids;

            EditorUtility.SetDirty(spawnData);
            AssetDatabase.SaveAssets();

            Debug.Log($"SceneSpawnData generado con éxito para {sceneAsset.name} ({ids.Count} spawn points)");
        }
    }
}
