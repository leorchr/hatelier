using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.UIElements;
#endif
using UnityEngine;
using UnityEngine.UIElements;


#if UNITY_EDITOR
public class S_ChangeMaterial : EditorWindow
{
    ColorField shadowColorField;
    ColorField lightColorField;
    /// Add a new menu item under an existing menu

    [MenuItem("Tools/Change SH_Main Material Value")]
    public static void Initialize()
    {
        // This method is called when the user selects the menu item in the Editor
        EditorWindow wnd = GetWindow<S_ChangeMaterial>();
        wnd.titleContent = new GUIContent("Change Material Value");

        wnd.minSize = new Vector2(200,100);
    }

    Shader shaderBase ;
    private List<Material> materials = new();

    private void ApplyChange()
    {

        shaderBase = Shader.Find("Shader Graphs/SH_MAIN_SHADER");
        if (shaderBase != null)
        {
            
            int count = 0;
            materials.Clear();
            string[] allMaterialPaths = AssetDatabase.FindAssets("t:Material");
            foreach (string path in allMaterialPaths)
            {
                string assetPath = AssetDatabase.GUIDToAssetPath(path);
                if (assetPath.StartsWith("Assets/"))
                {
                    Material mat = AssetDatabase.LoadAssetAtPath<Material>(assetPath);
                    if (mat != null && mat.shader != null && !string.IsNullOrEmpty(mat.shader.name))
                    {
                        
                        if (mat.shader.name.Equals(shaderBase.name, StringComparison.OrdinalIgnoreCase))
                        {
                            materials.Add(mat);
                            count++;
                        }
                    }
                }

            }
            materials.Sort((a, b) => a.name.CompareTo(b.name));
        }
        
        foreach(Material mat in materials)
        {
            mat.SetColor("_Light_Color", lightColorField.value);
            mat.SetColor("_Shadows_Color", shadowColorField.value);
        }
    }

    

    private void CreateGUI()
    {
        VisualElement root = rootVisualElement;

        shadowColorField = new ColorField();
        shadowColorField.label = "Shadow Color";

        lightColorField = new ColorField();
        lightColorField.label = "Light Color";

        Button button = new Button(ApplyChange);
        button.text = "Apply to all material";
        root.Add(shadowColorField);
        
        root.Add(lightColorField);

        root.Add(button);

    }
}
#endif