using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CreateAssetMenu(fileName = "pose", menuName = "ScriptableObjects/partData", order = 3)]
[System.Serializable]
public class ConfigDataSO : ScriptableObject
{
    [Header("Source JSON file")]
    public TextAsset jsonFile;

    [Header("Body Parts Data")]
    public List<Vector3> head;
    public List<Vector3> body;
    public List<Vector3> shoulder;
    public List<Vector3> leftHand;
    public List<Vector3> rightHand;
    public List<Vector3> leftFoot;
    public List<Vector3> rightFoot;
}

[CustomEditor(typeof(ConfigDataSO))]
public class ConfigDataSOEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GUILayout.Space(10);

        ConfigDataSO data = (ConfigDataSO)target;

        if (GUILayout.Button("Load from assigned JSON"))
        {
            LoadFromAssignedJson(data);
        }
    }

    private void LoadFromAssignedJson(ConfigDataSO data)
    {
        if (data.jsonFile == null)
        {
            Debug.LogWarning("Chưa gán file JSON trong ScriptableObject!");
            return;
        }

        string json = data.jsonFile.text;
        if (string.IsNullOrEmpty(json))
        {
            Debug.LogError("File JSON rỗng hoặc lỗi!");
            return;
        }

        JsonData temp = JsonUtility.FromJson<JsonData>(json);
        if (temp == null)
        {
            Debug.LogError("Không thể parse JSON!");
            return;
        }

        data.head = temp.head;
        data.body = temp.body;
        data.shoulder = temp.shoulder;
        data.leftHand = temp.leftHand;
        data.rightHand = temp.rightHand;
        data.leftFoot = temp.leftFoot;
        data.rightFoot = temp.rightFoot;

        EditorUtility.SetDirty(data);

        Debug.Log("Loaded JSON from " + data.jsonFile.name);
    }

    [System.Serializable]
    private class JsonData
    {
        public List<Vector3> head;
        public List<Vector3> body;
        public List<Vector3> shoulder;
        public List<Vector3> leftHand;
        public List<Vector3> rightHand;
        public List<Vector3> leftFoot;
        public List<Vector3> rightFoot;
    }
}
