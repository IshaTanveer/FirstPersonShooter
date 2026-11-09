using UnityEngine;
using UnityEditor;

public class MyEditorWindow : EditorWindow
{
    Color color;
    [MenuItem("Window/Colorizer")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<MyEditorWindow>("Colorizer");
    }
    void OnGUI()
    {
        GUILayout.Label("Color the selected Objects", EditorStyles.centeredGreyMiniLabel);
        color = EditorGUILayout.ColorField("Color", color);
        if (GUILayout.Button("Enter"))
        {
            foreach(GameObject obj in Selection.gameObjects)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if(renderer != null)
                {
                    renderer.sharedMaterial.color = color;
                }
            }
        }
    }
}
