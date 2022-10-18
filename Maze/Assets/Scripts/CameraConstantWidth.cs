using UnityEngine;
public class CameraConstantWidth : MonoBehaviour
{
    public Vector2 DefaultResolution = new Vector2(720, 1280);
    [Range(0f, 1f)] public float WidthOrHeight = 1;

    private Camera componentCamera;
    
    private float initialSize;
    private float targetAspect;

    private void Start()
    {
        componentCamera = Camera.main;
        initialSize = componentCamera.orthographicSize;
        targetAspect = DefaultResolution.x / DefaultResolution.y;
    }

    private void Update()
    {
        if (componentCamera.orthographic)
        {
            float constantWidthSize = initialSize * (targetAspect / componentCamera.aspect);
            componentCamera.orthographicSize = Mathf.Lerp(constantWidthSize, initialSize, WidthOrHeight);
        }
    }
}