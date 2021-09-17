using UnityEngine;
using UnityEditor;

public class DDOL : MonoBehaviour {
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}

[CustomEditor(typeof(DDOL))]
public class DDOLEditor : Editor
{
    public override void OnInspectorGUI()
    {                   
        EditorGUILayout.HelpBox(" INDESTRUCTIBLE // Dont Destroy On Load", MessageType.Warning); 
        // EditorGUILayout.LabelField("");     
    }
}