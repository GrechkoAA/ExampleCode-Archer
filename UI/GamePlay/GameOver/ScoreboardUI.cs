using DG.Tweening;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(UIGameOver))]
public class ScoreboardUI : MonoBehaviour
{
    [SerializeField] private UIGameOver _uiGameOver;
    [SerializeField] private TMP_Text _totalScore;
    [SerializeField] private TMP_Text _sceneScore;
    [SerializeField] private GamePointsModel _gamePointsModel;
    [SerializeField] private RectTransform _scoreboard;
    [SerializeField] private float _delayAnchorPosY;
    [SerializeField] private Ease _easeAnchorPosY;
    [SerializeField] private TMP_Text _hader;

    private int _currentScoreScene;

    private void Show(bool isFinish)
    {
        _scoreboard.gameObject.SetActive(true);
        _hader.text = isFinish == true ? "YOU WIN!": "YOU LOSE!";
        _scoreboard.DOAnchorPosY(0, _delayAnchorPosY).SetEase(_easeAnchorPosY);
    }

    private void OnEnable()
    {
        _uiGameOver.Enabled += Show;

        _gamePointsModel.ScoreChanged += (generalPoints, score, position) =>
        {
            _currentScoreScene += score;
            _totalScore.text = $"You score: {generalPoints}";
            _sceneScore.text = $"+{_currentScoreScene}";
        };
    }
}