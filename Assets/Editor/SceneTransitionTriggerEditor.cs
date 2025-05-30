using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SceneTransitionTrigger))]
public class SceneTransitionTriggerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        SceneTransitionTrigger trigger = (SceneTransitionTrigger)target;

        // Selector de escena
        trigger.sceneAsset = (SceneAsset)EditorGUILayout.ObjectField("Scene", trigger.sceneAsset, typeof(SceneAsset), false);

        // Guardar el nombre de la escena automáticamente
        if (trigger.sceneAsset != null)
        {
            string path = AssetDatabase.GetAssetPath(trigger.sceneAsset);
            trigger.targetScene = System.IO.Path.GetFileNameWithoutExtension(path);
        }

        // Spawn Point
        trigger.targetSpawnPoint = EditorGUILayout.TextField("Target Spawn Point", trigger.targetSpawnPoint);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(trigger);
        }
    }
}
