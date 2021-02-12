using UnityEngine;

public class ConstantWidth : MonoBehaviour
{
    [SerializeField] Camera _camera;
    [SerializeField] private Vector2 _defaultResolution;

    private void Start()
    {
        SetFieldOfView();
    }

    private void SetFieldOfView()
    {
        float targetAspect = _defaultResolution.x / _defaultResolution.y;
        float initialFov = _camera.fieldOfView;
        float horizontalFov = CalcVerticalFov(initialFov, 1 / targetAspect);
        float constantWidthFov = CalcVerticalFov(horizontalFov, _camera.aspect);

        _camera.fieldOfView = constantWidthFov;
    }

    private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
    {
        var hFovInRads = hFovInDeg * Mathf.Deg2Rad;
        var vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

        return vFovInRads * Mathf.Rad2Deg;
    }
}