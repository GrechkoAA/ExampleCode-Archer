using UnityEngine;

[RequireComponent(typeof(TargetList))]
public class Level : MonoBehaviour
{
    [SerializeField] private int _additionalAttempts;
    [SerializeField] private ListTargetsLevel _listTargets;
    [SerializeField] private UnityEventString _attemptsReduced;

    public int CurrentLevel { get; private set; }

    public event System.Action Completed;
    public event System.Action<bool> SceneFinished;

    public int CurrentAttempts => _additionalAttempts;

    public int MaximumAttempts { get; private set; }

    private void Start()
    {
        MaximumAttempts = _additionalAttempts;

        SetNumberHits();

        _attemptsReduced?.Invoke($"x{_additionalAttempts}");
    }

    private void SetNumberHits()
    {
        for (int level = 0; level < _listTargets.Count; level++)
        {
            for (int target = 0; target < _listTargets[level].Count; target++)
            {
                _listTargets[level].MaximumNumberShots += _listTargets[level][target].Healpt;
            }
        }
    }

    public void UpdateTargetList(GameObject other)
    {
        if (NumberShots() == 0)
        {
            throw new System.Exception("There are no targets on the level");
        }

        if (other.TryGetComponent(out Health _))
        {
            UpdateNumberShots();
            UpdateLevel();
        }
        else
        {
            UpdateAttempts();
        }
    }

    private void UpdateNumberShots()
    {
        _listTargets[CurrentLevel].MaximumNumberShots--;
    }

    private void UpdateLevel()
    {
        if (CanGoNextLevel() == true)
        {
            CurrentLevel++;

            if (IsGameFinished() == true)
            {
                SceneFinished?.Invoke(true);
            }
            else
            {
                Completed?.Invoke();
            }
        }
    }

    private void UpdateAttempts()
    {
        _additionalAttempts--;

        _attemptsReduced?.Invoke($"x{_additionalAttempts}");

        if (_additionalAttempts == 0)
        {
            SceneFinished?.Invoke(false);
        }
    }

    private int NumberShots()
    {
        return _listTargets[CurrentLevel].MaximumNumberShots;
    }

    private bool CanGoNextLevel()
    {
        return NumberShots() == 0;
    }

    private bool IsGameFinished()
    {
        return CurrentLevel == _listTargets.Count;
    }
}

[System.Serializable]
public class UnityEventString : UnityEngine.Events.UnityEvent<string>
{
}