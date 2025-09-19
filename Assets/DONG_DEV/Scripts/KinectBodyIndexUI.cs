using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Bỏ
/// </summary>
public class KinectBodyIndexUI : MonoBehaviour
{
    [SerializeField] RawImage rawImage1;
    [SerializeField] RawImage rawImage2;
    AspectRatioFitter aspect;

    void Awake()
    {
        //rawImage = GetComponent<RawImage>();
        aspect = gameObject.AddComponent<AspectRatioFitter>();
        aspect.aspectMode = AspectRatioFitter.AspectMode.FitInParent;
    }

    void Update()
    {
        if (Kinect_Manager.Instance != null && Kinect_Manager.Instance.BodyTexture != null)
        {
            rawImage1.texture = Kinect_Manager.Instance.BodyTexture;
            //aspect.aspectRatio = (float)Kinect_Manager.Instance.Width_Dec / Kinect_Manager.Instance.Height_Dec;
        }
        if (Kinect_Manager.Instance != null && Kinect_Manager.Instance.BodyTexture1 != null)
        {
            rawImage2.texture = Kinect_Manager.Instance.BodyTexture1;
        }
    }
}
