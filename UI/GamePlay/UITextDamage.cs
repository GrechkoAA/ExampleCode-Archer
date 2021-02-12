using UnityEngine;
using DG.Tweening;

public class UITextDamage : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _score;
    [SerializeField] private GamePointsModel _gamePointsModel;
    [SerializeField] private Camera _camera;
    [SerializeField] private float _durationMovement;

    private void OnEnable()
    {
        _gamePointsModel.ScoreChanged += (generalPoints, points, position) =>
        {
             var offsetPosition = 100;
             Vector3 targetPositionScreen = _camera.WorldToScreenPoint(position);
            
             _score.enabled = true;
             _score.text = points.ToString();
            
             targetPositionScreen.y += offsetPosition;
             transform.position = targetPositionScreen;
            
             transform.DOMoveY(-offsetPosition, _durationMovement)
                      .SetEase(Ease.Linear)
                      .From(true)
                      .OnComplete(() => _score.enabled = false);
            
        };
    }
}