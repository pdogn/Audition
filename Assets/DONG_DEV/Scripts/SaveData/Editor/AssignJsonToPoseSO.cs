using UnityEngine;
using UnityEditor;
using System.IO;
using System.Reflection;

public class AssignJsonToPoseSO : EditorWindow
{
    private string folderPath = "Assets/DONG_DEV/GameData/ConfigData/Music_1";
    private bool includeSubfolders = false;

    [MenuItem("Tools/Assign JSON to Pose ScriptableObjects")]
    public static void ShowWindow()
    {
        GetWindow<AssignJsonToPoseSO>("Assign JSON to Pose SO");
    }

    private void OnGUI()
    {
        GUILayout.Label("Auto Assign JSON + Load Data", EditorStyles.boldLabel);

        EditorGUILayout.Space(8);

        folderPath = EditorGUILayout.TextField("Target Folder", folderPath);

        if (GUILayout.Button("Chọn Folder..."))
        {
            string selectedPath = EditorUtility.OpenFolderPanel("Chọn thư mục chứa pose", "Assets", "");
            if (!string.IsNullOrEmpty(selectedPath))
            {
                if (selectedPath.StartsWith(Application.dataPath))
                    folderPath = "Assets" + selectedPath.Substring(Application.dataPath.Length);
                else
                    Debug.LogWarning("Vui lòng chọn thư mục nằm trong Assets.");
            }
        }

        includeSubfolders = EditorGUILayout.Toggle("Bao gồm thư mục con", includeSubfolders);

        EditorGUILayout.Space(10);

        if (GUILayout.Button("Assign JSON + Load Data"))
        {
            AssignAndLoadJsons(folderPath, includeSubfolders);
        }
    }

    private void AssignAndLoadJsons(string path, bool searchSubfolders)
    {
        if (!Directory.Exists(path))
        {
            Debug.LogError("Thư mục không tồn tại: " + path);
            return;
        }

        SearchOption option = searchSubfolders ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly;
        string[] assetFiles = Directory.GetFiles(path, "*.asset", option);
        int assigned = 0, loaded = 0;

        foreach (string assetPath in assetFiles)
        {
            string assetName = Path.GetFileNameWithoutExtension(assetPath);
            string folder = Path.GetDirectoryName(assetPath);
            string matchingJsonPath = Path.Combine(folder, assetName + ".json");

            if (!File.Exists(matchingJsonPath)) continue;

            ConfigDataSO so = AssetDatabase.LoadAssetAtPath<ConfigDataSO>(assetPath);
            TextAsset json = AssetDatabase.LoadAssetAtPath<TextAsset>(matchingJsonPath);

            if (so == null || json == null) continue;

            // Gán jsonFile
            so.jsonFile = json;
            assigned++;

            // Tìm Editor tương ứng
            var editor = Editor.CreateEditor(so, typeof(ConfigDataSOEditor));

            // Gọi hàm private "LoadFromAssignedJson" bằng reflection
            MethodInfo method = typeof(ConfigDataSOEditor)
                .GetMethod("LoadFromAssignedJson", BindingFlags.NonPublic | BindingFlags.Instance);

            if (method != null)
            {
                method.Invoke(editor, new object[] { so });
                loaded++;
            }

            Object.DestroyImmediate(editor);
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"Đã gán {assigned} JSON và load dữ liệu thành công {loaded} ScriptableObjects trong: {path}");
    }
}
