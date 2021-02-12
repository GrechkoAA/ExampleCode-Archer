using DG.Tweening;
using UnityEngine;

public class MainSetupMenu : MonoBehaviour
{
    [SerializeField] private RectTransform _rectTransform;
    [SerializeField] private Ease _ease;
    [SerializeField] private float _delay;

    private bool _isOpen;

    public void OnOpen()
    {
        _rectTransform.DOAnchorPosX(_isOpen == true ? 0 : 300, _delay, true).SetEase(_ease);
        _isOpen = !_isOpen;
    }
}