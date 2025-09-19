using UnityEngine;
using Windows.Kinect;
/// <summary>
/// Bỏ
/// </summary>
public class BodyVisualizer : MonoBehaviour
{
    public GameObject jointPrefab;

    private GameObject[] jointObjects1;
    private GameObject[] jointObjects2;

    private Body trackedBody1;
    private Body trackedBody2;

    [SerializeField] Transform BodyJoints1;
    [SerializeField] Transform BodyJoints2;

    private bool hasBody1, hasBody2;

    // cache mảng joint types để tránh Enum.GetValues trong Update
    private JointType[] allJoints;

    void Start()
    {
        allJoints = (JointType[])System.Enum.GetValues(typeof(JointType));

        jointObjects1 = SpawnJoint(BodyJoints1);
        jointObjects2 = SpawnJoint(BodyJoints2);

        hasBody1 = false;
        hasBody2 = false;
    }

    void Update()
    {
        var bodyData = Kinect_Manager.Instance.GetData();
        if (bodyData == null) return;

        trackedBody1 = null;
        trackedBody2 = null;

        float nearestZ1 = float.MaxValue;
        float nearestZ2 = float.MaxValue;

        // tìm tối đa 2 người gần nhất
        for (int i = 0; i < bodyData.Length; i++)
        {
            var b = bodyData[i];
            if (b != null && b.IsTracked)
            {
                float z = b.Joints[JointType.SpineBase].Position.Z;

                if (z < nearestZ1)
                {
                    nearestZ2 = nearestZ1; trackedBody2 = trackedBody1;
                    nearestZ1 = z; trackedBody1 = b;
                }
                else if (z < nearestZ2)
                {
                    nearestZ2 = z; trackedBody2 = b;
                }
            }
        }

        // update body1
        if (trackedBody1 != null)
        {
            if (!hasBody1) { BodyJoints1.gameObject.SetActive(true); hasBody1 = true; }
            UpdateJointPositions(trackedBody1, jointObjects1);
        }
        else if (hasBody1)
        {
            BodyJoints1.gameObject.SetActive(false); hasBody1 = false;
        }

        // update body2
        if (trackedBody2 != null)
        {
            if (!hasBody2) { BodyJoints2.gameObject.SetActive(true); hasBody2 = true; }
            UpdateJointPositions(trackedBody2, jointObjects2);
        }
        else if (hasBody2)
        {
            BodyJoints2.gameObject.SetActive(false); hasBody2 = false;
        }
    }

    void UpdateJointPositions(Body body, GameObject[] joints)
    {
        for (int i = 0; i < allJoints.Length; i++)
        {
            JointType jt = allJoints[i];
            CameraSpacePoint jointPos = body.Joints[jt].Position;

            // dùng localPosition để đỡ tính toán transform world
            joints[i].transform.localPosition = new Vector3(jointPos.X, jointPos.Y, jointPos.Z) * 3f;
        }
    }

    GameObject[] SpawnJoint(Transform parent)
    {
        var arr = new GameObject[25];
        for (int i = 0; i < 25; i++)
        {
            arr[i] = Instantiate(jointPrefab, Vector3.zero, Quaternion.identity, parent);
            arr[i].name = ((JointType)i).ToString();
        }
        parent.gameObject.SetActive(false);
        return arr;
    }
}
