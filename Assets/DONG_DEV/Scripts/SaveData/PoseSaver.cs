using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class PoseSaver : MonoBehaviour
{
    public List<GameObject> AllJoints;
    public string baseFileName = "pose"; // tên gốc
    public string saveFolder = "Assets/DONG_DEV/GameData/ConfigData/PoseDataJson"; // nơi lưu file

    public PlayerManager playermanager;

    private readonly Dictionary<string, int[]> jointIndexGroups = new Dictionary<string, int[]>
    {
        { "head",       new [] { 3, 2, 20 } },
        { "body",       new [] { 20, 1, 0 } },
        { "shoulder",   new [] { 4, 20, 8 } },
        { "leftHand",   new [] { 4, 5, 6, 7 } },
        { "rightHand",  new [] { 8, 9, 10, 11 } },
        { "leftFoot",   new [] { 12, 13, 14 } },
        { "rightFoot",  new [] { 16, 17, 18 } },
    };

    [System.Serializable]
    public class PoseData
    {
        public List<Vector3> head = new();
        public List<Vector3> body = new();
        public List<Vector3> shoulder = new();
        public List<Vector3> leftHand = new();
        public List<Vector3> rightHand = new();
        public List<Vector3> leftFoot = new();
        public List<Vector3> rightFoot = new();
    }

    public void SavePoseToJson(List<GameObject> allJoints)
    {
        if (allJoints == null || allJoints.Count == 0)
        {
            Debug.LogWarning("Danh sách allJoints trống!");
            return;
        }

        // 1. Chuẩn bị dữ liệu
        PoseData data = new PoseData();

        foreach (var group in jointIndexGroups)
        {
            List<Vector3> positions = new();
            foreach (int index in group.Value)
            {
                if (index < allJoints.Count && allJoints[index] != null)
                    positions.Add(allJoints[index].transform.position);
            }

            switch (group.Key)
            {
                case "head": data.head = positions; break;
                case "body": data.body = positions; break;
                case "shoulder": data.shoulder = positions; break;
                case "leftHand": data.leftHand = positions; break;
                case "rightHand": data.rightHand = positions; break;
                case "leftFoot": data.leftFoot = positions; break;
                case "rightFoot": data.rightFoot = positions; break;
            }
        }

        // 2. Tìm số thứ tự file tiếp theo
        if (!Directory.Exists(saveFolder))
            Directory.CreateDirectory(saveFolder);

        int nextIndex = GetNextFileIndex();

        string fileName = $"{baseFileName}_{nextIndex}.json";
        string path = System.IO.Path.Combine(saveFolder, fileName);

        // 3. Ghi file JSON
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(path, json);

#if UNITY_EDITOR
        UnityEditor.AssetDatabase.Refresh(); // để thấy file ngay trong Unity
#endif

        Debug.Log($"Đã lưu pose: {path}");
    }

    private int GetNextFileIndex()
    {
        // Lấy tất cả file trong thư mục có dạng pose_*.json
        string[] files = Directory.GetFiles(saveFolder, $"{baseFileName}_*.json");

        int maxIndex = 0;
        foreach (var f in files)
        {
            string file = Path.GetFileNameWithoutExtension(f);
            string[] parts = file.Split('_');
            if (parts.Length > 1 && int.TryParse(parts[^1], out int index))
            {
                if (index > maxIndex)
                    maxIndex = index;
            }
        }

        return maxIndex + 1; // file kế tiếp
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (AllJoints.Count == 0)
            {
                AllJoints = new List<GameObject>();
                for (int i = 0; i < playermanager.JointObjects1.Length; i++)
                {
                    AllJoints.Add(playermanager.JointObjects1[i]);
                }
            }
            SavePoseToJson(AllJoints);
        }
    }
}
