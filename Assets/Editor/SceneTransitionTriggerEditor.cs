using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SceneTransitionTrigger))]
public class SceneTransitionTriggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var trigger = (SceneTransitionTrigger)target;

        // Escena destino
        trigger.sceneAsset = (SceneAsset)EditorGUILayout.ObjectField("Target Scene", trigger.sceneAsset, typeof(SceneAsset), false);
        if (trigger.sceneAsset != null)
        {
            string path = AssetDatabase.GetAssetPath(trigger.sceneAsset);
            trigger.targetScene = System.IO.Path.GetFileNameWithoutExtension(path);
        }

        // Datos de spawn
        trigger.spawnData = (SceneSpawnData)EditorGUILayout.ObjectField("Spawn Data", trigger.spawnData, typeof(SceneSpawnData), false);

        if (trigger.spawnData != null && trigger.spawnData.spawnPointIDs.Count > 0)
        {
            trigger.selectedSpawnIndex = EditorGUILayout.Popup("Spawn Point", trigger.selectedSpawnIndex, trigger.spawnData.spawnPointIDs.ToArray());
        }
        else if (trigger.spawnData != null)
        {
            EditorGUILayout.HelpBox("Este asset no tiene spawn points. Generalo desde Tools > Scene Spawn Data Generator.", MessageType.Warning);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(trigger);
        }
    }
}
