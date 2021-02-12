using UnityEngine;

[System.Serializable]
public class WeaponTrajectory
{
    [Header("Components")]
    [SerializeField] private Camera _mainCamera;
    [SerializeField] private LineRenderer _lineRender;
    [SerializeField] private Transform[] _points;

    [Header("Options")]
    [SerializeField, Range(0, 0.5f)] private float _maximumHeightAnchors;
    [SerializeField] private float _verticalSightSpeed;
    [SerializeField] private float _horizontalSightSpeed;
    [SerializeField] private float _maximumVerticalPositionSight;

    private int _numberLineSegments = 25;
    private Vector3 _distanceFromTouchToSight;
    private Vector3[] _originalPointPosition;

    public Transform[] Points => _points;

    public RaycastHit HitTarget;

    public bool IsTarget { get; private set; }

    public void Update()
    {
        Draw();
    }

    private void Draw()
    {
        _lineRender.positionCount = _numberLineSegments + 1;
        _lineRender.enabled = true;

        Vector3 preveousePoint = _points[0].position;

        for (int i = 0; i < _numberLineSegments + 1; i++)
        {
            float paremeter = (float)i / _numberLineSegments;
            Vector3 point = Bezier.GetPoint(_points[0].position, _points[1].position, _points[2].position, _points[3].position, paremeter);

            if (Physics.Linecast(preveousePoint, point, out HitTarget))
            {
                _lineRender.positionCount = i;

                IsTarget = GetTarget(HitTarget);

                break;
            }

            preveousePoint = point;
            _lineRender.SetPosition(i, preveousePoint);
        }
    }

    private bool GetTarget(RaycastHit hitTarget)
    {
        if (hitTarget.collider.gameObject.TryGetComponent(out Health _) || hitTarget.collider.GetComponentInParent<Health>())
            return true;
        else
            return false;
    }

    public void Activate(Vector2 screenPosion)
    {
        SetOriginAnchorPosition();
        ResetAnchorPosition();

        _distanceFromTouchToSight = _points[3].position - GetWorldPositionFromScreen(screenPosion);
    }

    private void SetOriginAnchorPosition()
    {
        float offsetTarget = 7f;
        _originalPointPosition = new Vector3[_points.Length];

        for (int i = 0; i < _points.Length - 1; i++)
        {
            _originalPointPosition[i] = _points[i].position;
        }

        _originalPointPosition[3] = new Vector3(_points[0].position.x, _points[3].position.y, _points[0].position.z + offsetTarget);
    }

    public void Move(Vector2 screenPosion)
    {
        MoveSight(screenPosion);
        MoveAnchors(screenPosion);
    }

    public void Disable()
    {
        _lineRender.enabled = false;
    }

    private void MoveSight(Vector2 screenPosition)
    {
        int screenBordersInPercent = 20;
        float projectionDistance = 12;
        float horizontalScreenClamp = GetHorizontalScreenClamp(screenPosition.x, screenBordersInPercent);

        Vector3 clampingWorldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(horizontalScreenClamp, screenPosition.y, projectionDistance));
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, projectionDistance));

        _points[3].position = new Vector3(GetInversionHorizontalWorldClamp(worldPosition.x, clampingWorldPosition.x),
                                                                           _points[3].position.y,
                                                                           GetVerticalClamp(GetWorldPositionFromScreen(screenPosition).z));
    }

    private void MoveAnchors(Vector2 screenPosition)
    {
        int screenBordersInPercent = 18;
        float projectionDistance = Vector3.Distance(_points[3].position, _mainCamera.transform.position);
        float horizontalScreenClamp = GetHorizontalScreenClamp(screenPosition.x, screenBordersInPercent);

        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(horizontalScreenClamp, screenPosition.y, projectionDistance));
        float verticalClamp = Mathf.Clamp(worldPosition.y, GetAnchorPositions(0.5f).y, GetAnchorPositions(0.5f).y);
        float distanceExtremePoints = Vector3.Distance(_points[0].position, _points[3].position);

        _points[1].position = new Vector3(worldPosition.x, verticalClamp + distanceExtremePoints * _maximumHeightAnchors, GetAnchorPositions(0.33f).z);
        _points[2].position = new Vector3(worldPosition.x, verticalClamp + distanceExtremePoints * _maximumHeightAnchors, GetAnchorPositions(0.66f).z);
    }

    private float GetInversionHorizontalWorldClamp(float worldTouchPosition, float worldTouchClampingPosition)
    {
        float position;
        float borderEdgePosition = worldTouchClampingPosition - (worldTouchPosition - worldTouchClampingPosition) * _horizontalSightSpeed;

        if ((worldTouchPosition - worldTouchClampingPosition) < 0)
        {
            position = Mathf.Clamp(borderEdgePosition, float.NegativeInfinity, _points[0].position.x);
        }
        else if ((worldTouchPosition - worldTouchClampingPosition) > 0)
        {
            position = Mathf.Clamp(borderEdgePosition, _points[0].position.x, float.PositiveInfinity);
        }
        else
        {
            position = worldTouchClampingPosition - (worldTouchPosition - worldTouchClampingPosition) * _horizontalSightSpeed;
        }

        return position;
    }

    private float GetVerticalClamp(float worldPosition)
    {
        float offsetForward = 0.3f;

        return Mathf.Clamp(worldPosition + _distanceFromTouchToSight.z,
                          _points[0].position.z + offsetForward,
                          _points[0].position.z + _maximumVerticalPositionSight);
    }

    private float GetHorizontalScreenClamp(float horizontalScreenPosition, int screenBorder)
    {
        float maximumPercentage = 100;

        return Mathf.Clamp(horizontalScreenPosition,
                           Screen.width * screenBorder / maximumPercentage,
                           Screen.width - Screen.width * screenBorder / maximumPercentage);
    }

    private Vector3 GetAnchorPositions(float percentageWay)
    {
        Vector3 direction = _points[3].position - _points[0].position;

        return _points[0].position + percentageWay * direction;
    }

    private Vector3 GetWorldPositionFromScreen(Vector2 screenPosition)
    {
        Vector3 worldPosition = _mainCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, _verticalSightSpeed));

        return new Vector3(worldPosition.x, _points[0].position.y, worldPosition.z);
    }

    private void ResetAnchorPosition()
    {
        for (int i = 0; i < _points.Length; i++)
        {
            _points[i].position = _originalPointPosition[i];
        }
    }
}