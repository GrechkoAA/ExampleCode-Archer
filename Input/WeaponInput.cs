using UnityEngine;
using UnityEngine.EventSystems;

public class WeaponInput : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public event System.Action<Vector2> ConstructedTrajectory;
    public event System.Action<Vector2> MovedTrajectory;
    public event System.Action DisabledTrajectory;

    private bool _isTouch;
    private int _pointerId;

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isTouch == false)
        {
            ConstructedTrajectory?.Invoke(eventData.position);

            _pointerId = eventData.pointerId;
           _isTouch = true;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (IsOneTouch(eventData) == true)
        {
            MovedTrajectory?.Invoke(eventData.position);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsOneTouch(eventData) == true)
        {
            DisabledTrajectory?.Invoke();

            _isTouch = false;
        }
    }

    private bool IsOneTouch(PointerEventData eventData)
    {
        return _pointerId == eventData.pointerId;
    }
}