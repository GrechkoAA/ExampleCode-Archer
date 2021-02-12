using UnityEngine;

public class UIGamePoints : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _score;
    [SerializeField] private GamePointsModel _gamePointsModel;

    private void OnEnable()
    {
        _gamePointsModel.ScoreChanged += (generalPoints, score, position) =>
        {
            _score.text = generalPoints.ToString();
        };
    }
}