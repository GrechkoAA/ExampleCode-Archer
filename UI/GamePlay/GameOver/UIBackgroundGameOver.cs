using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(UIGameOver))]
public class UIBackgroundGameOver : MonoBehaviour
{
    [SerializeField] private UIGameOver _uiGameOver;
    [SerializeField] private UnityEngine.UI.Image _background;
    [SerializeField] private float _delay;

    public void Show(bool _)
    {
        _background.gameObject.SetActive(true);
        _background.raycastTarget = true;
        _background.DOFade(0.75f, 0.1f).SetDelay(_delay);
    }

    private void OnEnable()
    {
        _uiGameOver.Enabled += Show;
    }
}
