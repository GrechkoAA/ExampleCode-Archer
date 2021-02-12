using UnityEngine;

public class GamePointsModel : MonoBehaviour
{
    [SerializeField] private SaveSystem _saveSystem;
    [SerializeField] private ListTargetsLevel _listTargets;

    private int _currentScore;

    public int Score => _currentScore;

    public event System.Action<int, int, Vector3> ScoreChanged;

    private void Start()
    {
        _currentScore = _saveSystem.GetGamePoints();

        OnUpdateScore(0);
        SubscribeUpdateGamePoints();
    }

    private void SubscribeUpdateGamePoints()
    {
        if (_listTargets == null)
        {
            return;
        }

        for (int level = 0; level < _listTargets.Count; level++)
        {
            for (int targetId = 0; targetId < _listTargets[level].Count; targetId++)
            {
                _listTargets[level][targetId].ReceivedDamage += UpdateScore;
            }
        }
    }

    private void UpdateScore(int score, Vector3 position)
    {
        _currentScore += score;
        ScoreChanged?.Invoke(_currentScore, score, position);

        _saveSystem.SetGamePoints(_currentScore);
    }

    public void OnUpdateScore(int score)
    {
        _currentScore += score;

        ScoreChanged?.Invoke(_currentScore, score, Vector3.zero);
        _saveSystem.SetGamePoints(_currentScore);
    }
}