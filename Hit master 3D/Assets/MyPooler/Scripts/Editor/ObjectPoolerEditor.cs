using UnityEngine;
using UnityEditor;
using MyPooler;

[CustomEditor(typeof(ObjectPooler))]
public class ObjectPoolerEditor : Editor
{

    public SerializedProperty pools, isDebug, shouldDestroyOnLoad;

    void OnEnable()
    {
        isDebug = serializedObject.FindProperty("isDebug");
        shouldDestroyOnLoad = serializedObject.FindProperty("shouldDestroyOnLoad");
        pools = serializedObject.FindProperty("pools");

    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        GUI.color = Color.white;
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
      

        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(isDebug, new GUIContent("IsDebug"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(shouldDestroyOnLoad, new GUIContent("Should Destroy on Load"));
        EditorGUILayout.Space();
        EditorGUILayout.PropertyField(pools, new GUIContent("Pools"));      
        EditorGUILayout.Space();
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        serializedObject.ApplyModifiedProperties();
    }

}
